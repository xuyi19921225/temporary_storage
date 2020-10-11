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
    public class DashboardController : ControllerBase
    {
        private readonly IProjectService projectService;

        public DashboardController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        /// <summary>
        /// 获取报表信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<PageModel<ProjectViewModel>>> Get([FromQuery]DashboardRequestModel model)
        {
            return new MessageModel<PageModel<ProjectViewModel>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await projectService.GetProjectListMulti(model)
            };
        }

    }
}