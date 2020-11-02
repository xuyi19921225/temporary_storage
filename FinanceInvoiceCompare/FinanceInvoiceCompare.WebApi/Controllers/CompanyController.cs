using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceInvoiceCompare.WebApi.Controllers
{
    /// <summary>
    /// 公司操作
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }


        /// <summary>
        /// 获取公司信息(分页)
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<PageModel<Company>>> Get([FromQuery] CompanyRequestModel model)
        {
            return new MessageModel<PageModel<Company>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await companyService.QueryPage(a => a.IsDelete == false, model.PageIndex, model.PageSize, " ID desc ")
            };
        }


        /// <summary>
        /// 添加公司信息
        /// </summary>
        /// <param name="companys">公司信息</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post([FromBody] List<Company> companys)
        {
            var data = new MessageModel<string>();

            if (companys.Count > 0)
            {

                var companyCodes = companys.Select(x => x.Code).ToArray();

                //// 检查是否存在相同的VendorCode
                var exsitsVendors = (await companyService.Query(x => x.IsDelete == false && companyCodes.Contains(x.Code)));

                bool flag = false;

                if (exsitsVendors.Count > 0)
                {
                    data.Success = flag;
                    data.Message = "存在相同的CompanyCode，请检查";
                }
                else
                {
                    data.Success = flag = await companyService.Add(companys) > 0;

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
        /// 更新一个公司
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<MessageModel<string>> Put([FromBody]Company model)
        {
            var data = new MessageModel<string>();

            var flag = data.Success = await companyService.Update(model);

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
        /// 删除一个公司
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
                var companyDetail = await companyService.QueryById(id);
                companyDetail.IsDelete = true;

                var flag = data.Success = await companyService.Update(companyDetail);

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


        /// <summary>
        /// 获取所有公司信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetAllCompanyList")]
        public async Task<MessageModel<List<Company>>> GetAllCompanyList()
        {
            return new MessageModel<List<Company>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await companyService.Query(x => x.IsDelete == false)
            };
        }


        /// <summary>
        ///根据CompanyId获取Campany信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("GetCompanyById")]
        public async Task<MessageModel<List<Company>>> GetCompanyById([FromQuery] object[] ids)
        {
            return new MessageModel<List<Company>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await companyService.QueryByIDs(ids)
            };
        }

    }
}
