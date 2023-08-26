using Coreplus.Sample.Api.Types;
using System.Globalization;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Coreplus.Sample.Api.Services.Interface;
using Coreplus.Sample.Api.Services;
using System.Text.Json;
using Coreplus.Sample.Api.ResponseModel;
using Coreplus.Sample.Api.utils;
using Newtonsoft.Json;

public class AppointmentService : IAppointmentService
{

    public async Task<APIResponse<List<MonthlyCostRevenue>>> GetMonthlyCostRevenue(int practitionerId, DateTime startDate, DateTime endDate)
    {
        try
        {
            // Fetch appointments
            List<Appointment> _appointments = await GetAppointmentsAsync();

            // Aggregate monthly data
            var monthlyData = _appointments
                .Where(appointment => appointment.practitioner_id == practitionerId && appointment.date >= startDate && appointment.date <= endDate)
                .GroupBy(appointment => appointment.date.ToString("MMM yyyy", CultureInfo.InvariantCulture))
                .Select(group => new MonthlyCostRevenue
                {
                    Month = group.Key,
                    TotalCost = group.Sum(appointment => appointment.cost),
                    TotalRevenue = group.Sum(appointment => appointment.revenue)
                })
                .ToList();

            // Fill missing months with zero values
            for (var currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddMonths(1))
            {
                var currentMonth = currentDate.ToString("MMM yyyy", CultureInfo.InvariantCulture);
                if (!monthlyData.Any(data => data.Month == currentMonth))
                {
                    monthlyData.Add(new MonthlyCostRevenue
                    {
                        Month = currentMonth,
                        TotalCost = 0,
                        TotalRevenue = 0
                    });
                }
            }

            // Sort the monthly data
            var sortedMonthlyData = monthlyData.OrderBy(data => DateTime.ParseExact(data.Month, "MMM yyyy", CultureInfo.InvariantCulture)).ToList();

            // If no data found
            if (sortedMonthlyData.Count == 0)
            {
                return new APIResponse<List<MonthlyCostRevenue>>()
                {
                    statusCode = 404,
                    message = "No data found!"
                };
            }

            // Successful response with data
            return new APIResponse<List<MonthlyCostRevenue>>()
            {
                statusCode = 200,
                message = "Success",
                data = sortedMonthlyData
            };
        }
        catch (Exception)
        {
            // Handle any unexpected errors
            return new APIResponse<List<MonthlyCostRevenue>>()
            {
                statusCode = 500,
                message = "Internal server error"
            };
        }
    }



    public async Task<APIResponse<List<Appointment>>> GetPractitionerAppointments(int practitionerId, DateTime startDate, DateTime endDate)
    {
        try
        {
            // Get the list of appointments from an async source (e.g., GetAppointmentsAsync())
            List<Appointment> _appointments = await GetAppointmentsAsync();

            // Filter appointments based on practitioner ID and date range, then order them by date
            var selectedAppointments = _appointments
                .Where(appointment =>
                    appointment.practitioner_id == practitionerId &&
                    appointment.date >= startDate && appointment.date <= endDate)
                .OrderBy(appointment => appointment.date)
                .ToList();

            // If no appointments found for the selected date range
            if (selectedAppointments.Count() == 0)
            {
                return new APIResponse<List<Appointment>>()
                {
                    statusCode = 404,
                    message = "No appointments found for the selected date range."
                };
            }

            // Successful response with data
            return new APIResponse<List<Appointment>>()
            {
                statusCode = 200,
                message = "Success",
                data = selectedAppointments
            };
        }
        catch (FileNotFoundException)
        {
            // File not found error
            return new APIResponse<List<Appointment>>()
            {
                statusCode = 500,
                message = "Data source not found."
            };
        }
        catch (System.Text.Json.JsonException)
        {
            // JSON deserialization error
            return new APIResponse<List<Appointment>>()
            {
                statusCode = 500,
                message = "Error in reading data."
            };
        }
        catch (Exception)
        {
            // Other unexpected errors
            return new APIResponse<List<Appointment>>()
            {
                statusCode = 500,
                message = "Internal server error."
            };
        }
    }


    public async Task<APIResponse<Appointment>> GetAppointmentDetails(int appointmentId)
    {
        try
        {
            // Fetch appointments data
            List<Appointment> _appointments = await GetAppointmentsAsync();

            // Find the appointment with the specified ID
            var selectedAppointment = _appointments.FirstOrDefault(appointment => appointment.Id == appointmentId);

            // If no appointment with the given ID is found
            if (selectedAppointment == null)
            {
                return new APIResponse<Appointment>()
                {
                    statusCode = 404, // Not Found
                    message = "Appointment not found."
                };
            }

            // Successful response with appointment details
            return new APIResponse<Appointment>()
            {
                statusCode = 200, // OK
                message = "Success",
                data = selectedAppointment
            };
        }
        catch (FileNotFoundException)
        {
            // File not found error
            return new APIResponse<Appointment>()
            {
                statusCode = 404, // Not Found
                message = "Appointments data file not found."
            };
        }
        catch (System.Text.Json.JsonException)
        {
            // JSON deserialization error
            return new APIResponse<Appointment>()
            {
                statusCode = 500, // Internal Server Error
                message = "Error while processing appointments data."
            };
        }
        catch (Exception)
        {
            // Other unexpected errors
            return new APIResponse<Appointment>()
            {
                statusCode = 500, // Internal Server Error
                message = "An error occurred while processing the request."
            };
        }
    }


    private async Task<List<Appointment>> GetAppointmentsAsync()
    {

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonDateTimeConverter("M/d/yyyy") } // Use the appropriate date format
        };
        using var fileStream = File.OpenRead(@"./Data/appointments.json");
        var data = await System.Text.Json.JsonSerializer.DeserializeAsync<Appointment[]>(fileStream, options);
        if (data == null)
        {
            throw new Exception("Data read error");
        }

        return data.ToList<Appointment>();

    }

    
}
