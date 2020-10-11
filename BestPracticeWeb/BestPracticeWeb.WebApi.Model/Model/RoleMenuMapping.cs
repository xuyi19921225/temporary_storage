using System;
using System.Collections.Generic;
using System.Text;

namespace BestPracticeWeb.WebApi.Model
{
    /// <summary>
    /// 角色和菜单关联表
    /// </summary>
    public class RoleMenuMapping:OperateBaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuID { get; set; }
    }
}
