using Microsoft.AspNetCore.Mvc.Filters;

namespace FinanceInvoiceCompare.WebApi.Middlewares
{
    /// <summary>
    /// 验证请求实体
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }
}
