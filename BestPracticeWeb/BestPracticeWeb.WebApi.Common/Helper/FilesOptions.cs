using System;
using System.Collections.Generic;
using System.Text;

namespace BestPracticeWeb.WebApi.Common
{
    public class FilesOptions
    {
        /// <summary>
        /// 允许的文件类型
        /// </summary>
        public string FileTypes { get; set; }
        /// <summary>
        /// 最大文件大小
        /// </summary>
        public int MaxSize { get; set; }
        /// <summary>
        /// 基地址
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Host
        /// </summary>
        public string BaseHost { get; set; }
    }
}
