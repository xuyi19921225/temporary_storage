using SqlSugar;
using System;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class RootEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(IsOnlyIgnoreUpdate =true)]
        public DateTime CreateAt { get; set; } = DateTime.Now;

        [SugarColumn(IsOnlyIgnoreUpdate = true)]
        public string CreateBy { get; set; }

        [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true)]
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        [SugarColumn(IsNullable = true, IsOnlyIgnoreInsert = true)]
        public string UpdatedBy { get; set; }

    }
}
