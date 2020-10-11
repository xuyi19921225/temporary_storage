using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.Service
{
    public class ProjectService : BaseServices<Project>, IProjectService
    {
        private readonly IProjectRepository projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
            base.BaseDal = projectRepository;
        }


        public bool DeleteProjectMulti(Project model)
        {
            model.UpdatedAt = DateTime.Now;
            return projectRepository.DeleteProjectMulti(model);
        }


        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="model">项目列表查询实体</param>
        /// <returns></returns>
        public async Task<PageModel<ProjectViewModel>> GetProjectListMulti(ProjectRequestModel model)
        {
            return await projectRepository.QueryProjectListMulti(model);
        }

        /// <summary>
        /// 获取报表信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageModel<ProjectViewModel>> GetProjectListMulti(DashboardRequestModel model)
        {
            return await projectRepository.QueryProjectListMulti(model);
        }

        public bool SaveProjectMulti(ProjectSaveRequestModel model)
        {
            model.CreateAt = DateTime.Now;
            return projectRepository.AddProjectMulti(model);
        }

        public bool UpdateProjectMulti(Project model)
        {
            model.UpdatedAt = DateTime.Now;
            return projectRepository.UpdateProjectMulti(model);
        }
    }
}
