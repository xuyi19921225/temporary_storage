using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Model.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace FinanceInvoiceCompare.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCodeController : ControllerBase
    {
        private readonly ITaxCodeService taxCodeService;

        public TaxCodeController(ITaxCodeService taxCodeService)
        {
            this.taxCodeService = taxCodeService;
        }


        /// <summary>
        /// 获取税码信息
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<PageModel<TaxCode>>> Get([FromQuery] TaxCodeRequestModel model)
        {
            var expressions = Expressionable.Create<TaxCode>()
             .And(it => it.IsDelete == false)
             .AndIF(!string.IsNullOrEmpty(model.Code),it=>it.Code.Contains(model.Code)).ToExpression();

            return new MessageModel<PageModel<TaxCode>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await taxCodeService.QueryPage(expressions, model.PageIndex, model.PageSize, " ID desc ")
            };
        }



        /// <summary>
        /// 添加一个税码
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post([FromBody] TaxCode model)
        {
            var data = new MessageModel<string>();


            var taxCode = await taxCodeService.Query(x => x.Code == model.Code && x.IsDelete == false);

            if (taxCode.Count > 0)
            {
                data.Success = false;
                data.Message = "该税码已经存在";
            }
            else
            {
                var flag = data.Success = await taxCodeService.Add(model) > 0;
                if (flag)
                {
                    data.Message = "添加成功";
                }
                else
                {
                    data.Message = "添加失败";
                }

            }
            return data;
        }

        /// <summary>
        /// 更新一个税码
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<MessageModel<string>> Put([FromBody] TaxCode model)
        {
            var data = new MessageModel<string>();



            var flag = data.Success = await taxCodeService.Update(model);
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
        /// 删除一个税码
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
                var taxCodeDetail = await taxCodeService.QueryById(id);
                taxCodeDetail.IsDelete = true;

                var flag = data.Success = await taxCodeService.Update(taxCodeDetail);

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
