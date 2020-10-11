using System;
using System.Collections.Generic;
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
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly ISiteAreaService siteAreaService;
        private readonly IDepartmentService departmentService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="projectService"></param>
        /// <param name="siteAreaService"></param>
        /// <param name="departmentService"></param>
        public ProjectController(IProjectService projectService,ISiteAreaService siteAreaService,IDepartmentService departmentService)
        {
            this.projectService = projectService;
            this.siteAreaService = siteAreaService;
            this.departmentService = departmentService;
        }

        /// <summary>
        /// 获取项目
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<PageModel<ProjectViewModel>>> Get([FromQuery]ProjectRequestModel model)
        {
            return new MessageModel<PageModel<ProjectViewModel>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await projectService.GetProjectListMulti(model)
            };
        }


        /// <summary>
        /// 根据项目ID获取项目
        /// </summary>
        /// <param name="id">项目ID</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetProjectByID")]
        public async Task<MessageModel<Project>> Get(int id)
        {
            return new MessageModel<Project>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await projectService.QueryByID(id)
            };
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public MessageModel<bool> Put([FromBody] Project model)
        {

            model.UpdatedAt = DateTime.Now;
            bool flag = projectService.UpdateProjectMulti(model);
            if (flag)
            {
                return new MessageModel<bool>()
                {
                    Message = "更新信息成功",
                    Success = true,
                    Response = flag
                };
            }
            else
            {
                return new MessageModel<bool>()
                {
                    Message = "更新信息失败",
                    Success = true,
                    Response = flag
                };
            }
        }


        /// <summary>
        /// 删除项目
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public MessageModel<bool> Delete([FromQuery]Project model)
        {
            model.UpdatedAt = DateTime.Now;
            model.IsDelete = 1;
            bool flag = projectService.DeleteProjectMulti(model);
            if (flag)
            {
                return new MessageModel<bool>()
                {
                    Message = "删除信息成功",
                    Success = true,
                    Response = flag
                };
            }
            else
            {
                return new MessageModel<bool>()
                {
                    Message = "删除信息失败",
                    Success = true,
                    Response = flag
                };
            }
        }



        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="model">请求实体</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public MessageModel<bool> Post([FromBody]ProjectSaveRequestModel model)
        {
            bool flag = projectService.SaveProjectMulti(model);
            if (flag)
            {
                return new MessageModel<bool>()
                {
                    Message = "创建信息成功",
                    Success = true,
                    Response = flag
                };
            }
            else
            {
                return new MessageModel<bool>()
                {
                    Message = "创建信息失败",
                    Success = true,
                    Response = flag
                };
            }
        }

        /// <summary>
        /// 批量创建项目
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("BatchAdd")]
        public async Task<MessageModel<string>> BatchAdd(List<ProjectUploadRequestModel> list)
        {
            if (list.Count > 0)
            {
                DateTime dateTime = DateTime.Now;
                List<Project> projects = new List<Project>();
                SiteArea siteArea;
                Department department;
                int siteID;
                int divisionID;
                int stationID;
                foreach (var item in list)
                {
                    //// 查询SiteID
                    siteArea = await siteAreaService.QueryFirst(model => model.Value == item.Site && model.Type == "Site");
                    if (siteArea != null)
                    {
                        siteID = siteArea.ID;
                    }
                    else
                    {
                        return new MessageModel<string>() { Message = $"{ item.Site}不存在，请检查", Success = false };
                    }

                    //// 查询DivisionID
                    siteArea = await siteAreaService.QueryFirst(model => model.Value == item.Division && model.ParentID == siteID && model.Type == "Division");
                    if (siteArea != null)
                    {
                        divisionID = siteArea.ID;
                    }
                    else
                    {
                        return new MessageModel<string>() { Message = $"{ item.Division}不存在，请检查", Success = false };
                    }

                    //// 查询StationID
                    siteArea = await siteAreaService.QueryFirst(model => model.Value == item.Station && model.ParentID == divisionID && model.Type == "Station");
                    if (siteArea != null)
                    {
                        stationID = siteArea.ID;
                    }
                    else
                    {
                        return new MessageModel<string>() { Message = $"{ item.Station}不存在，请检查", Success = false };
                    }

                    //// 查询Department
                    department = await departmentService.QueryFirst(model => model.Department1 == item.Department);

                    if (department == null)
                    {
                        return new MessageModel<string>() { Message = $"{ item.Department}不存在，请检查", Success = false };
                    }

                    ////  无法批量插入历史表（待修改）
                    projects.Add(new Project()
                    {
                        SiteID = siteID,
                        DivisionID = divisionID,
                        StationID = stationID,
                        Department = item.Department,
                        ProjectTitle = item.ProjectTitle,
                        CreateBy = item.CreateBy,
                        CreateAt = dateTime
                    });
                }

                int i = await projectService.Add(projects, new List<string>() { "UpdatedAt", "UpdatedBy" });

                if (i > 0)
                {
                    return new MessageModel<string>() { Message = "数据导入成功", Success = true };
                }
                else
                {
                    return new MessageModel<string>() { Message = "数据导入失败", Success = false };
                }
            }
            else
            {
                return new MessageModel<string>() { Message = "数据为空，无法导入", Success = false };
            }
        }
    }
}