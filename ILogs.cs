using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zrcwaw_l2
{
    public interface ILogs
    {
        Task<Models.LogsData[]> GetLogs();
        Task<bool> InsertLog(Models.LogsData logsData);
    }
}
