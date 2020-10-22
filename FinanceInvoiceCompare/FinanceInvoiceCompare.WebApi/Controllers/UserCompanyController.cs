using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.Common;
using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FinanceInvoiceCompare.WebApi.Controllers
{
    /// <summary>
    /// 用户和公司操作
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserCompanyController : ControllerBase
    {
        private readonly IUserCompanyService userCompanyService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<UserCompanyController> logger;

        public UserCompanyController(IUserCompanyService userCompanyService, IUnitOfWork unitOfWork, ILogger<UserCompanyController> logger)
        {
            this.userCompanyService = userCompanyService;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        /// <summary>
        /// 保存用户和公司的授权关系
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post([FromBody]UserCompanyMapping model)
        {
            var data = new MessageModel<string>();
            try
            {
                unitOfWork.BeginTran();
                if (model != null && model.UserID > 0)
                {

                    // 无论 Update Or Add , 先删除当前用户的全部 U_C 关系
                    var companys = (await userCompanyService.Query(d => d.UserID == model.UserID)).Select(d => d.ID.ToString()).ToArray();

                    bool isAllDeleted;

                    if (companys.Length > 0)
                    {
                        ////删除关联关系
                        isAllDeleted = await userCompanyService.DeleteByIds(companys);
                    }
                    else
                    {
                        isAllDeleted = true;
                    }


                    var userCompany = new List<UserCompanyMapping>();
                    //// 如果没有添加的关系 删除关系结果为最终结果 
                    var flag = isAllDeleted;

                    if (model.list != null && model.list.Count > 0)
                    {
                        model.list.ForEach(x =>
                        {
                            userCompany.Add(new UserCompanyMapping() { UserID = model.UserID, CompanyID = x.Id, CreateAt = DateTime.Now, CreateBy = x.CreateBy });
                        });
                        ////添加关系
                        flag = await userCompanyService.Add(userCompany) > 0;
                    }

                    unitOfWork.CommitTran();

                    data.Success = flag;
                    if (flag)
                    {
                        data.Message = "保存成功";
                    }
                    else
                    {
                        data.Message = "保存失败";
                    }
                }
            }
            catch (Exception ex)
            {
                unitOfWork.RollbackTran();
                logger.LogError(ex, ex.Message);
            }




            return data;
        }


        /// <summary>
        /// 获取用户已选公司信息
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<MessageModel<List<int>>> Get([FromQuery]int userId)
        {
            return new MessageModel<List<int>>()
            {
                Message = "获取信息成功",
                Success = true,
                Response = (await userCompanyService.GetUCompanyList()).Where(x=>x.UId==userId).Select(y=>y.Id).ToList()
            };
        }
    }
}