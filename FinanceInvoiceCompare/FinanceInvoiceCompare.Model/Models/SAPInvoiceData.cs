using System;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class SAPInvoiceData:RootEntity
    {
        public string Cocd { get; set; }

        public string Document { get; set; }

        public DateTime NetDueDT { get; set; }

        public DateTime PstngDate { get; set; }

        public DateTime DocDate { get; set; }

        public string Curr { get; set; }

        public float AmountInDC { get; set; }

        public string PBk { get; set; }

        public string Text { get; set; }

        public DateTime BlineDate { get; set; }

        public float AmtLC2 { get; set; }

        public string Assign { get; set; }

        public int GL { get; set; }

        public string ClrngDoc { get; set; }

        public bool IsDelete { get; set; }

        public char IsMatch { get; set; }

        public int Check { get; set; }

        public string Remark { get; set; }
    }
}
