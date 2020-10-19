using System.Threading.Tasks;
using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.IService;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Service.BASE;

namespace FinanceInvoiceCompare.WebApi.Service
{
    public class UserService : BaseServices<User>, IUserService
    {
        private readonly IUserRepository _dal;
        private readonly ICompanyRepository companyRepository;

        public UserService(IUserRepository dal, ICompanyRepository companyRepository)
        {
            this._dal = dal;
            this.companyRepository = companyRepository;
            base.BaseDal = dal;
        }

        /// <summary>
        /// 获取用户信息和授权公司
        /// </summary>
        /// <param name="ntid"></param>
        /// <returns></returns>
        public async Task<sysUserInfo> GetSysUserInfo(string ntid)
        {
            var sysUserInfo = await _dal.GetSysUserInfo(ntid);

            sysUserInfo.Code = await companyRepository.GetCompanyCode(sysUserInfo.UserID);

            return sysUserInfo;
        }
    }
}
