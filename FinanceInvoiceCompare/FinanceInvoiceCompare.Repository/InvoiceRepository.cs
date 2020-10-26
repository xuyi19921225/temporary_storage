using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class InvoiceRepository:BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
    }
}
