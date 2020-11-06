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
        /// 获取SAP发票信息
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
        public async Task<MessageModel<int>> AddSiteInvoice([FromBody] List<Invoice> invoices)
        {
            var data = new MessageModel<int>();
            try
            {
                unitOfWork.BeginTran();
                if (invoices.Count > 0)
                {

                    var invoiceNumbers = invoices.Select(x => x.InvoiceNumber).ToArray();
                    var companyCodes = invoices.Select(x => x.CompanyCode).ToArray();



                    // 检查是否存在相同的发票号
                    var exsitsVendors = await invoiceService.Query(x => x.IsDelete == false && invoiceNumbers.Contains(x.InvoiceNumber) && companyCodes.Contains(x.CompanyCode));

                    //var s = await invoiceService.Query(x => x.IsDelete == false);
                    //var list = from item in s
                    //           join item2 in invoices on new { item.CompanyCode ,item.InvoiceNumber} equals new { item2.CompanyCode,item2.InvoiceNumber }
                    //           select item;

                    //var exsitsVendors = list;

                    if (exsitsVendors.Count > 0)
                    {
                        data.Success = false;
                        data.Message = "存在相同的InvoiceNumber，请检查";
                    }
                    else
                    {
                        //// 添加发票信息
                        bool flag = await invoiceService.Add(invoices) > 0;


                        //// 查询上传的发票相匹配的SAP发票的List
                        var matchList = (await sAPService.Query(x => x.IsDelete == false && invoiceNumbers.Contains(x.Reference) && companyCodes.Contains(x.Cocd))).Select(x=>new {x.Reference,x.Cocd }).Distinct().ToList();

                        var updateList=new List<Invoice>();

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
                            ////记录导入数量
                            data.Response = invoices.Count;
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
           var expressions= Expressionable.Create<Invoice>()
                .And(it => it.IsDelete == false)
                .AndIF(!string.IsNullOrEmpty(model.CompanyCode), it => it.CompanyCode==model.CompanyCode)
                .AndIF(!string.IsNullOrEmpty(model.InvoiceNumber),it => it.InvoiceNumber.Contains(model.InvoiceNumber)).ToExpression();

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

        public async Task<MessageModel<string>> SaveInvoice([FromBody]Invoice model)
        {
            var data = new MessageModel<string>();

            var flag = data.Success = await invoiceService.Update(model,new List<string>() {"InvoiceNumber", "InvoiceDate", "InvoiceDate", "Amount", "UpdatedBy", "UpdatedAt" });
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