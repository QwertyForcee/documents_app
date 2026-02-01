using DocumentsAPI.Api.Extensions;
using DocumentsAPI.Core.Statistics.Interfaces;
using DocumentsAPI.Core.Statistics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsAPI.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatisticsController : ControllerBase
    {
        private readonly IUserStatisticsService _statisticsService;

        public UserStatisticsController(IUserStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserStatistics>>> GetUserStatistics()
        {
            var userId = User.GetUserId();
            var stats = await _statisticsService.GetStatisticsByUserIdAsync(userId);
            return Ok(stats);
        }
    }
}
