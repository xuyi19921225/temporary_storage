using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace FinanceInvoiceCompare.WebApi.Controllers
{
    /// <summary>
    /// 供应商业务
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService vendorService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<VendorController> logger;

        public VendorController(IVendorService vendorService, IUnitOfWork unitOfWork, ILogger<VendorController> logger)
        {
            this.vendorService = vendorService;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
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
            var expressions = Expressionable.Create<Vendor>()
            .And(it => it.IsDelete == false)
            .AndIF(!string.IsNullOrEmpty(model.CompanyCode), it => it.CompanyCode == model.CompanyCode)
            .AndIF(!string.IsNullOrEmpty(model.VendorCode), it => it.VendorCode == model.VendorCode)
            .ToExpression();

            return new MessageModel<PageModel<Vendor>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = await vendorService.QueryPage(expressions, model.PageIndex, model.PageSize, " ID desc ")
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
                var companyCodes = string.Join(',', vendors.Select(x => string.IsNullOrEmpty(x.CompanyCode) ? "" : x.CompanyCode.PadLeft(4, '0')).ToArray());
                var vendorCodes = string.Join(',', vendors.Select(x => x.VendorCode).ToArray());
                var vendorChNames = string.Join(',', vendors.Select(x => x.VendorChName).ToArray());
                var operators = vendors.First().CreateBy;


                List<SugarParameter> parameters = new List<SugarParameter>()
                {
                    new SugarParameter("@VendorCodes",vendorCodes),
                    new SugarParameter("@VendorChNames",vendorChNames),
                    new SugarParameter("@CompanyCodes",companyCodes),
                    new SugarParameter("@Operator",operators),
                    new SugarParameter("@ResultMessage",string.Empty,true),
                };

                parameters[0].Size = -1;
                parameters[1].Size = -1;
                parameters[2].Size = -1;
                parameters[3].Size = 50;
                parameters[4].Size = -1;

                await vendorService.UseProc("Proc_Export_Vendor", parameters);


                string returnMessage = parameters[4].Value.ToString();

                if (returnMessage == "Success")
                {
                    data.Success = true;
                    data.Message = "添加成功";
                }
                else
                {
                    data.Message = "添加失败";
                }

            }

            //try
            //{
            //    unitOfWork.BeginTran();
            //    if (vendors.Count > 0)
            //    {
            //        //// 检查是否存在相同的VendorCode
            //        //Expressionable<Vendor> exp = new Expressionable<Vendor>();

            //        //foreach (var item in vendors)
            //        //{
            //        //    exp.Or(it => it.IsDelete == false && it.VendorCode == item.VendorCode && it.CompanyCode == item.CompanyCode);
            //        //}

            //        //////这种方式会溢出
            //        //var exsitsVendors = await vendorService.Query(exp.ToExpression());

            //        var allVendors = await vendorService.Query(x => x.IsDelete == false);


            //        bool flag = false;

            //        ////查询需要需要的vendors
            //        var updateVendors = (from a in vendors
            //                             join b in allVendors on new { a.CompanyCode, a.VendorCode } equals new { b.CompanyCode, b.VendorCode } into rb
            //                             from c in rb.DefaultIfEmpty()
            //                             select a).ToList();


            //        if (updateVendors.Count > 0)
            //        {
            //            //// 更新相同的VendorCode和companycode信息
            //            flag = await vendorService.Update(updateVendors, new List<string>() { "VendorChName", "CompanyCode", "VendorCode", "UpdatedAt", "UpdatedBy" }, null, x => new { x.CompanyCode, x.VendorCode });
            //        }

            //        var insertVendors = (from a in vendors
            //                             join b in allVendors on new { a.CompanyCode, a.VendorCode } equals new { b.CompanyCode, b.VendorCode } into rb
            //                             from c in rb.DefaultIfEmpty()
            //                             select c).ToList();

            //        if (insertVendors.Count > 0)
            //        {
            //            //// 插入的Vendor信息
            //            flag = await vendorService.Add(insertVendors) > 0;
            //        }


            //        unitOfWork.CommitTran();

            //        data.Success = flag;
            //        if (flag)
            //        {
            //            data.Message = "添加成功";
            //        }
            //        else
            //        {
            //            unitOfWork.RollbackTran();
            //            data.Message = "添加失败";
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    unitOfWork.RollbackTran();
            //    logger.LogError(ex, ex.Message);
            //}

            return data;
        }

        /// <summary>
        /// 更新一个供应商
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<MessageModel<string>> Put([FromBody] Vendor model)
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