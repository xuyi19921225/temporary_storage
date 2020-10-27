using FinanceInvoiceCompare.WebApi.IService.BASE;
using FinanceInvoiceCompare.WebApi.Model;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.IService
{
    public interface IInvoiceService : IBaseServices<Invoice>
    {
        Task<PageModel<UMatchInvoiceReportViewModel>> GetMatchInvoiceRepost(MatchInvoiceReportRequestModel model);
    }
}
