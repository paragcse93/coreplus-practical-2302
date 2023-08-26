using Coreplus.Sample.Api.Endpoints.Appoinments;
using Coreplus.Sample.Api.Endpoints.Practitioner;
using Coreplus.Sample.Api.Services;
using Coreplus.Sample.Api.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IPractitionerService, PractitionerService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();

builder.Services.AddCors();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors(opt =>
{
    opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var practitionerEndpoints = app.MapGroup("/practitioners");
practitionerEndpoints.MapPractitionerEndpoints();

var appoinmentEndPoints = app.MapGroup("/appointments");
appoinmentEndPoints.MapAppoinmentEndpoints();



app.Run();
