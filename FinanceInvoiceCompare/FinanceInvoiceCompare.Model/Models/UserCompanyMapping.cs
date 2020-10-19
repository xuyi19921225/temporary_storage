using System;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class UserCompanyMapping
    {
        public int ID { get; set; }

        public int CompanyID { get; set; }

        public int UserID { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
