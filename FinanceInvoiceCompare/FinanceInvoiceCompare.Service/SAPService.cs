using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;

namespace FinanceInvoiceCompare.WebApi.Service
{
    public class SAPService:BaseServices<SAPInvoiceData>, ISAPService
    {
        private readonly ISAPRepository sapRepository;

        public SAPService(ISAPRepository sapRepository )
        {
            this.sapRepository = sapRepository;
            base.BaseDal = sapRepository;
        }
    }
}
