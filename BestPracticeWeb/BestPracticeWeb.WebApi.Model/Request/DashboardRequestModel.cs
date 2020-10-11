using System.ComponentModel.DataAnnotations;

namespace BestPracticeWeb.WebApi.Model
{
    public class DashboardRequestModel
    {
        public int? SiteID { get; set; }

        public int? DivisionID { get; set; }

        public int? StationID { get; set; }

        public string ProjectTitle { get; set; }

        public string Department { get; set; }

        [Required(ErrorMessage = "页码不能为空")]
        public int PageIndex { get; set; }

        [Required(ErrorMessage = "每页数量不能为空")]
        public int PageSize { get; set; }
    }
}
