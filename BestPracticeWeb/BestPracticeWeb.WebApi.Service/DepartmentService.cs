using BestPracticeWeb.WebApi.IRepository;
using BestPracticeWeb.WebApi.IService;
using BestPracticeWeb.WebApi.Model;

namespace BestPracticeWeb.WebApi.Service
{
    public class DepartmentService:BaseServices<Department>,IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
            base.BaseDal = departmentRepository;
        }
    }
}
