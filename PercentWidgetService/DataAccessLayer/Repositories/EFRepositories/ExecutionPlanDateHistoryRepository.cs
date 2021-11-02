using DataAccessLayer.AppContext.Avrora37;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.EFRepositories
{
    public class ExecutionPlanDateHistoryRepository : IExecutionPlanDateHistoryRepository
    {
        private readonly Avrora37Context _avrora37Context;

        public ExecutionPlanDateHistoryRepository(Avrora37Context avrora37Context)
        {
            _avrora37Context = avrora37Context;
        }

        public async Task<decimal?> getPercent()
        {
            decimal? percent = await _avrora37Context.ExecutionPlanDateHistories.OrderByDescending(x => x.Dates).Select(x=>x.ExecutionPlanToDatePercent).FirstOrDefaultAsync();

            return percent;
        }
    }
}
