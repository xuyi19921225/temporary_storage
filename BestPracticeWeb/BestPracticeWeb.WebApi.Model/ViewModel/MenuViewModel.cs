namespace BestPracticeWeb.WebApi.Model
{
    public class MenuViewModel:OperateBaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 重定向
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }


        /// <summary>
        /// 是否隐藏
        /// </summary>
        public bool Hidden { get; set; }
    }
}
