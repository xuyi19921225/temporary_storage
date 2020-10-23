using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class VendorRepository:BaseRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
    }
}
