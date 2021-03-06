﻿using FinanceInvoiceCompare.WebApi.IRepository.Base;
using FinanceInvoiceCompare.WebApi.Model;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.IRepository
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<sysUserInfo> GetSysUserInfo(string ntid);
        
    }
}
