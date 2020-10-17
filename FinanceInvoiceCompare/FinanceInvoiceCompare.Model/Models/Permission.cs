namespace FinanceInvoiceCompare.WebApi.Model
{
    public class Permission:RootEntity
    {
        public string PermissionType { get; set; }

        public bool IsDelete { get; set; }
    }
}
