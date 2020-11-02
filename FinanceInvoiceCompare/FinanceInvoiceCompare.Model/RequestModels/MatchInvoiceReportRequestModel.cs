using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class MatchInvoiceReportRequestModel : PageEntity
    {
        public string InvoiceNumber { get; set; }

        public string Compare { get; set; }
        public string MultipleCompany{ get; set; }

        public string[] CompanyCodeList => MultipleCompany?.Split(",");

    }
}
