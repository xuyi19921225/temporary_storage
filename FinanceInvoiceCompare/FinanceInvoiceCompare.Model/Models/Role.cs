namespace FinanceInvoiceCompare.WebApi.Model
{
    public class Role:RootEntity
    {
        public string RoleName { get; set; }

        public string RoleCode { get; set; }

        public bool IsDelete { get; set; } = false;
    }
}
