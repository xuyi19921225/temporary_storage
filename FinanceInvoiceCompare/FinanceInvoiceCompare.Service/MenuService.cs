using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Service
{
    public class MenuService : BaseServices<Menu>, IMenuService
    {
        private readonly IMenuRepository menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            base.BaseDal = menuRepository;
            this.menuRepository = menuRepository;
        }

    }
}
