using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace FinanceInvoiceCompare.WebApi.Controllers
{
    /// <summary>
    /// SAP发票操作
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ISAPService sAPService;
        private readonly IInvoiceService invoiceService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<InvoiceController> logger;

        public InvoiceController(ISAPService sAPService, IInvoiceService invoiceService, IUnitOfWork unitOfWork, ILogger<InvoiceController> logger)
        {
            this.sAPService = sAPService;
            this.invoiceService = invoiceService;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }


        /// <summary>
        /// 获取SAP发票信息(分页)
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetSAPInvoiceList")]
        public async Task<MessageModel<PageModel<SAPInvoiceData>>> GetSAPInvoiceList([FromQuery] SAPRequestModel model)
        {
            return new MessageModel<PageModel<SAPInvoiceData>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await sAPService.GetSAPInvoiceList(model)
            };
        }

        /// <summary>
        /// 获取SAP发票信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetAllSAPInvoiceList")]
        public async Task<MessageModel<List<SAPInvoiceData>>> GetAllSAPInvoiceList([FromQuery] SAPRequestModel model)
        {
            return new MessageModel<List<SAPInvoiceData>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await sAPService.GetAllSAPInvoiceList(model)
            };
        }

        /// <summary>
        /// 添加SAP发票信息
        /// </summary>
        /// <param name="sapInvoice">sap发票信息</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("AddSAPInvoice")]
        public async Task<MessageModel<string>> AddSAPInvoice([FromBody] List<SAPInvoiceData> sapInvoice)
        {
            var data = new MessageModel<string>();

            if (sapInvoice.Count > 0)
            {

                sapInvoice.ForEach(item =>
                {
                    if (!string.IsNullOrEmpty(item.Cocd))
                    {
                        item.Cocd = item.Cocd.PadLeft(4, '0');
                    }
                });

                ////检查CompanyCode和DocumentNo的是否唯一
                Expressionable<SAPInvoiceData> exp = new Expressionable<SAPInvoiceData>();

                foreach (var item in sapInvoice)
                {
                    exp.Or(it => it.Cocd == item.Cocd && it.DocumentNo == item.DocumentNo);
                }

                var existsSAPInvoice = await sAPService.Query(exp.ToExpression());

                if (existsSAPInvoice.Count > 0)
                {
                    data.Message = "SAP发票重复，请检查！";
                }
                else
                {
                    var flag = data.Success = await sAPService.Add(sapInvoice) > 0;

                    if (flag)
                    {
                        data.Message = "添加成功";
                    }
                    else
                    {
                        data.Message = "添加失败";
                    }
                }

            }

            return data;
        }




        /// <summary>
        /// 添加Site发票信息
        /// </summary>
        /// <param name="invoices">发票信息</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("AddSiteInvoice")]
        public async Task<MessageModel<List<Invoice>>> AddSiteInvoice([FromBody] List<Invoice> invoices)
        {
            var data = new MessageModel<List<Invoice>>();
            try
            {
                unitOfWork.BeginTran();
                if (invoices.Count > 0)
                {

                    #region 转化数据格式   CompanyCode不够4位前面补0

                    invoices.ForEach(item =>
                    {
                        if (!string.IsNullOrEmpty(item.CompanyCode))
                        {
                            item.CompanyCode = item.CompanyCode.PadLeft(4, '0');
                        }
                    });

                    #endregion

                    #region 检查是否存在相同的发票号

                    Expressionable<Invoice> exp = new Expressionable<Invoice>();

                    foreach (var item in invoices)
                    {
                        exp.Or(it => it.IsDelete == false && it.InvoiceNumber == item.InvoiceNumber && it.CompanyCode == item.CompanyCode);
                    }

                    var exsitsInvoices = await invoiceService.Query(exp.ToExpression());

                    #endregion

                    if (exsitsInvoices.Count > 0)
                    {
                        data.Success = false;
                        data.Message = "存在相同的InvoiceNumber！";
                        data.Response = exsitsInvoices;
                    }
                    else
                    {
                        //// 添加发票信息
                        bool flag = await invoiceService.Add(invoices) > 0;


                        //// 查询上传的发票相匹配的SAP发票的List

                        Expressionable<SAPInvoiceData> expSAP = new Expressionable<SAPInvoiceData>();

                        foreach (var item in invoices)
                        {
                            expSAP.Or(it => it.IsDelete == false && it.Reference == item.InvoiceNumber && it.Cocd == item.CompanyCode);
                        }

                        var matchList = (await sAPService.Query(expSAP.ToExpression())).Select(x => new { x.Reference, x.Cocd }).Distinct().ToList();

                        var updateList = new List<Invoice>();

                        if (matchList.Count > 0)
                        {
                            matchList.ForEach(item =>
                            {
                                updateList.Add(new Invoice() { InvoiceNumber = item.Reference, CompanyCode = item.Cocd, MatchDate = DateTime.Now });
                            });

                            //// 更新Site发票MatchDate
                            data.Success = flag = await invoiceService.Update(updateList, new List<string>() { "MatchDate", "InvoiceNumber", "CompanyCode" }, null, x => new { x.InvoiceNumber, x.CompanyCode });
                        }
                        else
                        {

                            data.Success = flag;
                        }


                        if (flag)
                        {
                            data.Message = "添加成功";
                        }
                        else
                        {
                            data.Message = "添加失败";
                            unitOfWork.RollbackTran();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTran();
                logger.LogError(ex, ex.Message);
            }

            return data;
        }

        /// <summary>
        /// 获取Site发票信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetSiteInvoiceList")]
        public async Task<MessageModel<PageModel<Invoice>>> GetSiteInvoiceList([FromQuery] SiteInvoiceRequestModel model)
        {
            var expressions = Expressionable.Create<Invoice>()
                 .And(it => it.IsDelete == false)
                 .AndIF(!string.IsNullOrEmpty(model.CompanyCode), it => it.CompanyCode == model.CompanyCode)
                 .AndIF(!string.IsNullOrEmpty(model.InvoiceNumber), it => it.InvoiceNumber.Contains(model.InvoiceNumber)).ToExpression();

            return new MessageModel<PageModel<Invoice>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await invoiceService.QueryPage(expressions, model.PageIndex, model.PageSize, " ID desc ")
            };
        }


        /// <summary>
        /// 更新发票
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("SaveInvoice")]

        public async Task<MessageModel<string>> SaveInvoice([FromBody] Invoice model)
        {
            var data = new MessageModel<string>();

            var flag = data.Success = await invoiceService.Update(model, new List<string>() { "InvoiceNumber", "InvoiceDate", "InvoiceDate", "Amount", "UpdatedBy", "UpdatedAt" });
            if (flag)
            {
                data.Message = "更新成功";
            }
            else
            {
                data.Message = "更新失败";
            }

            return data;
        }

    }
}