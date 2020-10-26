using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class SAPRepository:BaseRepository<SAPInvoiceData>, ISAPRepository
    {
        public SAPRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
    }
}
