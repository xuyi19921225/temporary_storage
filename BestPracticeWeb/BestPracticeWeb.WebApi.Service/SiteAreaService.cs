using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.Service
{
    public class SiteAreaService : BaseServices<SiteArea>, ISiteAreaService
    {
        private readonly ISiteAreaRepository siteAreaRepository;

        public SiteAreaService(ISiteAreaRepository siteAreaRepository)
        {
            this.siteAreaRepository = siteAreaRepository;
            base.BaseDal = siteAreaRepository;
        }

        /// <summary>
        /// 分页查询SiteArea信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageModel<SiteAreaViewModel>> GetSiteAreaList(SiteAreaRequestModel model)
        {
            return await siteAreaRepository.QuerySiteAreaListMulti(model);
        }

        /// <summary>
        /// 查询SiteArea信息
        /// </summary>
        /// <param name="parentID">父级ID</param>
        /// <returns></returns>
        public async Task<List<SiteAreaViewModel>> GetSiteAreaList(int parentID)
        {
            return await siteAreaRepository.QuerySiteAreaListMulti(parentID);
        }


        /// <summary>
        /// 根据参数获取SiteArea信息
        /// </summary>
        /// <param name="model">参数实体</param>
        /// <returns></returns>
        public async Task<List<SiteArea>> GetSiteAreaList(SiteAreaRequestParameterModel model)
        {
            return await siteAreaRepository.QuerySiteAreaListByParameter(model);
        }
    }
}
