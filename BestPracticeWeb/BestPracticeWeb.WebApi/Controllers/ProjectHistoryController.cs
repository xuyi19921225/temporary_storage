using System.Threading.Tasks;
using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticeWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectHistoryController : ControllerBase
    {
        private readonly IProjectHistoryService projectHistoryService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="projectHistoryService"></param>
        public ProjectHistoryController(IProjectHistoryService projectHistoryService)
        {
            this.projectHistoryService = projectHistoryService;
        }

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<PageModel<ProjectHistory>>> Get([FromQuery]ProjectHistoryRequestModel model)
        {
            return new MessageModel<PageModel<ProjectHistory>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await projectHistoryService.GetProjectHistoryList(model)
            };
        }

    }
}