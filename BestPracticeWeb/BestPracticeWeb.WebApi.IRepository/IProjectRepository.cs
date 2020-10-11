using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.IRepository
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<PageModel<ProjectViewModel>> QueryProjectListMulti(ProjectRequestModel model);


        Task<PageModel<ProjectViewModel>> QueryProjectListMulti(DashboardRequestModel model);

        bool AddProjectMulti(ProjectSaveRequestModel model);

        bool DeleteProjectMulti(Project model);

        bool UpdateProjectMulti(Project model);
        
    }
}
