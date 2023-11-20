using TruckRouteAPI.Models;
using Route = TruckRouteAPI.Models.Route;

namespace TruckRouteAPI.Services.Interface
{
    public interface ITruckRouteService
    {
        public void CreateUser(User user);

        public User GetUserById(long id);

        public void AddRoute(Route route);

        public void SaveRoutetoFile(string routeName, string password);

    }
}
