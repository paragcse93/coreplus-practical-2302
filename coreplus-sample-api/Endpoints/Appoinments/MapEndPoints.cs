using Coreplus.Sample.Api.Endpoints.Practitioner;

namespace Coreplus.Sample.Api.Endpoints.Appoinments
{
    public static class MapEndPoints
    {
        public static RouteGroupBuilder MapAppoinmentEndpoints(this RouteGroupBuilder group)
        {
            group.MapGetAllAppoinments();
            group.MapGetMonthluRevCosts();
           
            return group;
        }
    }
}
