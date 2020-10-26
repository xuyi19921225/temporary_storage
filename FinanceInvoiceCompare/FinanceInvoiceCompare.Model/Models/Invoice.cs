using System;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class Invoice:RootEntity
    {
        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public float Amount { get; set; }

        public DateTime EntryDate { get; set; } = DateTime.Now;

        public string DataSource { get; set; }

        public DateTime MatchDate { get; set; }

        public char IsMatch { get; set; } = 'N';

        public bool IsDelete { get; set; } = false;
    }
}
