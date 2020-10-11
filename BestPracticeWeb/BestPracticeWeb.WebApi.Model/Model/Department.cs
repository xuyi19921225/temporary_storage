using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestPracticeWeb.WebApi.Model
{
    public class Department
    {
        public int ID { get; set; }

        [SugarColumn(ColumnName = "Department")]
        public string Department1 { get; set; }

        public DateTime CreateAt { get; set; }

        public string CreateBy { get; set; }
    }
}
