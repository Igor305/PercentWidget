using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PercentWindgetService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PercentController : ControllerBase
    {
        private readonly IPercentWidgetService _percentWidgetService;

        public PercentController(IPercentWidgetService percentWidgetService)
        {
            _percentWidgetService = percentWidgetService;
        }

        [HttpGet]
        public async Task<string> getPercent()
        {
            string percent = await _percentWidgetService.getPercent();

            return percent;
        }
    }
}
