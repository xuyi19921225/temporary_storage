using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.IService
{
    public interface ISiteAreaService:IBaseServices<SiteArea>
    {
        Task<PageModel<SiteAreaViewModel>> GetSiteAreaList(SiteAreaRequestModel model);

        Task<List<SiteAreaViewModel>> GetSiteAreaList(int parentID);

        Task<List<SiteArea>> GetSiteAreaList(SiteAreaRequestParameterModel model);
    }
}
