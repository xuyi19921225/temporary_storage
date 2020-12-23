using FinanceInvoiceCompare.WebApi.IService.BASE;
using FinanceInvoiceCompare.WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.IService
{
    public interface ISAPService : IBaseServices<SAPInvoiceData>
    {
        Task<PageModel<SAPInvoiceData>> GetSAPInvoiceList(SAPRequestModel model);

        Task<List<SAPInvoiceData>> GetAllSAPInvoiceList(SAPRequestModel model);
    }
}
