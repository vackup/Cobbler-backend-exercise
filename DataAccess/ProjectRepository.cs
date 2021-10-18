using DataAccess.Contracts;
using Entities;

namespace DataAccess
{
    public class ProjectRepository : Repository<Project, int>, IProjectRepository
    {
        public ProjectRepository(BudgetAllocatorDbContext context) : base(context)
        {
        }
    }
}