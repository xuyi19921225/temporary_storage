using System;
using System.Collections.Generic;
using System.Text;

namespace BestPracticeWeb.WebApi.Model
{
    public class Project:OperateBaseModel
    {
        public int ID { get; set; }

        public string ProjectTitle { get; set; }

        public int SiteID { get; set; }

        public int DivisionID { get; set; }

        public int StationID { get; set; }

        public string Department { get; set; }

        public string FileName { get; set; }

        public string Attachment { get; set; }

        public string Description { get; set; }

        public int IsDelete { get; set; }
    }
}
