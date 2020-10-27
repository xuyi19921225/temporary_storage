using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;
using System.Threading.Tasks;

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

        public async Task<PageModel<SAPInvoiceData>> GetSAPInvoiceList(SAPRequestModel model) 
        {
            return await sapRepository.GetSAPInvoiceList(model);
        }
    }
}
