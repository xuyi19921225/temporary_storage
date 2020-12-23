using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class VendorRequestModel: PageEntity
    {
        public string CompanyCode { get; set; }

        public string VendorCode { get; set; }
    }
}
