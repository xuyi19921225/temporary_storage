using SqlSugar;
using System;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class UserRoleMapping
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int RoleID { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
