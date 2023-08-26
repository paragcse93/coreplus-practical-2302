using Coreplus.Sample.Api.Services.Interface;

namespace Coreplus.Sample.Api.Endpoints.Practitioner
{
    public static class GetOtherPractisioner
    {
        public static RouteGroupBuilder MapGetOtherPractitioners(this RouteGroupBuilder group)
        {
           
            group.MapGet("/otherpractionars", async (IPractitionerService practitionerService) =>
            {
                var practitioners = await practitionerService.GetOtherPractitioners();
                return Results.Ok(practitioners);
            });

            return group;
        }
    }
}
