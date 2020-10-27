using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class SAPRequestModel: PageEntity
    {
        public string InvoiceNumber { get; set; }
    }
}
