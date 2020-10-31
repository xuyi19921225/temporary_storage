using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class Payment : RootEntity
    {
        public string Cocd { get; set; }

        public string Vendor { get; set; }

        public string Reference { get; set; }

        public string DocumentNo { get; set; }


        [SugarColumn(SerializeDateTimeFormat = "yyyy-MM-dd")]
        public DateTime NetDueDT { get; set; }

        //public DateTime _NetDueDT { get; set; }
        //public DateTime NetDueDT { get => _NetDueDT; set => _NetDueDT = DateTime.Parse(value.ToString("yyyy-MM-dd")); }


        [SugarColumn(SerializeDateTimeFormat = "yyyy-MM-dd")]
        public DateTime PstngDate { get; set; }


        [SugarColumn(SerializeDateTimeFormat = "yyyy-MM-dd")]
        public DateTime DocDate { get; set; }

        public string Curr { get; set; }

        public float DCAmount { get; set; }

        public string PBk { get; set; }

        public string Type { get; set; }

        public bool IsDelete { get; set; } = false;

        [SugarColumn(IsIgnore =true, SerializeDateTimeFormat = "yyyy-MM-dd")]
         public DateTime MatchDate { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string RecivedStatus { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string VendorChName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public int Day { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string BlockStatus { get; set; }


    }
}
