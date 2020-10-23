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
    /// 菜单操作
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<PageModel<Menu>>> Get([FromQuery] MenuRequestModel model)
        {
            return new MessageModel<PageModel<Menu>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await menuService.QueryPage(a => a.IsDelete == false, model.PageIndex, model.PageSize, " ID desc ")
            };
        }


        /// <summary>
        /// 获取所有父级菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetAllParentMenus")]
        public async Task<MessageModel<List<Menu>>> GetAllParentMenus()
        {
            return new MessageModel<List<Menu>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await menuService.Query(x=>x.IsDelete==false&&x.ParentID==-1)
            };
        }

 

        /// <summary>
        /// 添加一个菜单
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post([FromBody]Menu model)
        {
            var data = new MessageModel<string>();


            var menu = await menuService.Query(x => x.Name == model.Name && x.IsDelete == false);

            if (menu.Count > 0)
            {
                data.Success = false;
                data.Message = "该菜单已经存在";
            }
            else
            {
                var flag = data.Success = await menuService.Add(model) > 0;
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
        /// 更新一个菜单
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<MessageModel<string>> Put([FromBody]Menu model)
        {
            var data = new MessageModel<string>();



            var flag = data.Success = await menuService.Update(model);
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
        /// 删除一个菜单
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
                var menuDetail = await menuService.QueryById(id);
                menuDetail.IsDelete = true;

                var flag = data.Success = await menuService.Update(menuDetail);

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
