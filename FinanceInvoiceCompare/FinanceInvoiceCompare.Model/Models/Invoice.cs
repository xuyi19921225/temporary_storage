using SqlSugar;
using System;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class Invoice:RootEntity
    {
        public string CompanyCode { get; set; }
        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal Amount { get; set; }

        [SugarColumn(IsOnlyIgnoreUpdate = true)]
        public DateTime EntryDate { get; set; } = DateTime.Now;

        public string DataSource { get; set; }

        public DateTime? MatchDate { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string IsMatch { get; set; }

        public bool IsDelete { get; set; } = false;
    }
}
