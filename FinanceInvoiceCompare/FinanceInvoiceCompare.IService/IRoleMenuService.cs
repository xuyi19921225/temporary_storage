using FinanceInvoiceCompare.WebApi.IService.BASE;
using FinanceInvoiceCompare.WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.IService
{
    public interface IRoleMenuService: IBaseServices<RoleMenuMapping>
    {
        Task<List<Menu>> GetMenusByRoleID(int roleID)
    }
}
