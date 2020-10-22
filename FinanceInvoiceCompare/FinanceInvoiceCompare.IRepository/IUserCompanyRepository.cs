using FinanceInvoiceCompare.WebApi.IRepository.Base;
using FinanceInvoiceCompare.WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.IRepository
{
    public interface IUserCompanyRepository:IBaseRepository<UserCompanyMapping>
    {
        Task<List<Company>> GetUCompanyList();
    }
}
