using FinanceInvoiceCompare.WebApi.IService.BASE;
using FinanceInvoiceCompare.WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.IService
{
    public interface IInvoiceService : IBaseServices<Invoice>
    {
        Task<PageModel<UMatchInvoiceReportViewModel>> GetMatchInvoiceReport(MatchInvoiceReportRequestModel model);

        Task<PageModel<UMatchInvoiceReportViewModel>> GetUnMatchInvoiceReport(MatchInvoiceReportRequestModel model);

        Task<PageModel<UMatchInvoiceReportViewModel>> GetCompareMatchInvoiceReport(MatchInvoiceReportRequestModel model);

        Task<List<UMatchInvoiceReportViewModel>> GetAllCompareMatchInvoiceReport(MatchInvoiceReportRequestModel model);

        Task<PageModel<Payment>> GetInvoicePaymentReport(MatchInvoiceReportRequestModel model);

        Task<List<Payment>> GetAllInvoicePaymentReport(MatchInvoiceReportRequestModel model);

        Task<string> MatchInvoice(string proc, List<SqlSugar.SugarParameter> parameters = null);
    }
}
