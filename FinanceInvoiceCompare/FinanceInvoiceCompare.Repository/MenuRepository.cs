using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


    }
}
