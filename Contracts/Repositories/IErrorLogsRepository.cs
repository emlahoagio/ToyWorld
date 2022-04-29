using Entities.Models;

namespace Contracts.Repositories
{
    public interface IErrorLogsRepository
    {
        void LogError(ErrorLogs error);
    }
}
