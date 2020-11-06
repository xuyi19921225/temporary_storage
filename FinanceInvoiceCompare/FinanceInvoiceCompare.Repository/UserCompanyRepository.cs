using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class UserCompanyRepository : BaseRepository<UserCompanyMapping>, IUserCompanyRepository
    {
        public UserCompanyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<List<Company>> GetUCompanyList()
        {
            return await Db.Queryable<User, UserCompanyMapping, Company>((a1, b2, c3) => new object[]
            {
                JoinType.Inner,a1.Id==b2.UserID,
                JoinType.Inner,b2.CompanyID==c3.Id

            })
            .Where((a1, b2, c3) =>a1.IsDelete==false && c3.IsDelete == false)
            .Select((a1, b2, c3) => new Company
            {
                Id = c3.Id,
                Code = c3.Code,
                CompanyName = c3.CompanyName,
                IsDelete = c3.IsDelete,
                UId = a1.Id,
                CreateAt=c3.CreateAt,
                CreateBy=c3.CreateBy,
                UpdatedAt=c3.UpdatedAt,
                UpdatedBy=c3.UpdatedBy
            })
            .ToListAsync();
        }
    }
}
