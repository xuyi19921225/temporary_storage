using System.Collections.Generic;
using System.Threading.Tasks;
using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.Model;
using Microsoft.Extensions.Options;

namespace BestPracticeWeb.WebApi.Repository
{
    public class ProjectHistoryRepository:BaseRepository<ProjectHistory>, IProjectHistoryRepository
    {
        public ProjectHistoryRepository(IOptions<ConnectionStringsOptions> options) : base(options.Value.SQLConnection.BestPracticeWeb)
        {

        }

        public async Task<PageModel<ProjectHistory>> QueryProjectHistoryList(ProjectHistoryRequestModel model)
        {
            int totolcount = 0;


            List<ProjectHistory> list = await Task.Run(() => Db.Queryable<ProjectHistory>()
             .WhereIF(model.ParentID != null, a1 => a1.ParentID == model.ParentID)
             .Select<ProjectHistory>()
             .ToPageList(model.PageIndex, model.PageSize, ref totolcount));

            PageModel<ProjectHistory> pageModel = new PageModel<ProjectHistory>()
            {
                TotalCount = totolcount,
                List = list,
                PageIndex = model.PageIndex,
                PageSize = model.PageSize
            };

            return pageModel;
        }
    }
}
