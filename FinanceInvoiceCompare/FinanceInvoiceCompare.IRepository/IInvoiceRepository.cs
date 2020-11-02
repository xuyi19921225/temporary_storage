using FinanceInvoiceCompare.WebApi.IRepository.Base;
using FinanceInvoiceCompare.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.IRepository
{
    public interface IInvoiceRepository: IBaseRepository<Invoice>
    {
        Task<PageModel<UMatchInvoiceReportViewModel>> GetMatchInvoiceReport(MatchInvoiceReportRequestModel model);

        Task<PageModel<UMatchInvoiceReportViewModel>> GetUnMatchInvoiceReport(MatchInvoiceReportRequestModel model);

        Task<PageModel<UMatchInvoiceReportViewModel>> GetCompareMatchInvoiceReport(MatchInvoiceReportRequestModel model);

        Task<List<UMatchInvoiceReportViewModel>> GetAllCompareMatchInvoiceReport(MatchInvoiceReportRequestModel model);

        Task<PageModel<Payment>> GetInvoicePaymentReport(MatchInvoiceReportRequestModel model);

        Task<List<Payment>> GetAllInvoicePaymentReport(MatchInvoiceReportRequestModel model);
    }
}
