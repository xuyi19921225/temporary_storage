using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class SAPRequestModel: PageEntity
    {
        public string InvoiceNumber { get; set; }

        public string CompanyCode { get; set; }

        public string IsMatch { get; set; }

        public int? Check { get; set; }

        public DateTime? PstingBeginDate { get; set; }

        public DateTime? PstingEndDate { get; set; }

        public DateTime? DocBeginDate { get; set; }

        public DateTime? DocEndDate { get; set; }
    }
}
