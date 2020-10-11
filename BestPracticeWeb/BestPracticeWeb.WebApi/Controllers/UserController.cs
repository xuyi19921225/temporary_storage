using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.Controllers
{
    /// <summary>
    /// User Moudule
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ISAPUserService sAPUserService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="sAPUserService"></param>
        public UserController(IUserService userService, ISAPUserService sAPUserService)
        {
            this.userService = userService;
            this.sAPUserService = sAPUserService;
        }

        /// <summary>
        /// 获取人员角色菜单信息
        /// </summary>
        /// <param name="ntid"></param>
        /// <param name="siteID"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetUserRoleMenuInfo")]
        public async Task<MessageModel<UserRolePermissionsInfoViewModel>> GetUserRoleMenuInfo([FromQuery]string ntid, [FromQuery]int siteID)
        {
            return new MessageModel<UserRolePermissionsInfoViewModel>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await userService.GetUserRoleMenuInfo(ntid, siteID)
            };

        }


        /// <summary>
        /// 获取人员信息列表
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<PageModel<UserViewModel>>> Get([FromQuery]UserRequestModel model)
        {
            return new MessageModel<PageModel<UserViewModel>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await userService.GetUserList(model)
            };
        }

        /// <summary>
        /// 通过用户ID获取用户信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetUserInfoByID")]
        public async Task<MessageModel<User>> GetUserInfoByID([FromQuery]int id)
        {
            return new MessageModel<User>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await userService.QueryByID(id)
            };
        }


        /// <summary>
        /// 通过WorkdayID获取SAP人员信息
        /// </summary>
        /// <param name="workdayID">workdayID</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetSAPUserInfoByWorkdayID")]
        public async Task<MessageModel<SAPUser>> GetSAPUserInfoByWorkdayID([FromQuery]string workdayID)
        {
            return new MessageModel<SAPUser>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await sAPUserService.QueryByExpression(model => model.WorkDayID == workdayID)
            };

        }

        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post([FromBody]User model)
        {
            //检查是否存在用户
            List<User> list = await userService.Query(i => i.JobNumber == model.JobNumber || i.NTID == model.NTID);

            if (list.Count > 0)
            {
                return new MessageModel<string>()
                {
                    Success = false,
                    Message = "用户工号或NTID已经存在，请检查",
                };
            }
            else
            {
                model.CreateAt = DateTime.Now;
                bool flag = await userService.Add(model) > 0;
                if (flag)
                {
                    return new MessageModel<string>()
                    {
                        Success = flag,
                        Message = "创建成功",
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        Success = flag,
                        Message = "创建失败",
                    };
                }

            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<MessageModel<string>> Put([FromBody]User model)
        {
            //检查是否存在用户
            List<User> list = await userService.Query(i => (i.JobNumber == model.JobNumber || i.NTID == model.NTID) && i.ID != model.ID);

            if (list.Count > 0)
            {
                return new MessageModel<string>()
                {
                    Success = false,
                    Message = "用户工号或NTID已经存在，请检查",
                };
            }
            else
            {
                model.UpdatedAt = DateTime.Now;
                bool flag = await userService.Update(model);
                if (flag)
                {
                    return new MessageModel<string>()
                    {
                        Success = flag,
                        Message = "更新成功",
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        Success = flag,
                        Message = "更新失败",
                    };
                }

            }
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<MessageModel<bool>> Delete([FromQuery]int id)
        {
            bool flag = await userService.DeleteById(id);

            if (flag)
            {
                return new MessageModel<bool>()
                {
                    Success = flag,
                    Message = "更新成功",
                };
            }
            else
            {
                return new MessageModel<bool>()
                {
                    Success = flag,
                    Message = "更新失败",
                };
            }
        }

    }
}