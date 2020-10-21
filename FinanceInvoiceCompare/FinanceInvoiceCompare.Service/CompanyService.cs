using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Service
{
    public class CompanyService:BaseServices<Company>, ICompanyService
    {
        private readonly ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
            base.BaseDal = companyRepository;
        }
    }
}
