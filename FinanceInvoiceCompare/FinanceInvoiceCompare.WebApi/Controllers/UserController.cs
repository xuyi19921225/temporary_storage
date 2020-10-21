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
    /// 用户操作
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IJwtSerivce jwtSerivce;
        private readonly IUserRoleService userRoleService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, IJwtSerivce jwtSerivce, IUserRoleService userRoleService, IUnitOfWork unitOfWork, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.jwtSerivce = jwtSerivce;
            this.userRoleService = userRoleService;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// 根据Token获取用户信息
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetUserInfoByToken")]
        public async Task<MessageModel<sysUserInfo>> GetUserInfoByToken(string token)
        {
            var data = new MessageModel<sysUserInfo>();
            if (!string.IsNullOrEmpty(token))
            {
                var tokenModel = jwtSerivce.SerializeJwt(token);
                if (tokenModel != null && !string.IsNullOrEmpty(tokenModel.NTID.Trim()))
                {
                    var userinfo = await userService.GetSysUserInfo(tokenModel.NTID);
                    if (userinfo != null)
                    {
                        data.Response = userinfo;
                        data.Success = true;
                        data.Message = "获取成功";
                    }
                }

            }
            return data;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<PageModel<UserRoleViewModel>>> Get([FromQuery] UserRequestModel model)
        {
            return new MessageModel<PageModel<UserRoleViewModel>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await userRoleService.UserRoleMaps(model)
            };
        }


        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post([FromBody] User model)
        {
            var data = new MessageModel<string>();


            var user = await userService.Query(x => x.NTID == model.NTID && x.IsDelete == false);

            if (user.Count > 0)
            {
                data.Success = false;
                data.Message = "该用户已经存在";
            }
            else
            {
                var flag = data.Success = await userService.Add(model) > 0;
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
        /// 更新一个用户
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<MessageModel<string>> Put([FromBody] User model)
        {
            var data = new MessageModel<string>();
            try
            {
                unitOfWork.BeginTran();
                if (model.RID > 0)
                {
                    // 无论 Update Or Add , 先删除当前用户的全部 U_R 关系
                    var usreroles = (await userRoleService.Query(d => d.UserID == model.Id)).Select(d => d.ID.ToString()).ToArray();
                    ////删除关联关系
                    var isAllDeleted = await userRoleService.DeleteByIds(usreroles);

                    ////添加关系
                    var addUserRole = await userRoleService.Add(new UserRoleMapping() { RoleID = model.RID, UserID = model.Id, CreateBy = model.UpdatedBy });
                }
          
                var flag = data.Success = await userService.Update(model);

                unitOfWork.CommitTran();
                if (flag)
                {
                    data.Message = "更新成功";
                }
                else
                {
                    data.Message = "更新失败";
                }
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTran();
                logger.LogError(ex, ex.Message);
            }




            return data;
        }

        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<MessageModel<string>> Delete([FromQuery] int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var userDetail = await userService.QueryById(id);
                userDetail.IsDelete = true;

                var flag = data.Success = await userService.Update(userDetail);

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