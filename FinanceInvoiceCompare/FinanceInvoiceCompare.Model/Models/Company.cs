using SqlSugar;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public  class Company:RootEntity
    {
        public string Code { get; set; }

        public string CompanyName { get; set; }

        public bool IsDelete { get; set; }


        [SugarColumn(IsIgnore = true)]
        public int UId { get; set; }
    }
}
