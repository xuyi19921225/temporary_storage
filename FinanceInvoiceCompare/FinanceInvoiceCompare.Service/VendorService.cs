using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;

namespace FinanceInvoiceCompare.WebApi.Service
{
    public  class VendorService:BaseServices<Vendor>, IVendorService
    {
        private readonly IVendorRepository vendorRepository;

        public VendorService(IVendorRepository vendorRepository)
        {
            this.vendorRepository = vendorRepository;
            base.BaseDal = vendorRepository;
        }
    }
}
