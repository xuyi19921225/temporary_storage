namespace FinanceInvoiceCompare.WebApi.Model
{
    public class Vendor:RootEntity
    {
        public string CompanyCode { get; set; }

        public string VendorCode { get; set; }

        public string VendorChName { get; set; }

        public bool IsDelete { get; set; } = false;


    }
}
