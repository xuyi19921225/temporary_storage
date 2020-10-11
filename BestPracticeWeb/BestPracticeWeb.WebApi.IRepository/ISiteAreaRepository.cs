using BestPracticeWeb.WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.IRepository
{
    public interface ISiteAreaRepository:IBaseRepository<SiteArea>
    {
        Task<PageModel<SiteAreaViewModel>> QuerySiteAreaListMulti(SiteAreaRequestModel requestModel);

        Task<List<SiteAreaViewModel>> QuerySiteAreaListMulti(int? parentID);

        Task<List<SiteArea>> QuerySiteAreaListByParameter(SiteAreaRequestParameterModel model);
    }
}
