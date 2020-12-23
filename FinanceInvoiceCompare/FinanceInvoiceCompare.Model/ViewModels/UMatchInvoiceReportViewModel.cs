using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class UMatchInvoiceReportViewModel
    {
        public string CompanyCode { get; set; }
        public string InvoiceNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public float? Amount { get; set; }

        public DateTime? EntryDate { get; set; }

        public string DataSource { get; set; }

        public DateTime? MatchDate { get; set; }

        public string IsMatch { get; set; }

        public string Cocd { get; set; }

        public string Vendor { get; set; }


        public string Reference { get; set; }

        public string DocumentNo { get; set; }

        public DateTime? NetDueDT { get; set; }

        public DateTime? PstngDate { get; set; }

        public DateTime? DocDate { get; set; }

        public string Curr { get; set; }

        public float? AmountInDC { get; set; }

        public string PBk { get; set; }

        public string Text { get; set; }

        public string Type { get; set; }

        public DateTime? BlineDate { get; set; }

        public float? AmtLC2 { get; set; }

        public string Assign { get; set; }

        public int? GL { get; set; }

        public string ClrngDoc { get; set; }

        public string VendorChName { get; set; }


        public float? Check { get; set; }
    }
}
