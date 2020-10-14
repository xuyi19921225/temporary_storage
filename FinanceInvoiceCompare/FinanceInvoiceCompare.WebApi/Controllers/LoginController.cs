using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJwtSerivce _jwtFactory;
        private readonly ILDAPService _ladpUtility;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginController(IJwtSerivce jwtFactory, ILDAPService ladpUtility)
        {
            _jwtFactory = jwtFactory;
            _ladpUtility = ladpUtility;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="model">账号信息</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<string>> Get([FromQuery]LoginRequestModel model)
        {
            //// 验证AD 账号
            if (_ladpUtility.ValidADUser(model))
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
                    Message = "账号或密码不正确"
                };
            }
        }

    }
}