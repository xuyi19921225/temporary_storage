using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public InvoiceController(ISAPService sAPService,IInvoiceService invoiceService)
        {
            this.sAPService = sAPService;
            this.invoiceService = invoiceService;
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
                Response = await sAPService.QueryPage(a => a.IsDelete == false, model.PageIndex, model.PageSize, " ID desc ")
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
        public async Task<MessageModel<string>> AddSiteInvoice([FromBody] List<Invoice> invoices)
        {
            var data = new MessageModel<string>();

            if (invoices.Count > 0)
            {

                var invoiceNos = invoices.Select(x => x.InvoiceNumber).ToArray();

                //// 检查是否存在相同的发票号
                var exsitsVendors = (await invoiceService.Query(x => x.IsDelete == false && invoiceNos.Contains(x.InvoiceNumber)));

                bool flag = false;

                if (exsitsVendors.Count > 0)
                {
                    data.Success = flag;
                    data.Message = "存在相同的InvoiceNumber，请检查";
                }
                else
                {
                    data.Success = flag = await invoiceService.Add(invoices) > 0;

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
        /// 获取Site发票信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetSiteInvoiceList")]
        public async Task<MessageModel<PageModel<Invoice>>> GetSiteInvoiceList([FromQuery] SiteInvoiceRequestModel model)
        {
            return new MessageModel<PageModel<Invoice>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await invoiceService.QueryPage(a => a.IsDelete == false, model.PageIndex, model.PageSize, " ID desc ")
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

            var flag = data.Success = await invoiceService.Update(model);
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