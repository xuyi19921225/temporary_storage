using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class UserRoleRepository : BaseRepository<UserRoleMapping>, IUserRoleRepository
    {
        public UserRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// 查询用户信息（分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageModel<UserRoleViewModel>> UserRoleMaps(UserRequestModel model)
        {
            int totolcount = 0;

            List<UserRoleViewModel> list = await Task.Run(() => Db.Queryable<User, UserRoleMapping, Role>((a1, a2, a3) => new object[]
            {
                JoinType.Left,a1.Id==a2.UserID,
                JoinType.Left,a2.RoleID==a3.Id&&a3.IsDelete==false

            })
             .Where((a1)=> a1.IsDelete == false)
             .WhereIF(model.NTID != null, (a1, a2, a3) =>a1.NTID .Contains(model.NTID))
             .Select((a1, a2, a3) => new UserRoleViewModel
             {
                 Id = a1.Id,
                 NTID = a1.NTID,
                 UserName = a1.UserName,
                 IsActive = a1.IsActive,
                 IsDelete = a1.IsDelete,
                 Email = a1.Email,
                 RoleID = a3.Id,
                 RoleName = a3.RoleName,
                 RoleCode = a3.RoleCode
             })
             .ToPageList(model.PageIndex, model.PageSize, ref totolcount));

            PageModel<UserRoleViewModel> pageModel = new PageModel<UserRoleViewModel>()
            {
                TotalCount = totolcount,
                List = list,
                PageIndex = model.PageIndex,
                PageSize = model.PageSize
            };

            return pageModel;
        }
    }
}
