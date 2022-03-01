using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IErrorLogsRepository
    {
        void LogError(ErrorLogs error);
    }
}
