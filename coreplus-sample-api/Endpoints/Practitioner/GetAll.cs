using Coreplus.Sample.Api.Services;
using Coreplus.Sample.Api.Services.Interface;

namespace Coreplus.Sample.Api.Endpoints.Practitioner;

public static class GetAll
{
    public static RouteGroupBuilder MapGetAllPractitioners(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IPractitionerService practitionerService) =>
        {
            var practitioners = await practitionerService.GetAllPractitioners();
            return Results.Ok(practitioners);
        });

        return group;
    }
}