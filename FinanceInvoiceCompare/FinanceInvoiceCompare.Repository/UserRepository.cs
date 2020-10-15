using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IRepository.UnitOfWork;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }   
    }
}
