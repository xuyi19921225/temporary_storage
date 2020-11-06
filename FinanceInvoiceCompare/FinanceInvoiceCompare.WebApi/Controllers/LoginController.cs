using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Controllers
{
    /// <summary>
    /// 获取认证信息
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJwtSerivce _jwtFactory;
        private readonly ILDAPService _ladpUtility;
        private readonly IUserService userService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginController(IJwtSerivce jwtFactory, ILDAPService ladpUtility, IUserService userService)
        {
            _jwtFactory = jwtFactory;
            _ladpUtility = ladpUtility;
            this.userService = userService;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="model">账号信息</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<string>> GetToken([FromQuery]LoginRequestModel model)
        {
            //_ladpUtility.ValidADUser(model)
            //// 验证AD 账号
            if (_ladpUtility.ValidADUser(model))
            {
                var user = await userService.Query(x => x.NTID == model.NTID && x.IsActive == true && x.IsDelete == false);

                if (user.Count > 0)
                {
                    return new MessageModel<string>()
                    {
                        Success = true,
                        Message = "获取成功",
                        Response = await _jwtFactory.GenerateToken(model)
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        Success = false,
                        Message = "系统中不存在该账号或账号已被锁定，获取认证失败"
                    };
                }
            }
            else
            {
                return new MessageModel<string>()
                {
                    Success = false,
                    Message = "账号或密码不正确，获取认证失败"
                };
            }
        }

    }
}