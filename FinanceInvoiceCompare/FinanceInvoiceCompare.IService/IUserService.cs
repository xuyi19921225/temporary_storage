﻿using FinanceInvoiceCompare.WebApi.IService.BASE;
using FinanceInvoiceCompare.WebApi.Model;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.IService
{
    public interface IUserService:IBaseServices<User>
    {
        Task<sysUserInfo> GetSysUserInfo(string ntid);
    }
}
