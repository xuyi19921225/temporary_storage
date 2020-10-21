using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Service
{
    public class UserRoleService:BaseServices<UserRoleMapping>, IUserRoleService
    {
        private readonly IUserRoleRepository userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
            base.BaseDal = userRoleRepository;
        }

        public async Task<PageModel<UserRoleViewModel>> UserRoleMaps(UserRequestModel model)
        {
            return await userRoleRepository.UserRoleMaps(model);
        }
    }
}
