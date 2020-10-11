using System.Collections.Generic;
using System.Threading.Tasks;
using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.Model;
using Microsoft.Extensions.Options;
using SqlSugar;

namespace BestPracticeWeb.WebApi.Repository
{
    public class SiteAreaRepository : BaseRepository<SiteArea>, ISiteAreaRepository
    {
        public SiteAreaRepository(IOptions<ConnectionStringsOptions> options) : base(options.Value.SQLConnection.BestPracticeWeb)
        {

        }

        public async Task<List<SiteArea>> QuerySiteAreaListByParameter(SiteAreaRequestParameterModel model)
        {
            return await Task.Run(() => Db.Queryable<SiteArea>()
                    .WhereIF(model.ParentID != null, (a1) => a1.ParentID == UtilConvert.ObjToInt(model.ParentID))
                    .WhereIF(model.IsActive != null, (a1) => a1.IsActive == UtilConvert.ObjToInt(model.IsActive))
                    .WhereIF(!string.IsNullOrEmpty(model.Type), (a1) => a1.Type == model.Type)
                    .Select<SiteArea>()
                    .ToList());
        }

        public async Task<PageModel<SiteAreaViewModel>> QuerySiteAreaListMulti(SiteAreaRequestModel requestModel)
        {
            int totolcount = 0;

            List<SiteAreaViewModel> list = await Task.Run(() => Db.Queryable<SiteArea>()
             .WhereIF(requestModel.ParentID != null, (a1) => a1.ParentID == requestModel.ParentID)
             .Select((a1) => new SiteAreaViewModel
             {
                 ID = a1.ID,
                 ParentID = a1.ParentID,
                 Value = a1.Value,
                 IsActive = a1.IsActive,
                 IsActiveName = SqlFunc.IIF(a1.IsActive == 0, "启用", "禁用"),
                 Type = a1.Type,
                 Icon=a1.Icon,
                 ChildrenID = SqlFunc.Subqueryable<SiteArea>().Where(s => s.ParentID == a1.ID).Select(s => s.ID)
             })
             .ToPageList(requestModel.PageIndex, requestModel.PageSize, ref totolcount));

            PageModel<SiteAreaViewModel> pageModel = new PageModel<SiteAreaViewModel>()
            {
                TotalCount = totolcount,
                List = list,
                PageIndex = requestModel.PageIndex,
                PageSize = requestModel.PageSize
            };

            return pageModel;
        }

        public async Task<List<SiteAreaViewModel>> QuerySiteAreaListMulti(int? parentID)
        {
            return await Task.Run(() => Db.Queryable<SiteArea>()
                  .WhereIF(parentID != null, (a1) => a1.ParentID == parentID)
                  .Select((a1) => new SiteAreaViewModel
                  {
                      ID = a1.ID,
                      ParentID = a1.ParentID,
                      Value = a1.Value,
                      IsActive = a1.IsActive,
                      IsActiveName = SqlFunc.IIF(a1.IsActive == 0, "启用", "禁用"),
                      Type = a1.Type,
                      Icon=a1.Icon,
                      ChildrenID = SqlFunc.Subqueryable<SiteArea>().Where(s => s.ParentID == a1.ID).Select(s => s.ID)
                  })
                  .ToList());
        }
    }
}
