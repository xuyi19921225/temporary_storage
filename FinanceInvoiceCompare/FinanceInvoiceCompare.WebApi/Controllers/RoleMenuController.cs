using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        private readonly IMenuService menuService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<UserCompanyController> logger;

        public RoleMenuController(IRoleMenuService roleMenuService, IMenuService menuService, IUnitOfWork unitOfWork, ILogger<UserCompanyController> logger)
        {
            this.roleMenuService = roleMenuService;
            this.menuService = menuService;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// 获取所有二级菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetTreeMenus")]
        public async Task<MessageModel<List<Menu>>> GetTreeMenus([FromQuery] int roleID)
        {
            var listTree = new List<Menu>();

            List<Menu> parentMenus;



            if (roleID >= 0)
            {
                ////获取父级菜单
                parentMenus = (await roleMenuService.GetRMenuList()).Where(x => x.ParentID == -1 && x.RId == roleID).ToList();
            }
            else
            {
                ////获取父级菜单
                parentMenus = await menuService.Query(x => x.IsDelete == false && x.ParentID == -1);
            }

            foreach (var item in parentMenus)
            {
                var treeModel = new Menu();
                List<Menu> childrenMenus;
                if (roleID >= 0)
                {
                    ////获取二级菜单
                    childrenMenus = (await roleMenuService.GetRMenuList()).Where(x => x.ParentID == item.Id && x.RId == roleID).ToList();
                }
                else
                {
                    ////获取二级菜单
                    childrenMenus = await menuService.Query(x => x.IsDelete == false && x.ParentID == item.Id);
                }

                treeModel = item;
                treeModel.Children = childrenMenus;

                listTree.Add(treeModel);
            }

            return new MessageModel<List<Menu>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = listTree
            };
        }

        /// <summary>
        /// 获取角色已选菜单信息
        /// </summary>
        /// <param name="roleId">roleId</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<List<int>>> Get([FromQuery] int roleId)
        {
            return new MessageModel<List<int>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = (await roleMenuService.GetRMenuList()).Where(x => x.RId == roleId).Select(y => y.Id).ToList()
            };
        }

        /// <summary>
        /// 保存角色和菜单的授权关系
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post([FromBody] RoleMenuMapping model)
        {
            var data = new MessageModel<string>();
            try
            {
                unitOfWork.BeginTran();

                if (model != null && model.RoleID > 0)
                {

                    // 无论 Update Or Add , 先删除当前角色的全部 R_M 关系
                    var roleMenuIDS = (await roleMenuService.Query(d => d.RoleID == model.RoleID)).Select(d => d.ID.ToString()).ToArray();

                    bool isAllDeleted;

                    if (roleMenuIDS.Length > 0)
                    {
                        ////删除关联关系
                        isAllDeleted = await roleMenuService.DeleteByIds(roleMenuIDS);
                    }
                    else
                    {
                        isAllDeleted = true;
                    }


                    var roleMenu = new List<RoleMenuMapping>();
                    //// 如果没有添加的关系 删除关系结果为最终结果 
                    var flag = isAllDeleted;

                    if (model.list != null && model.list.Count > 0)
                    {
                        model.list.ForEach(x =>
                        {
                            roleMenu.Add(new RoleMenuMapping() { RoleID = model.RoleID, MenuID = x.Id, CreateAt = DateTime.Now, CreateBy = x.CreateBy });
                        });
                        ////添加关系
                        flag = await roleMenuService.Add(roleMenu) > 0;
                    }

                    unitOfWork.CommitTran();

                    data.Success = flag;
                    if (flag)
                    {
                        data.Message = "保存成功";
                    }
                    else
                    {
                        unitOfWork.RollbackTran();
                        data.Message = "保存失败";
                    }
                }
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTran();
                logger.LogError(ex, ex.Message);
            }




            return data;
        }
    }
}