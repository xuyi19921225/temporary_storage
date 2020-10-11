using System;
using System.Collections.Generic;
using System.Text;

namespace BestPracticeWeb.WebApi.Common
{
   public class ConnectionStringsOptions
    {
        public SQL SQLConnection { get; set; }
    }

    public class SQL 
    {
        public string BestPracticeWeb { get; set; }

        public string eFactory { get; set; }
    }
}
