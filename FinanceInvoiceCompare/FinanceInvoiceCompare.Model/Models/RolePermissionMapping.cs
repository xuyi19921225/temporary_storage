using System;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class RolePermissionMapping
    {
        public int ID { get; set; }

        public int RoleID { get; set; }

        public int PermissionID { get; set; }

        public DateTime CreateAt { get; set; }

        public string CreateBy { get; set; }
    }
}
