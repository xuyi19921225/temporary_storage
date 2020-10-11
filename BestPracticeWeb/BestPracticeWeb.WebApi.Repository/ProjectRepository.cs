using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.Model;
using Microsoft.Extensions.Options;
using SqlSugar;

namespace BestPracticeWeb.WebApi.Repository
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IOptions<ConnectionStringsOptions> options) : base(options.Value.SQLConnection.BestPracticeWeb)
        {


        }

        public bool AddProjectMulti(ProjectSaveRequestModel model)
        {
            try
            {
                Db.BeginTran();

                int id = Db.Insertable(new Project()
                {
                    DivisionID = model.DivisionID.ObjToInt(),
                    SiteID = model.SiteID.ObjToInt(),
                    StationID = model.StationID.ObjToInt(),
                    Department = model.Department,
                    Description = model.Description,
                    FileName = model.FileName,
                    Attachment = model.Attachment,
                    ProjectTitle = model.ProjectTitle,
                    CreateAt = model.CreateAt,
                    CreateBy = model.CreateBy
                }).ExecuteReturnIdentity();

                int j = Db.Insertable(new ProjectHistory() { ParentID = id, Action = "Create", CreateAt = model.CreateAt, CreateBy = model.CreateBy }).ExecuteCommand();

                Db.CommitTran();

                return j > 0;
            }
            catch (System.Exception ex)
            {
                Db.RollbackTran();
                throw ex;
            }
        }


        public bool DeleteProjectMulti(Project model)
        {
            try
            {
                Db.BeginTran();

                int id = Db.Updateable(new Project()
                {
                    IsDelete = model.IsDelete,
                    UpdatedAt = model.UpdatedAt,
                    UpdatedBy = model.UpdatedBy
                }).UpdateColumns(i => new { i.IsDelete, i.UpdatedAt, i.UpdatedBy }).Where(i => i.ID == model.ID).ExecuteCommand();

                int j = Db.Insertable(new ProjectHistory() { ParentID = model.ID, Action = "Delete", CreateAt = model.UpdatedAt.ObjToDate(), CreateBy = model.UpdatedBy }).ExecuteCommand();

                Db.CommitTran();

                return j > 0;
            }
            catch (System.Exception ex)
            {
                Db.RollbackTran();
                throw ex;
            }
        }

        public bool UpdateProjectMulti(Project model)
        {
            try
            {
                Db.BeginTran();

                int id = Db.Updateable(new Project()
                {
                    DivisionID = model.DivisionID.ObjToInt(),
                    SiteID = model.SiteID.ObjToInt(),
                    StationID = model.StationID.ObjToInt(),
                    Department = model.Department,
                    Description = model.Description,
                    FileName = model.FileName,
                    Attachment = model.Attachment,
                    ProjectTitle = model.ProjectTitle,
                    UpdatedAt = model.UpdatedAt,
                    UpdatedBy = model.UpdatedBy
                }).IgnoreColumns(new string[] { "CreateAt", "CreateBy", "IsDelete" }).Where(i => i.ID == model.ID).ExecuteCommand();

                int j = Db.Insertable(new ProjectHistory() { ParentID = model.ID, Action = "Update", CreateAt = model.UpdatedAt.ObjToDate(), CreateBy = model.UpdatedBy }).ExecuteCommand();

                Db.CommitTran();

                return j > 0;
            }
            catch (System.Exception ex)
            {
                Db.RollbackTran();
                throw ex;
            }
        }


        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageModel<ProjectViewModel>> QueryProjectListMulti(ProjectRequestModel model)

        {
            int totolcount = 0;


            List<ProjectViewModel> list = await Task.Run(() => Db.Queryable<Project, SiteArea, SiteArea, SiteArea>((a1, b2, c3, d4) => new object[]
                {
                JoinType.Left,a1.SiteID==b2.ID,
                JoinType.Left,a1.DivisionID==c3.ID,
                JoinType.Left,a1.StationID==d4.ID
                })
             .WhereIF(model.SiteID != null, a1 => a1.SiteID == model.SiteID)
             .WhereIF(model.DivisionID != null, a1 => a1.DivisionID == model.DivisionID)
             .WhereIF(model.StationID != null, a1 => a1.StationID == model.StationID)
             .WhereIF(model.IsDelete != null, a1 => a1.IsDelete == model.IsDelete)
             .Select((a1, b2, c3, d4) => new ProjectViewModel
             {
                 ID = a1.ID,
                 ProjectTitle = a1.ProjectTitle,
                 DivisionID = a1.DivisionID,
                 Division = c3.Value,
                 Site = b2.Value,
                 SiteID = a1.SiteID,
                 StationID = a1.StationID,
                 Station = d4.Value,
                 Department = a1.Department,
                 Attachment = a1.Attachment,
                 FileName = a1.FileName,
                 Description = a1.Description,
                 IsDelete = a1.IsDelete,
                 IsDeleteString = SqlFunc.IIF(a1.IsDelete == 0, "使用", "已删除")
             })
             .ToPageList(model.PageIndex, model.PageSize, ref totolcount));

            PageModel<ProjectViewModel> pageModel = new PageModel<ProjectViewModel>()
            {
                TotalCount = totolcount,
                List = list,
                PageIndex = model.PageIndex,
                PageSize = model.PageSize
            };

            return pageModel;
        }


        /// <summary>
        /// 获取报表信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageModel<ProjectViewModel>> QueryProjectListMulti(DashboardRequestModel model)
        {
            int totolcount = 0;


            List<ProjectViewModel> list = await Task.Run(() => Db.Queryable<Project, SiteArea, SiteArea, SiteArea>((a1, b2, c3, d4) => new object[]
                {
                JoinType.Left,a1.SiteID==b2.ID,
                JoinType.Left,a1.DivisionID==c3.ID,
                JoinType.Left,a1.StationID==d4.ID
                })
             .WhereIF(model.SiteID != null, a1 => a1.SiteID == model.SiteID)
             .WhereIF(model.DivisionID != null, a1 => a1.DivisionID == model.DivisionID)
             .WhereIF(model.StationID != null, a1 => a1.StationID == model.StationID)
             .WhereIF(!string.IsNullOrEmpty(model.Department), a1 => a1.Department==model.Department)
             .WhereIF(!string.IsNullOrEmpty(model.ProjectTitle), a1 => a1.ProjectTitle.Contains(model.ProjectTitle))
             .Select((a1, b2, c3, d4) => new ProjectViewModel
             {
                 ID = a1.ID,
                 ProjectTitle = a1.ProjectTitle,
                 DivisionID = a1.DivisionID,
                 Division = c3.Value,
                 Site = b2.Value,
                 SiteID = a1.SiteID,
                 StationID = a1.StationID,
                 Station = d4.Value,
                 Department = a1.Department,
                 Attachment = a1.Attachment,
                 FileName = a1.FileName,
                 Description = a1.Description,
                 IsDelete = a1.IsDelete,
                 IsDeleteString = SqlFunc.IIF(a1.IsDelete == 0, "使用", "已删除"),
                 CreateAt=a1.CreateAt,
                 CreateBy=a1.CreateBy
             })
             .ToPageList(model.PageIndex, model.PageSize, ref totolcount));

            PageModel<ProjectViewModel> pageModel = new PageModel<ProjectViewModel>()
            {
                TotalCount = totolcount,
                List = list,
                PageIndex = model.PageIndex,
                PageSize = model.PageSize
            };

            return pageModel;
        }
    }
}
