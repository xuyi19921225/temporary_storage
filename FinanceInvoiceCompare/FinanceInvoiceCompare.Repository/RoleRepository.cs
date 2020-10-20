using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class RoleRepository:BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
