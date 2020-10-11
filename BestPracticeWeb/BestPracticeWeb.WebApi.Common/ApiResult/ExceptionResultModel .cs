using System;

namespace BestPracticeWeb.WebApi.Common
{
    public class ExceptionResultModel : MessageModel<string>
    {
        public ExceptionResultModel(int? code, Exception exception)
        {
            Status =Convert.ToInt32(code);
            Success = false;
            Message = exception.InnerException != null ?
                exception.InnerException.Message :
                exception.Message;
            Response = exception.Message;
        }
    }
}
