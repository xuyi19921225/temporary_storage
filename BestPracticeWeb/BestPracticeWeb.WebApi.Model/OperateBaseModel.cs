using Newtonsoft.Json;
using SqlSugar;
using System;

namespace BestPracticeWeb.WebApi.Model
{
    public class OperateBaseModel
    {
        public DateTime CreateAt { get; set; }

        public string CreateBy { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? UpdatedAt { get; set; }

        [SugarColumn(IsNullable = true)]
        public string UpdatedBy { get; set; }
    }
}
