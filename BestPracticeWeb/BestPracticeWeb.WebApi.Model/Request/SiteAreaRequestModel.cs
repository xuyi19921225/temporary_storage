using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BestPracticeWeb.WebApi.Model
{
    public class SiteAreaRequestModel
    {
        public int? ParentID { get; set; }

        [Required(ErrorMessage = "页码不能为空")]
        public int PageIndex { get; set; }

        [Required(ErrorMessage = "每页数量不能为空")]
        public int PageSize { get; set; }
    }
}
