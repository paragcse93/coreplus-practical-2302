using Coreplus.Sample.Api.Services;
using Coreplus.Sample.Api.Services.Interface;

namespace Coreplus.Sample.Api.Endpoints.Practitioner;

public static class GetSupervisors
{
    public static RouteGroupBuilder MapGetSupervisorPractitioners(this RouteGroupBuilder group)
    {
        group.MapGet("/supervisorsPractionars", async (IPractitionerService practitionerService) =>
        {
            var practitioners = await practitionerService.GetSupervisorPractitioners();
            return Results.Ok(practitioners);
        });

       

        return group;
    }
}