using Coreplus.Sample.Api.Services.Interface;

namespace Coreplus.Sample.Api.Endpoints.Appoinments
{
    public static class GetMonthluRevCost
    {
        public static RouteGroupBuilder MapGetMonthluRevCosts(this RouteGroupBuilder group)
        {
            group.MapGet("/summarybydate", async (int practitionerId, DateTime startDate, DateTime endDate, IAppointmentService IAppointmentService) =>
            {
                var costRevReport = await IAppointmentService.GetMonthlyCostRevenue(practitionerId, startDate, endDate);
                return Results.Ok(costRevReport);
            });
            return group;
        }
    }
}
