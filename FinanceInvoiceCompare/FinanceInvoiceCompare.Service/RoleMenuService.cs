using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Service
{
    public class RoleMenuService : BaseServices<RoleMenuMapping>, IRoleMenuService
    {
        private readonly IRoleMenuRepository dal;

        public RoleMenuService(IRoleMenuRepository dal)
        {
            base.BaseDal = dal;
            this.dal = dal;
        }


        public async Task<List<Menu>> GetRMenuList()
        {
            return await dal.GetRMenuList();
        }
    }
}
