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
    /// 角色和菜单关系
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoleMenuController : ControllerBase
    {
        private readonly IRoleMenuService roleMenuService;

        public RoleMenuController(IRoleMenuService roleMenuService)
        {
            this.roleMenuService = roleMenuService;
        }


        /// <summary>
        /// 根据角色ID获取菜单信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<List<Menu>>> GetMenus(int roleId)
        {
            var data = new MessageModel<List<Menu>>();

            if (roleId > 0) 
            {
                var allroleMenuMapping = await roleMenuService.GetRMMaps();
                var roleMenuMapping = allroleMenuMapping.Where(x => x.RoleID == roleId).ToList();
                if (roleMenuMapping != null)
                {
                    data.Response = roleMenuMapping[0].Menus;
                    data.Success = true;
                    data.Message = "获取成功";
                }
            }
            return data;
        }
    }
}