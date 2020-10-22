using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Service
{
    public  class UserCompanyService:BaseServices<UserCompanyMapping>, IUserCompanyService
    {
        private readonly IUserCompanyRepository userCompanyRepository;

        public UserCompanyService(IUserCompanyRepository userCompanyRepository)
        {
            this.userCompanyRepository = userCompanyRepository;
            base.BaseDal = userCompanyRepository;
        }

        public async Task<List<Company>> GetUCompanyList() 
        {
           return await userCompanyRepository.GetUCompanyList();
        }
    }
}
