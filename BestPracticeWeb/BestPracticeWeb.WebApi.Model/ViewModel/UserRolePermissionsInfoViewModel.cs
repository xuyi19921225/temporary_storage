using SqlSugar;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BestPracticeWeb.WebApi.Model
{
    public class UserRolePermissionsInfoViewModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 所属部门ID
        /// </summary>
        public int DivisionID { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public string Division { get; set; }

        /// <summary>
        /// 所属厂区ID
        /// </summary>
        public int SiteID { get; set; }

        /// <summary>
        /// 所属厂区
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        ///  域账号
        /// </summary>
        public string NTID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        public string RoleName { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public List<MenuViewModel> MenuList
        {
            get; set;
        }


    }
}
