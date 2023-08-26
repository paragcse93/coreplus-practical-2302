using Coreplus.Sample.Api.Services;
using Coreplus.Sample.Api.Services.Interface;
using Coreplus.Sample.Api.Types;

namespace Coreplus.Sample.Api.Endpoints.Appoinments
{
    public static class GetAppoinments
    {
        public static RouteGroupBuilder MapGetAllAppoinments(this RouteGroupBuilder group)
        {
            

            group.MapGet("/practitionerappointmentlist", async (int practitionerId, DateTime startDate, DateTime endDate, IAppointmentService IAppointmentService) =>
            {
                var practitionersAppointments = await IAppointmentService.GetPractitionerAppointments(practitionerId, startDate, endDate);
                return Results.Ok(practitionersAppointments);
            });

            group.MapGet("/appointmentdetails", async(int appointmentId, IAppointmentService IAppointmentService) =>
            {
                var appointmentDetails = await IAppointmentService.GetAppointmentDetails(appointmentId);
                return Results.Ok(appointmentDetails);
            });

            return group;
        }
    }
}
