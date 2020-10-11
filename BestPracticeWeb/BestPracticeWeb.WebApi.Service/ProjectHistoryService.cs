using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.Service
{
    public class ProjectHistoryService : BaseServices<ProjectHistory>, IProjectHistoryService
    {
        private readonly IProjectHistoryRepository projectHistoryRepository;
        public ProjectHistoryService(IProjectHistoryRepository projectHistoryRepository)
        {
            this.projectHistoryRepository = projectHistoryRepository;
            base.BaseDal = projectHistoryRepository;
        }


        public async Task<PageModel<ProjectHistory>> GetProjectHistoryList(ProjectHistoryRequestModel model)
        {
            return await projectHistoryRepository.QueryProjectHistoryList(model);
        }
    }
}
