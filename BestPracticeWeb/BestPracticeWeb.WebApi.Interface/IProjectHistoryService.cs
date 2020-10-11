using BestPracticeWeb.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.IService
{
    public interface IProjectHistoryService:IBaseServices<ProjectHistory>
    {
        Task<PageModel<ProjectHistory>> GetProjectHistoryList(ProjectHistoryRequestModel model);
    }
}
