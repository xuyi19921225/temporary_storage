using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class UserRoleViewModel:RootEntity
    {
        public string NTID { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public int? RoleID { get; set; }

        public string RoleName { get; set; }

        public string RoleCode { get; set; }
    }
}
