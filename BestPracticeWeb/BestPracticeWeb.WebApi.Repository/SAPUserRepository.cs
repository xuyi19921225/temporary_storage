using BestPracticeWeb.WebApi.Common;
using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.Model;
using Microsoft.Extensions.Options;

namespace BestPracticeWeb.WebApi.Repository
{
    public class SAPUserRepository:BaseRepository<SAPUser>, ISAPUserRepository
    {
        public SAPUserRepository(IOptions<ConnectionStringsOptions> options) : base(options.Value.SQLConnection.eFactory)
        {
        }
    }
}
