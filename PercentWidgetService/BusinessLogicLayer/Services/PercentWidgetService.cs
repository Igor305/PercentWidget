using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PercentWidgetService : IPercentWidgetService
    {
        private readonly IExecutionPlanDateHistoryRepository _executionPlanDateHistoryRepository;

        public PercentWidgetService(IExecutionPlanDateHistoryRepository executionPlanDateHistoryRepository)
        {
            _executionPlanDateHistoryRepository = executionPlanDateHistoryRepository;
        }

        public async Task<string> getPercent()
        {
            decimal? percent = await _executionPlanDateHistoryRepository.getPercent();
            
            if (percent == null)
            {
                percent = 0;
            }

            string strPercent = Math.Truncate(percent ?? 0).ToString();

            return strPercent;
        }
    }
}
