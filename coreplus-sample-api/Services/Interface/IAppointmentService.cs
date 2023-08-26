using Coreplus.Sample.Api.ResponseModel;
using Coreplus.Sample.Api.Types;

namespace Coreplus.Sample.Api.Services.Interface
{
    public interface IAppointmentService
    {
        Task<APIResponseObject<List<MonthlyCostRevenue>>> GetMonthlyCostRevenue(int practitionerId, DateTime startDate, DateTime endDate);

        Task<APIResponse<Appointment>> GetPractitionerAppointments(int practitionerId, DateTime startDate, DateTime endDate);

        Task<APIResponseObject<Appointment>> GetAppointmentDetails(int appointmentId);
    }

}
