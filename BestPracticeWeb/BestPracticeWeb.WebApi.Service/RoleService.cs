using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BestPracticeWeb.WebApi.Service
{
   public  class RoleService:BaseServices<Role>,IRoleService
    {
        private readonly IRoleRepository roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
            base.BaseDal = roleRepository;
        }
    }
}
