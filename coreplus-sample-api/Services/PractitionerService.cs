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

    public async Task<APIResponse<List<PractitionerDto>>> GetAllPractitioners()
    {
        try
        {
            // Open and read the practitioners.json file
            using var fileStream = File.OpenRead(@"./Data/practitioners.json");
            var data = await JsonSerializer.DeserializeAsync<Practitioner[]>(fileStream);

            // If data read is unsuccessful
            if (data == null)
            {
                return new APIResponse<List<PractitionerDto>>()
                {
                    statusCode = 400,
                    message = "Data read error"
                };
            }

            // Transform data to PractitionerDto
            var results = data.Select(prac => new PractitionerDto(prac.id, prac.name, prac.level)).ToList();

            // If no results found
            if (results.Count() == 0)
            {
                return new APIResponse<List<PractitionerDto>>()
                {
                    statusCode = 404,
                    message = "No Data Found"
                };
            }

            // Successful response with data
            return new APIResponse<List<PractitionerDto>>()
            {
                statusCode = 200,
                message = "Success",
                data = results
            };
        }
        catch (FileNotFoundException)
        {
            // File not found error
            return new APIResponse<List<PractitionerDto>>()
            {
                statusCode = 404,
                message = "File not found"
            };
        }
        catch (JsonException)
        {
            // JSON deserialization error
            return new APIResponse<List<PractitionerDto>>()
            {
                statusCode = 400,
                message = "JSON deserialization error"
            };
        }
        catch (Exception)
        {
            // Other unexpected errors
            return new APIResponse<List<PractitionerDto>>()
            {
                statusCode = 500,
                message = "Internal server error"
            };
        }
    }


    public async Task<APIResponse<List<PractitionerDto>>> GetSupervisorPractitioners()
    {
        try
        {
            // Open and read the practitioners.json file
            using var fileStream = File.OpenRead(@"./Data/practitioners.json");
            var data = await JsonSerializer.DeserializeAsync<Practitioner[]>(fileStream);

            // If data read is unsuccessful
            if (data == null)
            {
                return new APIResponse<List<PractitionerDto>>()
                {
                    statusCode = 400,
                    message = "Data read error"
                };
            }

            // Filter practitioners with level less than 2 (supervisors)
            var results = data
                .Where(practitioner => (int)practitioner.level < 2)
                .Select(prac => new PractitionerDto(prac.id, prac.name, prac.level))
                .ToList();

            // If no results found
            if (results.Count() == 0)
            {
                return new APIResponse<List<PractitionerDto>>()
                {
                    statusCode = 404,
                    message = "No Data Found"
                };
            }

            // Successful response with data
            return new APIResponse<List<PractitionerDto>>()
            {
                statusCode = 200,
                message = "Success",
                data = results
            };
        }
        catch (FileNotFoundException)
        {
            // File not found error
            return new APIResponse<List<PractitionerDto>>()
            {
                statusCode = 404,
                message = "File not found"
            };
        }
        catch (JsonException)
        {
            // JSON deserialization error
            return new APIResponse<List<PractitionerDto>>()
            {
                statusCode = 400,
                message = "JSON deserialization error"
            };
        }
        catch (Exception)
        {
            // Other unexpected errors
            return new APIResponse<List<PractitionerDto>>()
            {
                statusCode = 500,
                message = "Internal server error"
            };
        }
    }


    public async Task<APIResponse<List<PractitionerDto>>> GetOtherPractitioners()
    {
        try
        {
            // Open and read the practitioners.json file
            using var fileStream = File.OpenRead(@"./Data/practitioners.json");
            var data = await JsonSerializer.DeserializeAsync<Practitioner[]>(fileStream);

            // If data read is unsuccessful
            if (data == null)
            {
                return new APIResponse<List<PractitionerDto>>()
                {
                    message = "Data read error"
                };
            }

            // Filter and transform data to PractitionerDto for practitioners with level >= 2
            var results = data.Where(practitioner => (int)practitioner.level >= 2)
                              .Select(prac => new PractitionerDto(prac.id, prac.name, prac.level))
                              .ToList();

            // If no results found
            if (results.Count() == 0)
            {
                return new APIResponse<List<PractitionerDto>>()
                {
                    statusCode = 400,
                    message = "No Data Found"
                };
            }

            // Successful response with data
            return new APIResponse<List<PractitionerDto>>()
            {
                statusCode = 200,
                message = "Success",
                data = results
            };
        }
        catch (FileNotFoundException)
        {
            // File not found error
            return new APIResponse<List<PractitionerDto>>()
            {
                statusCode = 404,
                message = "File not found"
            };
        }
        catch (JsonException)
        {
            // JSON deserialization error
            return new APIResponse<List<PractitionerDto>>()
            {
                statusCode = 400, // Bad Request
                message = "JSON deserialization error"
            };
        }
        catch (Exception)
        {
            // Other unexpected errors
            return new APIResponse<List<PractitionerDto>>()
            {
                statusCode = 500,
                message = "Internal server error"
            };
        }
    }


}