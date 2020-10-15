using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;

namespace FinanceInvoiceCompare.WebApi.Service
{
    public class UserService:BaseServices<User>,IUserService
    {
        private readonly IUserRepository _dal;
        public UserService(IUserRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
    }
}
