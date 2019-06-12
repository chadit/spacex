using System.Collections.Generic;
using System.Threading.Tasks;
using spacex.api.Repositories.Interfaces;
using spacex.api.Models;

namespace spacex.api.Services
{
    public class LaunchpadService : ILaunchpadService
    {
        private ILaunchpadRepository _launchpadRepository;

        public LaunchpadService(ILaunchpadRepository launchpadRepository)
        {
            _launchpadRepository = launchpadRepository;
        }

        public async Task<Launchpad> GetBySiteID(string siteID)
        {
            return await _launchpadRepository.GetBySiteID(siteID);
        }

        public async Task<IList<Launchpad>> Get(int? limit, int? offset)
        {
            return await _launchpadRepository.Get(limit, offset);
        }
    }
}