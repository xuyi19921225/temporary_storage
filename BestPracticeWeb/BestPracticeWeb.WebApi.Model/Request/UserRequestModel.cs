namespace BestPracticeWeb.WebApi.Model
{
    public class UserRequestModel: BaseModel
    {
        /// <summary>
        /// NTID
        /// </summary>
        public string NTID { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string JobNumber { get; set; }

        /// <summary>
        /// Division
        /// </summary>
        public int? DivisionID { get; set; }

        /// <summary>
        /// Site
        /// </summary>
        public int? SiteID { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department1 { get; set; }
    }
}
