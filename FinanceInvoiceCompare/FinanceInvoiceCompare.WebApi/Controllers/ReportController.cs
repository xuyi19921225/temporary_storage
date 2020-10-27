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
    /// 报表操作类
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IInvoiceService invoiceService;

        public ReportController( IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }


        /// <summary>
        /// 获取发票Match信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetMatchInvoiceRepost")]
        public async Task<MessageModel<PageModel<UMatchInvoiceReportViewModel>>> GetMatchInvoiceRepost([FromQuery] MatchInvoiceReportRequestModel model)
        {
            return new MessageModel<PageModel<UMatchInvoiceReportViewModel>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await invoiceService.GetMatchInvoiceRepost(model)
            };
        }
    }
}