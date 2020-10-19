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

        /// <summary>
        /// 查询出角色-菜单全部Map属性数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleMenuMapping>> GetRMMaps()
        {
            return await dal.GetRMMaps();
        }
    }
}
