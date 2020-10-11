using System;

namespace BestPracticeWeb.WebApi.Model
{
    public class ProjectSaveRequestModel
    {
        public int? ID { get; set; }
        public int? SiteID { get; set; }

        public int? DivisionID { get; set; }

        public int? StationID { get; set; }

        public string Department { get; set; }

        public string ProjectTitle { get; set; }

        public string FileName { get; set; }

        public string Attachment { get; set; }

        public string Description { get; set; }

        public DateTime CreateAt { get; set; }

        public string CreateBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
