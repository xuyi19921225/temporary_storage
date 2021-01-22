using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Service
{
    public class TaxCodeService : BaseServices<TaxCode>, ITaxCodeService
    {
        private readonly ITaxCodeRepository taxCodeRepository;

        public TaxCodeService(ITaxCodeRepository taxCodeRepository)
        {
            base.BaseDal = taxCodeRepository;
            this.taxCodeRepository = taxCodeRepository;
        }
    }
}
