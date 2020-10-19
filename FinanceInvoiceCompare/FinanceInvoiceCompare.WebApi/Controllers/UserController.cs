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

        public UserController(IUserService userService,IJwtSerivce jwtSerivce)
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
    }
}