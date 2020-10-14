using FinanceInvoiceCompare.WebApi.Model;

namespace FinanceInvoiceCompare.WebApi.IService
{
    public interface ILDAPService
    {
        bool ValidADUser(LoginRequestModel Model);
    }
}
