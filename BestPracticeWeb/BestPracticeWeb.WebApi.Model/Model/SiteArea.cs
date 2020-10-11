using System;
using System.Collections.Generic;
using System.Text;

namespace BestPracticeWeb.WebApi.Model
{
    public class SiteArea : OperateBaseModel
    {
        public int ID { get; set; }

        public int ParentID { get; set; }

        public string Value { get; set; }

        public string Icon { get; set; }

        public string Type { get; set; }

        public int IsActive { get; set; }
    }
}
