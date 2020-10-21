using FinanceInvoiceCompare.WebApi.IService.BASE;
using FinanceInvoiceCompare.WebApi.Model;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.IService
{
    public interface IUserRoleService:IBaseServices<UserRoleMapping>
    {
        Task<PageModel<UserRoleViewModel>> UserRoleMaps(UserRequestModel model);
    }
}
