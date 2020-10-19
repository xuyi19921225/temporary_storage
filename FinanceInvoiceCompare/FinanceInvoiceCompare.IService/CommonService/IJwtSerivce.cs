using FinanceInvoiceCompare.WebApi.Model;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.IService
{
    public interface IJwtSerivce
    {
        Task<string> GenerateToken(LoginRequestModel model);

        TokenModelJwt SerializeJwt(string token);
    }
}
