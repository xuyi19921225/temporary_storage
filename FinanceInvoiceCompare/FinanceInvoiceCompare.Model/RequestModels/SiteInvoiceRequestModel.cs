using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
   public  class SiteInvoiceRequestModel:PageEntity
    {
        public string CompanyCode { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime? InvoiceBeginDate { get; set; }

        public DateTime? InvoiceEndDate { get; set; }
    }
}
