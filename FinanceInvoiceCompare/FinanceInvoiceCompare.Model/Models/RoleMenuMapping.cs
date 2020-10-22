using SqlSugar;
using System;
using System.Collections.Generic;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class RoleMenuMapping
    {
        public int ID { get; set; }

        public int RoleID { get; set; }

        public int MenuID { get; set; }

        [SugarColumn(IsOnlyIgnoreUpdate = true)]
        public DateTime CreateAt { get; set; }

        [SugarColumn(IsOnlyIgnoreUpdate = true)]
        public string CreateBy { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<Menu> list { get; set; }
    }
}
