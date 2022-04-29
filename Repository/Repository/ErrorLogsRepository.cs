using Contracts.Repositories;
using Entities.Models;

namespace Repository.Repository
{
    public class ErrorLogsRepository : RepositoryBase<ErrorLogs>, IErrorLogsRepository
    {
        public ErrorLogsRepository(DataContext context) : base(context)
        {
        }

        public void LogError(ErrorLogs error) => Create(error);
    }
}
