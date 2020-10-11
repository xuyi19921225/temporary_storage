using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.Model;
using Microsoft.Extensions.Options;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IOptions<ConnectionStringsOptions> options) : base(options.Value.SQLConnection.BestPracticeWeb)
        {


        }


        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="ntid"></param>
        /// <returns></returns>
        public async Task<UserViewModel> QueryUserMulti(string ntid,int siteID)
        {
            return await Db.Queryable<User, Role>((a1, b2) => new object[]
             {
                 JoinType.Left,a1.RoleID==b2.ID
             })
            .Where((a1) => a1.NTID == ntid&&a1.SiteID==siteID)
            .Select<UserViewModel>()
            .FirstAsync();
        }

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageModel<UserViewModel>> QueryUserListMulti(UserRequestModel model)
        {
            int totolcount = 0;


            List<UserViewModel> list = await Task.Run(() => Db.Queryable<User, Role, SiteArea, SiteArea>((a1, b2, c3, d4) => new object[]
                {
                JoinType.Left,a1.RoleID==b2.ID,
                JoinType.Left,a1.DivisionID==c3.ID,
                JoinType.Left,a1.SiteID==d4.ID
                })
             .WhereIF(!string.IsNullOrEmpty(model.NTID), a1 => a1.NTID == model.NTID)
             .WhereIF(!string.IsNullOrEmpty(model.JobNumber), a1 => a1.JobNumber == model.JobNumber)
             .WhereIF(model.DivisionID != null, a1 => a1.DivisionID == model.DivisionID)
             .WhereIF(model.SiteID != null, a1 => a1.SiteID == model.SiteID)
             .WhereIF(!string.IsNullOrEmpty(model.Department1), a1 => a1.Department == model.Department1)
             .Select((a1, b2, c3, d4) => new UserViewModel
             {
                 ID = a1.ID,
                 NTID = a1.NTID,
                 Division = c3.Value,
                 DivisionID = a1.DivisionID,
                 Site = d4.Value,
                 SiteID = a1.SiteID,
                 Department = a1.Department,
                 JobNumber = a1.JobNumber,
                 UserName = a1.UserName,
                 Email = a1.Email,
                 SpecialPlane = a1.SpecialPlane,
                 RoleName = b2.RoleName,
                 RoleID = a1.RoleID,
                 CreateAt = a1.CreateAt,
                 CreateBy = a1.CreateBy,
                 UpdatedAt = a1.UpdatedAt,
                 UpdatedBy = a1.UpdatedBy
             })
             .ToPageList(model.PageIndex, model.PageSize, ref totolcount));

            PageModel<UserViewModel> pageModel = new PageModel<UserViewModel>()
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
