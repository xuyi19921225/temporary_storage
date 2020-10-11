using System;
using System.Collections.Generic;
using System.Text;

namespace BestPracticeWeb.WebApi.Model
{
    public class SiteAreaRequestParameterModel
    {
        public int? ParentID { get; set; }

        public int? IsActive { get; set; }


        public string Type { get; set; }
    }
}
