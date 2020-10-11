using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.Model;
using Microsoft.Extensions.Options;

namespace BestPracticeWeb.WebApi.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IOptions<ConnectionStringsOptions> options): base(options.Value.SQLConnection.BestPracticeWeb)
        {

        }
    }
}
