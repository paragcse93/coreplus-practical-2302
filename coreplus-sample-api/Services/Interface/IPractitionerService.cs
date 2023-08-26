using Coreplus.Sample.Api.ResponseModel;
using Coreplus.Sample.Api.Types;

namespace Coreplus.Sample.Api.Services.Interface
{
    public interface IPractitionerService
    {



        Task<APIResponse<List<PractitionerDto>>> GetAllPractitioners();

        Task<APIResponse<List<PractitionerDto>>> GetSupervisorPractitioners();

        Task<APIResponse<List<PractitionerDto>>> GetOtherPractitioners();
    }
}
