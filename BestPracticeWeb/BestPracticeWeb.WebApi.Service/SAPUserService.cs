using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestPracticeWeb.WebApi.Service
{
    public class SAPUserService: BaseServices<SAPUser>, ISAPUserService
    {
        private readonly ISAPUserRepository sapUserRepository;
        public SAPUserService(ISAPUserRepository sapUserRepository)
        {
            this.sapUserRepository = sapUserRepository;
            base.BaseDal = sapUserRepository;
        }
    }
}
