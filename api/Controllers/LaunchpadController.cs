using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using spacex.api.Services;
using spacex.api.Models;
using System.Collections.Specialized;

namespace SpaceX.Api.Controllers
{
    [Route("api/launchpads")]
    [ApiController]
    public class LaunchpadController : ControllerBase
    {
        private ILaunchpadService _launchpadService;
        private ILogger _logger;

        public LaunchpadController(ILaunchpadService launchpadService, ILogger<LaunchpadController> logger)
        {
            _launchpadService = launchpadService;
            _logger = logger;
        }

        // GET api/launchpads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Launchpad>>> Get()
        {
            string query_limit = HttpContext.Request.Query["limit"].ToString();
            string query_offset = HttpContext.Request.Query["offset"].ToString();

            int? limit = null;
            int? offset = null;

            if (!string.IsNullOrWhiteSpace(query_limit))
            {
                limit = int.Parse(query_limit);
            }

            if (!string.IsNullOrWhiteSpace(query_offset))
            {
                offset = int.Parse(query_offset);
            }

            _logger.Log(LogLevel.Information, "get called with query values: limit::{@Limit} offset::{@Offset}", limit, offset);

            var launchpads = await _launchpadService.Get(limit, offset);
            return Ok(launchpads);
        }
    }
}