using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IExecutionPlanDateHistoryRepository
    {
        public Task<decimal?> getPercent();
    }
}
