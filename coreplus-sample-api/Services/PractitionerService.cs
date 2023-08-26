using System.IO;
using System.Text.Json;
using Coreplus.Sample.Api.ResponseModel;
using Coreplus.Sample.Api.Services;
using Coreplus.Sample.Api.Services.Interface;
using Coreplus.Sample.Api.Types;

namespace Coreplus.Sample.Api.Services;

public record PractitionerDto(long id, string name, PractitionerLevel level);

public class PractitionerService : IPractitionerService
{
    
      public async Task<APIResponse<PractitionerDto>> GetAllPractitioners()
    {
        try
        {
           using var fileStream = File.OpenRead(@"./Data/practitioners.json");
        var data = await JsonSerializer.DeserializeAsync<Practitioner[]>(fileStream);
        if (data == null)
        {
            throw new Exception("Data read error");
        }

        var rsults = data.Select(prac => new PractitionerDto(prac.id, prac.name, prac.level)).ToList();

            if (rsults.Count() == 0)
            {
                return new APIResponse<PractitionerDto>()
                {
                    message = "No Data Found"
                };
            }

            return new APIResponse<PractitionerDto>()
            {
                statusCode = 200,
                message = "Success",
                data = rsults
            };

        }

        catch (Exception e)
        {
            return new APIResponse<PractitionerDto>()
            {
                statusCode = 400,
                message = "Something Went Wrong"
            };
        }
    }

    public async Task<APIResponse<PractitionerDto>> GetSupervisorPractitioners()
    {
        try
        {
            using var fileStream = File.OpenRead(@"./Data/practitioners.json");
            var data = await JsonSerializer.DeserializeAsync<Practitioner[]>(fileStream);
            if (data == null)
            {
                throw new Exception("Data read error");
            }

            var rsults = data.Where(practitioner => (int)practitioner.level < 2).Select(prac => new PractitionerDto(prac.id, prac.name, prac.level)).ToList();

            if (rsults.Count() == 0)
            {
                return new APIResponse<PractitionerDto>()
                {
                    message = "No Data Found"
                };
            }

            return new APIResponse<PractitionerDto>()
            {
                statusCode = 200,
                message = "Success",
                data = rsults
            };

        }

        catch (Exception e)
        {
            return new APIResponse<PractitionerDto>()
            {
                statusCode = 400,
                message = "Something Went Wrong"
            };
        }

    
    }

    public async Task<APIResponse<PractitionerDto>> GetOtherPractitioners()
    {
        try
        {
            using var fileStream = File.OpenRead(@"./Data/practitioners.json");
            var data = await JsonSerializer.DeserializeAsync<Practitioner[]>(fileStream);
            if (data == null)
            {
                throw new Exception("Data read error");
            }

            var rsults = data.Where(practitioner => (int)practitioner.level >= 2).Select(prac => new PractitionerDto(prac.id, prac.name, prac.level)).ToList();

            if (rsults.Count() == 0)
            {
                return new APIResponse<PractitionerDto>()
                {
                    message = "No Data Found"
                };
            }

            return new APIResponse<PractitionerDto>()
            {
                statusCode = 200,
                message = "Success",
                data = rsults
            };

        }

        catch (Exception e)
        {
            return new APIResponse<PractitionerDto>()
            {
                statusCode = 400,
                message = "Something Went Wrong"
            };
        }

    }

}