﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class sysUserInfo
    {
        public int UserID { get; set; }

        public int RoleID { get; set; }
        public string UserName { get; set; }

        public string RoleName { get; set; }

        public List<Company> Company { get; set; }

    }

}
