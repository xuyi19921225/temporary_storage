using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.Model;
using Microsoft.Extensions.Options;
using SqlSugar;

namespace BestPracticeWeb.WebApi.Repository
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {

        public MenuRepository(IOptions<ConnectionStringsOptions> options) : base(options.Value.SQLConnection.BestPracticeWeb)
        {

        }

        /// <summary>
        /// 根据角色获取菜单信息
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public async Task<List<MenuViewModel>> QueryMenuListMulti(int roleID)
        {
            return await Db.Queryable<RoleMenuMapping, Menu>((a1, b2) => new object[]
            {
                JoinType.Inner,a1.MenuID==b2.ID,

            })
            .Where((a1) => a1.RoleID == roleID)
            .Select((a1, b2) => new MenuViewModel
            {
                ID = b2.ID,
                ParentID = b2.ParentID,
                Name = b2.Name,
                path = b2.path,
                Redirect = b2.Redirect,
                Title = b2.Title,
                Icon = b2.Icon,
                Component = b2.Component,
                Hidden = b2.Hidden,
                CreateAt = b2.CreateAt,
                CreateBy = b2.CreateBy,
                UpdatedAt = b2.UpdatedAt,
                UpdatedBy = b2.UpdatedBy
            })
            .ToListAsync();
        }
    }
}
