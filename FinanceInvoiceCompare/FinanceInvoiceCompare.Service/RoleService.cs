using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;

namespace FinanceInvoiceCompare.WebApi.Service
{
    public  class RoleService: BaseServices<Role>, IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
            base.BaseDal = roleRepository;
        }
    }
}
