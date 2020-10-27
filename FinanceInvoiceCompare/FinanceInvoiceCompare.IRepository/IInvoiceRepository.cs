﻿using FinanceInvoiceCompare.WebApi.IRepository.Base;
using FinanceInvoiceCompare.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.IRepository
{
    public interface IInvoiceRepository: IBaseRepository<Invoice>
    {
        Task<PageModel<UMatchInvoiceReportViewModel>> GetMatchInvoiceRepost(MatchInvoiceReportRequestModel model);
    }
}