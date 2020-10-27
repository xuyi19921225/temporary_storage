using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class SAPInvoiceData:RootEntity
    {
        [SugarColumn(ColumnName = "Cocd")]//别名
        public string Cocd { get; set; }

        public string Vendor { get; set; }



        public string Reference { get; set; }

        public string DocumentNo { get; set; }

        public DateTime  NetDueDT { get; set; }

        public DateTime PstngDate { get; set; }

        public DateTime DocDate { get; set; }

        public string Curr { get; set; }

        public float AmountInDC { get; set; }

        public string PBk { get; set; }

        public string Text { get; set; }

        public string Type { get; set; }

        public DateTime BlineDate { get; set; }

        public float AmtLC2 { get; set; }

        public string Assign { get; set; }

        public int GL { get; set; }

        public string ClrngDoc { get; set; }

        public bool IsDelete { get; set; }

        public string Remark { get; set; } = string.Empty;

        [SugarColumn(IsIgnore = true)]
        public string VendorChName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string IsMatch { get; set; }

        [SugarColumn(IsIgnore = true)]
        public int Check { get; set; }


    }
}
