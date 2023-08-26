using Coreplus.Sample.Api.ResponseModel;
using Coreplus.Sample.Api.Types;

namespace Coreplus.Sample.Api.Services.Interface
{
    public interface IPractitionerService
    {



        Task<APIResponse<PractitionerDto>> GetAllPractitioners();

        Task<APIResponse<PractitionerDto>> GetSupervisorPractitioners();

        Task<APIResponse<PractitionerDto>> GetOtherPractitioners();
    }
}
