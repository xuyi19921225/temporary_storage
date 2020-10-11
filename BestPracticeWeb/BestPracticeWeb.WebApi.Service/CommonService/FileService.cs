using System;
using System.IO;
using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BestPracticeWeb.WebApi.Service
{
    public class FileService : IFileService
    {
        private readonly FilesOptions options;

        public FileService(IOptions<FilesOptions> options)
        {
            this.options = options.Value;
        }

        public byte[] SingleDownload(string attachment)
        {
            var addrUrl = $"{options.BaseUrl}/{attachment}";
            byte[] fileBytes = System.IO.File.ReadAllBytes(addrUrl);
            return fileBytes;
        }
    }
}
