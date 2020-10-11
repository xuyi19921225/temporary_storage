using BestPracticeWeb.WebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BestPracticeWeb.WebApi.IRepository
{
    public interface IMenuRepository:IBaseRepository<Menu>
    {
        Task<List<MenuViewModel>> QueryMenuListMulti(int roleID);
    }
}
