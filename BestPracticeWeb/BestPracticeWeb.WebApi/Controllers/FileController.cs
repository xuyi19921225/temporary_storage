using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace BestPracticeWeb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService uploadService;
        private readonly FilesOptions options;

        public FileController(IFileService uploadService, IOptions<FilesOptions> options)
        {
            this.uploadService = uploadService;
            this.options = options.Value;
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Authorize]
        public MessageModel<FileAttribute> Post([FromForm] IFormFile file)
        {
            if (file.Length <= this.options.MaxSize)//检查文件大小
            {
                var suffix = Path.GetExtension(file.FileName);//提取上传的文件文件后缀

                if (this.options.FileTypes.IndexOf(suffix) >= 0)//检查文件格式
                {
                    var id = Guid.NewGuid();
                    using (FileStream fs = System.IO.File.Create($@"{this.options.BaseUrl}\{id}{suffix}"))
                    {
                        file.CopyTo(fs);//将上传的文件文件流，复制到fs中
                        fs.Flush();//清空文件流
                    }

                    return new MessageModel<FileAttribute>()
                    {
                        Success = true,
                        Message = "上传成功",
                        Response = new FileAttribute()
                        {
                            FileName = file.FileName,
                            Url = $"{options.BaseHost}{id}{suffix}"
                        }
                    };
                }
                else
                {
                    return new MessageModel<FileAttribute>()
                    {
                        Success = false,
                        Message = "不支持此文件类型"
                    };
                }
            }
            else
            {
                return new MessageModel<FileAttribute>()
                {
                    Success = false,
                    Message = $"文件大小不得超过{this.options.MaxSize / (1024f * 1024f)}M"
                };
            }
        }



        /// <summary>
        /// 下载文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("download")]
        public FileResult Post(string attachment)
        {
            byte[] fileBytes = uploadService.SingleDownload(attachment);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet);
        }


    }
}