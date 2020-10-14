using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class User
    {
        public string NTID { get; set; }

        public string UserName  { get; set; }

        public string Email { get; set; }

        public int IsActive { get; set; }

        public int IsDelete { get; set; }
    }
}
