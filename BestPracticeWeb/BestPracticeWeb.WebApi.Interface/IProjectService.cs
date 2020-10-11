using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.IService
{
    public interface IProjectService:IBaseServices<Project>
    {
        Task<PageModel<ProjectViewModel>> GetProjectListMulti(ProjectRequestModel model);

        Task<PageModel<ProjectViewModel>> GetProjectListMulti(DashboardRequestModel model);


        /// <summary>
        /// 多表插入
        /// </summary>
        /// <returns></returns>
        bool SaveProjectMulti(ProjectSaveRequestModel model);

        /// <summary>
        /// 多表删除
        /// </summary>
        /// <returns></returns>
        bool DeleteProjectMulti(Project model);

        /// <summary>
        /// 多表更新
        /// </summary>
        /// <returns></returns>
        bool UpdateProjectMulti(Project model);
    }
}
