using Coreplus.Sample.Api.ResponseModel;
using Coreplus.Sample.Api.Types;

namespace Coreplus.Sample.Api.Services.Interface
{
    public interface IAppointmentService
    {
        Task<APIResponse<List<MonthlyCostRevenue>>> GetMonthlyCostRevenue(int practitionerId, DateTime startDate, DateTime endDate);

        Task<APIResponse<List<Appointment>>> GetPractitionerAppointments(int practitionerId, DateTime startDate, DateTime endDate);

        Task<APIResponse<Appointment>> GetAppointmentDetails(int appointmentId);
    }

}
