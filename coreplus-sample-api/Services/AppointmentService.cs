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

    public async Task<APIResponseObject<List<MonthlyCostRevenue>>> GetMonthlyCostRevenue(int practitionerId, DateTime startDate, DateTime endDate)
    {
        List<Appointment> _appointments = await GetAppointmentsAsync();
        var monthlyData = new List<MonthlyCostRevenue>();

       


        for (var currentDate = startDate; currentDate <= endDate; currentDate = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1))
        {
            var currentMonth = currentDate.ToString("MMM yyyy", CultureInfo.InvariantCulture);
            var nextMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1);


            var appointmentsInMonth = _appointments.Where(appointment =>
                appointment.practitioner_id == practitionerId &&
                appointment.date >= currentDate && appointment.date < nextMonth);

            int count = appointmentsInMonth.Count();
            var totalCost = appointmentsInMonth.Sum(appointment => appointment.cost);
            var totalRevenue = appointmentsInMonth.Sum(appointment => appointment.revenue);

            monthlyData.Add(new MonthlyCostRevenue
            {
                Month = currentMonth,
                TotalCost = totalCost,
                TotalRevenue = totalRevenue
            });
        }

       

        if (monthlyData == null)
        {
            return new APIResponseObject<List<MonthlyCostRevenue>>()
            {
                message = "No data found!"
            };
        }
        string monthlyJsonData = "";

        return new APIResponseObject<List<MonthlyCostRevenue>>()
        {
            statusCode = 200,
            message = "Success",
            data = monthlyData
        };


    }


    public async Task<APIResponse<Appointment>> GetPractitionerAppointments(int practitionerId, DateTime startDate, DateTime endDate)
    {
        try
        {
            List<Appointment> _appointments = await GetAppointmentsAsync();

            var selectedAppointments = _appointments
                .Where(appointment =>
                    appointment.practitioner_id == practitionerId &&
                    appointment.date >= startDate && appointment.date <= endDate)
                .OrderBy(appointment => appointment.date)
                .ToList();

            if (selectedAppointments.Count() == 0)
            {
                return new APIResponse<Appointment>()
                {
                    statusCode = 200,

                    message = "No appointments found for the selected date range."
                };
            }

            return new APIResponse<Appointment>()
            {
                statusCode = 200,
                message = "Success",
                data = selectedAppointments
            };

        }

        catch (Exception e)
        {
            return new APIResponse<Appointment>()
            {
                statusCode = 200,
                message = "Something Went Wrong"
            };
        }
    }


    public async Task<APIResponseObject<Appointment>> GetAppointmentDetails(int appointmentId)
    {
        try
        {
            List<Appointment> _appointments = await GetAppointmentsAsync();

            var selectedAppointment = _appointments
                .FirstOrDefault(appointment => appointment.Id == appointmentId);

            if (selectedAppointment == null)
            {
                return new APIResponseObject<Appointment>()
                {
                    message = "Appointment not found."
                };
            }

            return new APIResponseObject<Appointment>()
            {
                message = "Success",
                data = selectedAppointment
            };
        }

        catch (Exception e)
        {
            return new APIResponseObject<Appointment>()
            {
                message = "Something Went Wrong"
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

        private static async Task<string> getPractionerNameByID(int practitionerId)
        {
            using var fileStream = File.OpenRead(@"./Data/practitioners.json");
            var data = await System.Text.Json.JsonSerializer.DeserializeAsync<Practitioner[]>(fileStream);
            if (data == null)
            {
                throw new Exception("Data read error");
            }

            List<Practitioner> practionersList = data.ToList<Practitioner>();

            string? practitionerName = practionersList.FirstOrDefault(p => p.id == practitionerId)?.name;

            return practitionerName;

        }
    }
