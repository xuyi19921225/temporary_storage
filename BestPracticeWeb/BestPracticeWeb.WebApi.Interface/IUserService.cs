using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.Model;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.IService
{
    public interface IUserService:IBaseServices<User>
    {
        Task<UserRolePermissionsInfoViewModel> GetUserRoleMenuInfo(string ntid,int siteID);

        Task<PageModel<UserViewModel>> GetUserList(UserRequestModel model);

    }
}
