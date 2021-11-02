using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IPercentWidgetService
    {
        public Task<string> getPercent();
    }
}
