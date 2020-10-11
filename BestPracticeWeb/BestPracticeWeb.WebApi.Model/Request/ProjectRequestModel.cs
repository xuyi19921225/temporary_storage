using System.ComponentModel.DataAnnotations;

namespace BestPracticeWeb.WebApi.Model
{
    public class ProjectRequestModel
    {
        public int? SiteID { get; set; }

        public int? DivisionID { get; set; }

        public int? StationID { get; set; }

        public int? IsDelete { get; set; }

        [Required(ErrorMessage = "页码不能为空")]
        public int PageIndex { get; set; }

        [Required(ErrorMessage = "每页数量不能为空")]
        public int PageSize { get; set; }
    }
}
