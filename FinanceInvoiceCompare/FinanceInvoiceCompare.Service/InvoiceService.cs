using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Service
{
    public class InvoiceService:BaseServices<Invoice>, IInvoiceService
    {
        private readonly IInvoiceRepository invoiceRepository;
        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
            base.BaseDal = invoiceRepository;
        }

        /// <summary>
        /// 获取匹配的发票信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageModel<UMatchInvoiceReportViewModel>> GetMatchInvoiceReport(MatchInvoiceReportRequestModel model) 
        {
            return await invoiceRepository.GetMatchInvoiceReport(model);
        }

        /// <summary>
        /// 获取未匹配的发票信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageModel<UMatchInvoiceReportViewModel>> GetUnMatchInvoiceReport(MatchInvoiceReportRequestModel model)
        {
            return await invoiceRepository.GetUnMatchInvoiceReport(model);
        }

        /// <summary>
        /// 获取发票信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public async Task<PageModel<UMatchInvoiceReportViewModel>> GetCompareMatchInvoiceReport(MatchInvoiceReportRequestModel model)
        {
            return await invoiceRepository.GetCompareMatchInvoiceReport(model);
        }

        /// <summary>
        /// 获取所有的发票信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<UMatchInvoiceReportViewModel>> GetAllCompareMatchInvoiceReport(MatchInvoiceReportRequestModel model)
        {
            return await invoiceRepository.GetAllCompareMatchInvoiceReport(model);
        }

        /// <summary>
        /// 获取付款发票信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageModel<Payment>> GetInvoicePaymentReport(MatchInvoiceReportRequestModel model)
        {
           return  await invoiceRepository.GetInvoicePaymentReport(model);
        }


        /// <summary>
        /// 获取所有付款发票信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<Payment>> GetAllInvoicePaymentReport(MatchInvoiceReportRequestModel model) 
        {
            return await invoiceRepository.GetAllInvoicePaymentReport(model);
        }
    }
}
