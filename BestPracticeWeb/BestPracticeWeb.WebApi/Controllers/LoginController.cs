using System.Collections.Generic;
using System.Threading.Tasks;
using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticeWeb.WebApi.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        private readonly IJwtSerivce _jwtFactory;
        private readonly ILDAPService _ladpUtility;
        private readonly ISiteAreaService siteAreaService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginController(IJwtSerivce jwtFactory, ILDAPService ladpUtility,ISiteAreaService siteAreaService)
        {
            _jwtFactory = jwtFactory;
            _ladpUtility = ladpUtility;
            this.siteAreaService = siteAreaService;
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


        /// <summary>
        /// 根据参数获取SiteArea信息
        /// </summary>
        /// <param name="model">参数实体</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetSiteList")]
        public async Task<MessageModel<List<SiteArea>>> GetSiteList([FromQuery]SiteAreaRequestParameterModel model)
        {
            return new MessageModel<List<SiteArea>>()
            {
                Success = true,
                Message = "获取成功",
                Response = await siteAreaService.GetSiteAreaList(model)
            };
        }


    }
}