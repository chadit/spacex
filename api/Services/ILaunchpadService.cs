using System.Collections.Generic;
using System.Threading.Tasks;
using spacex.api.Models;

namespace spacex.api.Services
{
    public interface ILaunchpadService
    {
        Task<Launchpad> GetBySiteID(string siteID);
        Task<IList<Launchpad>> Get(int? limit, int? offset);
    }
}