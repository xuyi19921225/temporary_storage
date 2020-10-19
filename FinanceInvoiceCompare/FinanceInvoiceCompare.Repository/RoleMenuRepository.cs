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


        public async Task<List<Menu>> GetMenusByRoleID(int roleID)
        {
            //return await Db.Queryable<Menu>()
            //   .Mapper(rmp => rmp.Menus, rmp => rmp.MenuID)
            //   .ToListAsync();

            return await Db.Queryable<Menu, RoleMenuMapping, Role>((a1, b2, c3) => new object[]
          {
                        JoinType.Left,a1.Id==b2.MenuID,
                        JoinType.Left,b2.RoleID==c3.Id

          })
          .Where((a1, b2, c3) =>c3.Id==roleID&& a1.IsDelete == false && c3.IsDelete == false)
          .Select<Menu>()
          .ToListAsync();
        }
    }   
}
