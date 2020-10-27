using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;
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

        public async Task<PageModel<UMatchInvoiceReportViewModel>> GetMatchInvoiceRepost(MatchInvoiceReportRequestModel model) 
        {
            return await invoiceRepository.GetMatchInvoiceRepost(model);
        }
    }
}
