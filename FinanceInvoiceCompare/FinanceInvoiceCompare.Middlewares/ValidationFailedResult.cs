using FinanceInvoiceCompare.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FinanceInvoiceCompare.WebApi.Middlewares
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
              : base(new ValidationFailedResultModel(modelState))
        {
            //StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
