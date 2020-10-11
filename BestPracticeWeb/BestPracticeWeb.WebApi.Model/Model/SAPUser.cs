using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestPracticeWeb.WebApi.Model
{
    [SugarTable("HR_EmployeeInfo")]
    public class SAPUser
    {
        public string PSAName { get; set; }

        public string NameCN { get; set; }

        public string EmailAddress { get; set; }

        public string WorkDayID { get; set; }
    }
}
