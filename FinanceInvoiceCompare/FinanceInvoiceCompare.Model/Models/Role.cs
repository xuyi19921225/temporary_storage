using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class Role
    {
        public string RoleName { get; set; }

        public string RoleCode { get; set; }

        public int IsActive { get; set; }

        public int IsDelete { get; set; }
    }
}
