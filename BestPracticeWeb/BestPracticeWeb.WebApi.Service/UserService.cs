using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.Service
{
    public class UserService : BaseServices<User>, IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMenuRepository menuRepository;

        public UserService(IUserRepository userRepository, IMenuRepository menuRepository)
        {
            this.userRepository = userRepository;
            this.menuRepository = menuRepository;
            base.BaseDal = userRepository;
        }

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="model">用户查询实体</param>
        /// <returns></returns>
        public async Task<PageModel<UserViewModel>> GetUserList(UserRequestModel model)
        {
            return await userRepository.QueryUserListMulti(model);
        }

        /// <summary>
        /// 获取登录用户的权限信息
        /// </summary>
        /// <param name="ntid"></param>
        /// <returns></returns>
        public async Task<UserRolePermissionsInfoViewModel> GetUserRoleMenuInfo(string ntid,int siteID)
        {
            ////获取用户信息
            UserViewModel user = await userRepository.QueryUserMulti(ntid, siteID);
            ////获取菜单信息
            List<MenuViewModel> menuList;

            UserRolePermissionsInfoViewModel model;

            if (user != null)
            {
                menuList = await menuRepository.QueryMenuListMulti(user.RoleID);
                model = new UserRolePermissionsInfoViewModel()
                {
                    ID = user.ID,
                    DivisionID = user.DivisionID,
                    SiteID = user.SiteID,
                    RoleName = user.RoleName,
                    Department = user.Department,
                    UserName = user.UserName,
                    NTID = user.NTID,
                    MenuList = menuList
                };
            }
            else 
            {
                model = new UserRolePermissionsInfoViewModel()
                {
                    ID = 0,
                    DivisionID = 0,
                    SiteID = siteID,
                    RoleName = "",
                    Department = "",
                    UserName = "AnonymousUser",
                    NTID = ntid,
                    MenuList = new List<MenuViewModel>()
                };
            }

            return model;
        }
    }
}
