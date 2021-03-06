﻿using SqlSugar;
using System.Collections.Generic;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class User:RootEntity
    {
        public string NTID { get; set; }

        public string UserName  { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; } = false;

        [SugarColumn(IsIgnore = true)]
        public int RID { get; set; }
       
    }
}
