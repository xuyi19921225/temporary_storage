using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Controllers
{
    /// <summary>
    /// 角色逻辑
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }


        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<PageModel<Role>>> Get([FromQuery]RoleRequestModel model)
        {
            Expression<Func<Role, bool>> whereExpression = a => a.IsDelete == false;
            return new MessageModel<PageModel<Role>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await roleService.QueryPage(whereExpression, model.PageIndex, model.PageSize, " ID desc ")
            };
        }
    }
}