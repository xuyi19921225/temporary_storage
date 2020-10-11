using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticeWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteAreaController : ControllerBase
    {
        private readonly ISiteAreaService siteAreaService;

        public SiteAreaController(ISiteAreaService siteAreaService)
        {
            this.siteAreaService = siteAreaService;
        }

        /// <summary>
        /// 根据条件获取SiteArea表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetSiteAreaListByParameter")]
        public async Task<MessageModel<List<SiteArea>>> GetSiteAreaListByParameter([FromQuery]SiteAreaRequestParameterModel model)
        {
            return new MessageModel<List<SiteArea>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await siteAreaService.GetSiteAreaList(model)
            };
        }


        /// <summary>
        /// 根据ParentID获取所有数据（带分页）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<PageModel<SiteAreaViewModel>>> Get([FromQuery]SiteAreaRequestModel model)
        {
            return new MessageModel<PageModel<SiteAreaViewModel>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await siteAreaService.GetSiteAreaList(model)
            };
        }

        /// <summary>
        /// 根据ParentID获取所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetSiteAreaList")]
        public async Task<MessageModel<List<SiteAreaViewModel>>> GetSiteAreaList([FromQuery]int parentID)
        {
            return new MessageModel<List<SiteAreaViewModel>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await siteAreaService.GetSiteAreaList(parentID)
            };
        }

        /// <summary>
        /// 创建SiteArea字典
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post([FromBody]SiteArea model)
        {
            List<SiteArea> siteAreas = await siteAreaService.Query(i => i.Value == model.Value && i.ParentID == model.ParentID);

            //存在相同数据
            if (siteAreas.Count > 0)
            {
                return new MessageModel<string>()
                {
                    Success = false,
                    Message = "同类型的字典下不允许存在相同值",
                };
            }
            else
            {
                model.CreateAt = DateTime.Now;
                bool flag = await siteAreaService.Add(model)>0;

                if (flag)
                {
                    return new MessageModel<string>()
                    {
                        Success = flag,
                        Message = "创建成功",
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        Success = flag,
                        Message = "创建失败",
                    };
                }
            }
        }

        /// <summary>
        /// 更新SiteArea字典
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<MessageModel<string>> Put([FromBody]SiteArea model)
        {
            List<SiteArea> siteAreas = await siteAreaService.Query(i => i.Value == model.Value && i.ParentID == model.ParentID && i.ID != model.ID);

            //存在相同数据
            if (siteAreas.Count > 0)
            {
                return new MessageModel<string>()
                {
                    Success = false,
                    Message = "同类型的字典下不允许存在相同值",
                };
            }
            else
            {
                model.UpdatedAt = DateTime.Now;
                bool flag = await siteAreaService.Update(model);

                if (flag)
                {
                    return new MessageModel<string>()
                    {
                        Success = flag,
                        Message = "更新成功",
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        Success = flag,
                        Message = "更新失败",
                    };
                }
            }
        }
    }
}