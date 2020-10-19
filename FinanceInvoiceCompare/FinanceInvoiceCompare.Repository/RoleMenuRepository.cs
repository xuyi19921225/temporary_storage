using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class RoleMenuRepository:BaseRepository<RoleMenuMapping>, IRoleMenuRepository
    {
        public RoleMenuRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<List<RoleMenuMapping>> GetRMMaps()
        {
            return await Db.Queryable<RoleMenuMapping>()
               .Mapper(rmp => rmp.Menus, rmp => rmp.MenuID)
               .ToListAsync();
        }
    }   
}
