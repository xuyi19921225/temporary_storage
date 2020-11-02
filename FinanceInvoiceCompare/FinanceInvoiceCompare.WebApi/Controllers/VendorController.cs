using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceInvoiceCompare.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService vendorService;

        public VendorController(IVendorService vendorService)
        {
            this.vendorService = vendorService;
        }


        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<PageModel<Vendor>>> Get([FromQuery] VendorRequestModel model)
        {
            return new MessageModel<PageModel<Vendor>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await vendorService.QueryPage(a => a.IsDelete == false, model.PageIndex, model.PageSize, " ID desc ")
            };
        }

        /// <summary>
        /// 添加供应商信息
        /// </summary>
        /// <param name="vendors">供应商信息</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post([FromBody] List<Vendor> vendors)
        {
            var data = new MessageModel<string>();

            if (vendors.Count > 0)
            {

                var vendorCodes = vendors.Select(x => x.VendorCode).ToArray();

                //// 检查是否存在相同的VendorCode
                var exsitsVendors = (await vendorService.Query(x => x.IsDelete==false && vendorCodes.Contains(x.VendorCode)));

                bool flag = false;

                if (exsitsVendors.Count > 0)
                {
                    data.Success = flag;
                    data.Message = "存在相同的VendorCode，请检查";
                }
                else
                {
                    data.Success = flag = await vendorService.Add(vendors) > 0;

                    if (flag)
                    {
                        data.Message = "添加成功";
                    }
                    else
                    {
                        data.Message = "添加失败";
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// 更新一个供应商
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<MessageModel<string>> Put([FromBody]Vendor model)
        {
            var data = new MessageModel<string>();

            var flag = data.Success = await vendorService.Update(model);
            if (flag)
            {
                data.Message = "更新成功";
            }
            else
            {
                data.Message = "更新失败";
            }

            return data;
        }


        /// <summary>
        /// 删除一个供应商
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<MessageModel<string>> Delete([FromQuery] int id)
        {
            var data = new MessageModel<string>();
            if (id > 0)
            {
                var vendorDetail = await vendorService.QueryById(id);
                vendorDetail.IsDelete = true;

                var flag = data.Success = await vendorService.Update(vendorDetail);

                if (flag)
                {
                    data.Message = "删除成功";
                }
                else
                {
                    data.Message = "删除失败";
                }

            }
            return data;
        }
    }
}