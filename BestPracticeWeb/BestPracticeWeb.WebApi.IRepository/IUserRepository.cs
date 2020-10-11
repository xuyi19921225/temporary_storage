using BestPracticeWeb.WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.IRepository
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<UserViewModel> QueryUserMulti(string ntid,int siteId);

        Task<PageModel<UserViewModel>> QueryUserListMulti(UserRequestModel model);
    }
}
