using System.Collections.Generic;
using System.Threading.Tasks;
using spacex.api.Models;

namespace spacex.api.Repositories.Interfaces
{
    public interface ILaunchpadRepository
    {
        Task<Launchpad> GetBySiteID(string siteID);
        Task<IList<Launchpad>> Get(int? limit, int? offset);
    }
}