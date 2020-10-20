namespace FinanceInvoiceCompare.WebApi.Model
{
    public class Menu:RootEntity
    {
        public int ParentID { get; set; }

        public string MenuName { get; set; }

        public string Icon { get; set; }

        public string Path { get; set; }

        public string Redirect { get; set; }

        public string Component { get; set; }

        public string Title { get; set; }

        public bool AlwaysShow { get; set; }

        public bool Hidden { get; set; }

        public bool HasChildren { get; set; }

        public bool IsDelete { get; set; }
    }
}
