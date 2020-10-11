using System;


namespace BestPracticeWeb.WebApi.Model
{
    public class ProjectViewModel
    {
        public int ID { get; set; }

        public string ProjectTitle { get; set; }

        public int SiteID { get; set; }

        public string Site { get; set; }

        public int DivisionID { get; set; }

        public string Division { get; set; }

        public int StationID { get; set; }

        public string Station { get; set; }

        public string Department { get; set; }

        public string FileName { get; set; }
        public string Attachment { get; set; }

        public string Description { get; set; }

        public int IsDelete { get; set; }

        public string IsDeleteString { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
