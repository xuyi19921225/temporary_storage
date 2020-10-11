using BestPracticeWeb.WebApi.Common;
using Microsoft.AspNetCore.Http;

namespace BestPracticeWeb.WebApi.IService
{
    public interface IFileService
    {
        byte[] SingleDownload(string url);
    }
}
