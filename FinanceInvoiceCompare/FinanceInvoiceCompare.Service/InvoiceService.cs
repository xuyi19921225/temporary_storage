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

        public async Task<PageModel<UMatchInvoiceReportViewModel>> GetMatchInvoiceReport(MatchInvoiceReportRequestModel model) 
        {
            return await invoiceRepository.GetMatchInvoiceReport(model);
        }

        public async Task<PageModel<UMatchInvoiceReportViewModel>> GetUnMatchInvoiceReport(MatchInvoiceReportRequestModel model)
        {
            return await invoiceRepository.GetUnMatchInvoiceReport(model);
        }

        public async Task<PageModel<UMatchInvoiceReportViewModel>> GetCompareMatchInvoiceReport(MatchInvoiceReportRequestModel model)
        {
            return await invoiceRepository.GetCompareMatchInvoiceReport(model);
        }

        public async Task<List<UMatchInvoiceReportViewModel>> GetAllCompareMatchInvoiceReport(MatchInvoiceReportRequestModel model)
        {
            return await invoiceRepository.GetAllCompareMatchInvoiceReport(model);
        }
    }
}
