using System;
using System.Collections.Generic;
using System.Text;

namespace BestPracticeWeb.WebApi.Model
{
    public class SiteAreaViewModel
    {
        public int ID { get; set; }

        public int ParentID { get; set; }

        public int? ChildrenID { get; set; }

        public string Value { get; set; }

        public int IsActive { get; set; }

        public string IsActiveName { get; set; }

        public string Type { get; set; }

        public string Icon { get; set; }

        public bool HasChildren => this.ChildrenID == null ? false : true;

    }
}
