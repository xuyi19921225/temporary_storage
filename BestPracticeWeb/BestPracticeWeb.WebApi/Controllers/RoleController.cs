using System.Collections.Generic;
using System.Threading.Tasks;
using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticeWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;
        public RoleController(IRoleService roleService)
        {
            this.roleService =roleService;
        }

        /// <summary>
        /// 获取角色信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<List<Role>>> Get()
        {
            return new MessageModel<List<Role>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await roleService.Query()
            };
        }

    }
}