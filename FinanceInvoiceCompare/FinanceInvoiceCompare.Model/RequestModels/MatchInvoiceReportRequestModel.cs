using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class MatchInvoiceReportRequestModel:PageEntity
    {
        public string InvoiceNumber { get; set; }
        public List<Company> List { get; set; }

        public string[] CompanyCodeList => List.Select(x => x.Code).ToArray();

    }
}
