using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class TaxCode : RootEntity
    {
        public string Code { get; set; }

        public decimal Rate { get; set; }

        public string Remark { get; set; }

        public bool IsDelete { get; set; } = false;
    }
}
