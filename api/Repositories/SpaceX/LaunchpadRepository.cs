using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using spacex.api.Repositories.Interfaces;
using spacex.api.Models;


namespace spacex.api.Repositories.SpaceX
{
    public class LaunchpadRepository : ILaunchpadRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public LaunchpadRepository(IOptions<AppSettings> appSettings, HttpClient httpClient, ILogger<LaunchpadRepository> logger)
        {
            httpClient.BaseAddress = new Uri(appSettings.Value.SpaceXBaseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Launchpad> GetBySiteID(string siteID)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(siteID))
                {
                    throw new ArgumentException("invalid siteid");
                }

                var path = $"/launchpads/{siteID}";
                var response = await _httpClient.GetAsync(path);
                response.EnsureSuccessStatusCode();

                // parse response
                var content = await response.Content.ReadAsStringAsync();
                var jsonResult = (IEnumerable<dynamic>)JsonConvert.DeserializeObject(content);

                // map to result
                return jsonResult.Select(x => new Launchpad
                {
                    Id = x.id,
                    Name = x.site_name_long,
                    Status = x.status,
                }).First();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error fetching launchpad by siteid from spacex");
                throw;
            }
        }

        public async Task<IList<Launchpad>> Get(int? limit, int? offset)
        {
            try
            {
                // build query params 
                var queryParams = new Dictionary<string, string>();
                if (limit != null)
                {
                    queryParams.Add("limit", limit.ToString());
                }
                if (offset != null)
                {
                    queryParams.Add("offset", offset.ToString());
                }

                var pathAndQuery = QueryHelpers.AddQueryString("/v3/launchpads", queryParams);
                var response = await _httpClient.GetAsync(pathAndQuery);
                response.EnsureSuccessStatusCode();

                // parse response
                var content = await response.Content.ReadAsStringAsync();
                var jsonResult = (IEnumerable<dynamic>)JsonConvert.DeserializeObject(content);

                // map to result
                return jsonResult.Select(x => new Launchpad
                {
                    Id = x.id,
                    Name = x.site_name_long,
                    Status = x.status,
                }).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error fetching launchpad data from spacex");
                throw;
            }
        }
    }
}