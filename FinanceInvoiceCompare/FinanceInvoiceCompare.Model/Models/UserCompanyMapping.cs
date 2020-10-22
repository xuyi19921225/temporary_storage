using SqlSugar;
using System;
using System.Collections.Generic;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class UserCompanyMapping
    {
        [SugarColumn(IsNullable = true, IsPrimaryKey = true, IsIdentity = true, IsOnlyIgnoreInsert = true)]
        public int ID { get; set; }

        public int CompanyID { get; set; }

        public int UserID { get; set; }

        [SugarColumn(IsOnlyIgnoreUpdate =true)]
        public string CreateBy { get; set; }

        [SugarColumn(IsOnlyIgnoreUpdate = true)]
        public DateTime CreateAt { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<Company> list { get; set; }
    }
}
