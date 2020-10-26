using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;

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
    }
}
