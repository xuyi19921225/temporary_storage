using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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



        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetAllRoleList")]
        public async Task<MessageModel<List<Role>>> GetAllRoleList()
        {
            return new MessageModel<List<Role>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await roleService.Query(x => x.IsDelete == false)
            };
        }



        /// <summary>
        /// 添加一个角色
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post([FromBody]Role model)
        {
            var data = new MessageModel<string>();


            var role = await roleService.Query(x => x.RoleCode == model.RoleCode && x.IsDelete == false);

            if (role.Count > 0)
            {
                data.Success = false;
                data.Message = "该角色已经存在";
            }
            else
            {
                var flag = data.Success = await roleService.Add(model, null, new List<string>() { "UpdatedBy", "UpdatedAt" }) > 0;
                if (flag)
                {
                    data.Message = "添加成功";
                }
                else
                {
                    data.Message = "添加失败";
                }

            }
            return data;
        }

        /// <summary>
        /// 更新一个角色
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<MessageModel<string>> Put([FromBody]Role model)
        {
            var data = new MessageModel<string>();



            var flag = data.Success = await roleService.Update(model, null, new List<string>() { "CreateBy", "CreateAt" });
            if (flag)
            {
                data.Message = "更新成功";
            }
            else
            {
                data.Message = "更新失败";
            }

            return data;
        }

        /// <summary>
        /// 删除一个角色
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<MessageModel<string>> Delete([FromQuery]int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var roleDetail = await roleService.QueryById(id);
                roleDetail.IsDelete = true;

                var flag = data.Success = await roleService.Update(roleDetail);

                if (flag)
                {
                    data.Message = "删除成功";
                }
                else
                {
                    data.Message = "删除失败";
                }

            }
            return data;
        }

    }
}