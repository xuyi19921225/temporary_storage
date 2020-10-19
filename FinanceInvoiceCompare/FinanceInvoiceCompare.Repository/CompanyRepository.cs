using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<List<Company>> GetCompanyCode(int userID)
        {
            return await Db.Queryable<User, UserCompanyMapping, Company>((a1, b2, c3) => new object[]
            {
                        JoinType.Left,a1.Id==b2.UserID,
                        JoinType.Left,b2.CompanyID==c3.Id

            })
            .Where((a1, b2, c3) => a1.Id == userID && c3.IsDelete == false)
            .Select<Company>()
            .ToListAsync();
        }
    }
}
