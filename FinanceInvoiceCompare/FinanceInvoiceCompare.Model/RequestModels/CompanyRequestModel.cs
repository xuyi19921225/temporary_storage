using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class CompanyRequestModel:PageEntity
    {
        public string CompanyCode { get; set; }

        public string CompanyName { get; set; }
    }
}
