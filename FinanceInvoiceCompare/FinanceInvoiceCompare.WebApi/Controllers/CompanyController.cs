using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceInvoiceCompare.WebApi.Controllers
{
    /// <summary>
    /// 公司操作
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }



        /// <summary>
        /// 获取所有公司信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetAllCompanyList")]
        public async Task<MessageModel<List<Company>>> GetAllCompanyList()
        {
            return new MessageModel<List<Company>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await companyService.Query(x => x.IsDelete == false)
            };
        }


        /// <summary>
        ///根据CompanyId获取Campany信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetCompanyById")]
        public async Task<MessageModel<List<Company>>> GetCompanyById([FromQuery] object[] ids)
        {
            return new MessageModel<List<Company>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await companyService.QueryByIDs(ids)
            };
        }

    }
}
