using System;
using System.ComponentModel.DataAnnotations;

namespace BestPracticeWeb.WebApi.Model
{
    public  class ProjectHistory
    {
        public int ID { get; set; }

        public int ParentID { get; set; }

        public string Action { get; set; }

        public DateTime CreateAt { get; set; }

        public string CreateBy { get; set; }
    }
}
