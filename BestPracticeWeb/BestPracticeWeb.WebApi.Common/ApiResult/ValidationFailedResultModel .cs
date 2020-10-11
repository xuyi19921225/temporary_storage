using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BestPracticeWeb.WebApi.Common
{
    public class ValidationFailedResultModel : MessageModel<List<ValidationError>>
    {
        public ValidationFailedResultModel(ModelStateDictionary modelState)
        {
            Status = 422;
            Success = false;
            Message = "参数不合法";
            Response = modelState.Keys
                        .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                        .ToList();
        }
    }

    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }
        public string Message { get; }
        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
