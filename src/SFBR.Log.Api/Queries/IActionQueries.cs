using Microsoft.Extensions.Primitives;
using SFBR.Log.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.Queries
{
    public interface IActionQueries
    {
        Task<PageResult<ActionLogModel>> GetActionLog(IEnumerable<KeyValuePair<string, StringValues>> query, int page, int rows);
    }
}
