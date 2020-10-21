using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public UserController(IUserService userService, IJwtSerivce jwtSerivce)
        {
            this.userService = userService;
            this.jwtSerivce = jwtSerivce;
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
        public async Task<MessageModel<PageModel<User>>> Get([FromQuery]UserRequestModel model)
        {
            return new MessageModel<PageModel<User>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await userService.QueryPage(a => a.IsDelete == false, model.PageIndex, model.PageSize, " ID desc ")
            };
        }


        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post([FromBody]User model)
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
                var flag = data.Success = await userService.Add(model, null, new List<string>() { "UpdatedBy","UpdatedAt" }) > 0;
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
        public async Task<MessageModel<string>> Put([FromBody]User model)
        {
            var data = new MessageModel<string>();



            var flag = data.Success = await userService.Update(model,null,new List<string>() { "CreateBy","CreateAt" });
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
        /// 删除一个用户
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