using Newtonsoft.Json;
using SqlSugar;
using System.Collections.Generic;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class Menu : RootEntity
    {
        public int ParentID { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Redirect { get; set; }

        public string Component { get; set; }

        public bool AlwaysShow { get; set; }

        public bool Hidden { get; set; }

        public bool IsDelete { get; set; } = false;

        public string Title { get; set; }

        public string Icon { get; set; }


        [SugarColumn(IsIgnore = true)]
        public Meta Meta => new Meta() { Title = this.Title, Icon = this.Icon };

        [SugarColumn(IsIgnore = true)]
        public int RId { get; set; }

        [SugarColumn(IsIgnore = true)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Menu> Children { get; set; }
    }

    public class Meta
    {
        public string Title { get; set; }

        public string Icon { get; set; }
    }
}
