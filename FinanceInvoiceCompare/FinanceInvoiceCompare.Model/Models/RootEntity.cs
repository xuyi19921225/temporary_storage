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

        public DateTime CreateAt { get; set; }

        public string CreateBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

    }
}
