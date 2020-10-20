using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Model
{
    public class PageModel<T>
    {
        [Required(ErrorMessage = "页码不能为空")]
        public int PageIndex { get; set; }

        [Required(ErrorMessage = "每页数量不能为空")]
        public int PageSize { get; set; }

        /// <summary>
        /// 数据总数
        /// </summary>
        public int TotalCount { get; set; } = 0;

        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> List { get; set; }
    }
}
