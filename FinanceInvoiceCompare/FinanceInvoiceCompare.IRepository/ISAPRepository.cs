using FinanceInvoiceCompare.WebApi.IRepository.Base;
using FinanceInvoiceCompare.WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.IRepository
{
    public interface ISAPRepository:IBaseRepository<SAPInvoiceData>
    {
        Task<PageModel<SAPInvoiceData>> GetSAPInvoiceList(SAPRequestModel model);
    }
}
