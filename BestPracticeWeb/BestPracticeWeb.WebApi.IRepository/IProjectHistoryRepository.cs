using BestPracticeWeb.WebApi.Model;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.IRepository
{
    public interface IProjectHistoryRepository : IBaseRepository<ProjectHistory>
    {
        Task<PageModel<ProjectHistory>> QueryProjectHistoryList(ProjectHistoryRequestModel model);
    }
}
