using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;
using SqlSugar;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class RoleMenuRepository:BaseRepository<RoleMenuMapping>, IRoleMenuRepository
    {
        public RoleMenuRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<List<Menu>> GetRMenuList()
        {
            return await Db.Queryable<Role, RoleMenuMapping, Menu>((a1, b2, c3) => new object[]
           {
                    JoinType.Left,a1.Id==b2.RoleID,
                    JoinType.Left,b2.MenuID==c3.Id

           })
           .Where((a1, b2, c3) => c3.IsDelete == false)
           .Select((a1, b2, c3) => new Menu
           {
               Id = c3.Id,
               ParentID=c3.ParentID,
               Icon=c3.Icon,
               Path=c3.Path,
               Hidden=c3.Hidden,
               Redirect=c3.Redirect,
               Title=c3.Title,
               AlwaysShow=c3.AlwaysShow,
               Component=c3.Component,
               Name = c3.Name,
               IsDelete = c3.IsDelete,
               RId = a1.Id,
               CreateAt = c3.CreateAt,
               CreateBy = c3.CreateBy,
               UpdatedAt = c3.UpdatedAt,
               UpdatedBy = c3.UpdatedBy
           })
           .ToListAsync();
        }
    }   
}
