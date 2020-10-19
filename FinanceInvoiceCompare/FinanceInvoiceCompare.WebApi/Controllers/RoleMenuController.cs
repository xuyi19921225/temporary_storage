﻿using System;
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
        public async Task<MessageModel<List<RoleMenuMapping>>> GetMenus(int id)
        {
            var data = new MessageModel<List<RoleMenuMapping>>();

            if (id > 0) 
            {
                var allroleMenuMapping = await roleMenuService.GetRMMaps();
                var roleMenuMapping = allroleMenuMapping.Where(x => x.MenuID == id).ToList();
                if (roleMenuMapping != null)
                {
                    data.Response = roleMenuMapping;
                    data.Success = true;
                    data.Message = "获取成功";
                }
            }
            return data;
        }
    }
}