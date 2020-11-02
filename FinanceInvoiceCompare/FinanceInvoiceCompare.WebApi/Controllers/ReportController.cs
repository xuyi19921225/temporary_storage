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
        private readonly IPaymentService paymentService;

        public ReportController(IInvoiceService invoiceService, IPaymentService paymentService)
        {
            this.invoiceService = invoiceService;
            this.paymentService = paymentService;
        }


        /// <summary>
        /// 获取发票Match信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetMatchInvoiceReport")]
        public async Task<MessageModel<PageModel<UMatchInvoiceReportViewModel>>> GetMatchInvoiceReport([FromQuery] MatchInvoiceReportRequestModel model)
        {
            return new MessageModel<PageModel<UMatchInvoiceReportViewModel>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await invoiceService.GetMatchInvoiceReport(model)
            };
        }

        /// <summary>
        /// 获取未匹配的发票Match信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetUnMatchInvoiceReport")]
        public async Task<MessageModel<PageModel<UMatchInvoiceReportViewModel>>> GetUnMatchInvoiceReport([FromQuery]MatchInvoiceReportRequestModel model)
        {
            return new MessageModel<PageModel<UMatchInvoiceReportViewModel>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await invoiceService.GetUnMatchInvoiceReport(model)
            };
        }


        /// <summary>
        /// 获取发票比对信息(分页)
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetCompareMatchInvoiceReport")]
        public async Task<MessageModel<PageModel<UMatchInvoiceReportViewModel>>> GetCompareMatchInvoiceReport([FromQuery] MatchInvoiceReportRequestModel model)
        {
            return new MessageModel<PageModel<UMatchInvoiceReportViewModel>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await invoiceService.GetCompareMatchInvoiceReport(model)
            };
        }

        /// <summary>
        /// 获取发票比对信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetAllCompareMatchInvoiceReport")]
        public async Task<MessageModel<List<UMatchInvoiceReportViewModel>>> GetAllCompareMatchInvoiceReport([FromQuery] MatchInvoiceReportRequestModel model)
        {
            return new MessageModel<List<UMatchInvoiceReportViewModel>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await invoiceService.GetAllCompareMatchInvoiceReport(model)
            };
        }


        /// <summary>
        /// 添加付款信息
        /// </summary>
        /// <param name="payments">付款信息</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post([FromBody] List<Payment> payments)
        {
            var data = new MessageModel<string>();

            if (payments.Count > 0)
            {

                var flag = data.Success = await paymentService.Add(payments) > 0;

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
        /// 获取付款发票信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetPaymentInvoiceReport")]
        public async Task<MessageModel<PageModel<Payment>>> GetPaymentInvoiceReport([FromQuery] MatchInvoiceReportRequestModel model)
        {
            return new MessageModel<PageModel<Payment>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await invoiceService.GetInvoicePaymentReport(model)
            };
        }



        /// <summary>
        /// 获取所有付款发票信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetAllPaymentInvoiceReport")]
        public async Task<MessageModel<List<Payment>>> GetAllPaymentInvoiceReport([FromQuery] MatchInvoiceReportRequestModel model)
        {
            return new MessageModel<List<Payment>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await invoiceService.GetAllInvoicePaymentReport(model)
            };
        }

    }
}