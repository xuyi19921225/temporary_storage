using System;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class PermissionMenuMapping
    {
        public int ID { get; set; }

        public int MenuID { get; set; }

        public int PermissionID { get; set; }

        public DateTime CreateAt { get; set; }

        public string CreateBy { get; set; }
    }
}
