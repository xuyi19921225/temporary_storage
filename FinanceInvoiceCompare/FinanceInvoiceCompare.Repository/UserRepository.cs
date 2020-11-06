using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;
using SqlSugar;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<sysUserInfo> GetSysUserInfo(string ntid)
        {
            return await Db.Queryable<User, UserRoleMapping, Role>((a1, b2, c3) => new object[]
            {
                        JoinType.Left,a1.Id==b2.UserID ,
                        JoinType.Left,b2.RoleID==c3.Id && c3.IsDelete == false

            })
            .Where((a1, b2, c3) => a1.NTID == ntid && a1.IsDelete == false )
            .Select<sysUserInfo>()
            .FirstAsync();
        }
    }
}
