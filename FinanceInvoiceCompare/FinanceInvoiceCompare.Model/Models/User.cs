namespace FinanceInvoiceCompare.WebApi.Model
{
    public class User:RootEntity
    {
        public string NTID { get; set; }

        public string UserName  { get; set; }

        public string Email { get; set; }

        public int IsActive { get; set; }

        public int IsDelete { get; set; }
    }
}
