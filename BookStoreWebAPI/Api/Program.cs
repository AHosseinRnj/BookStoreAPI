using Infrastructure.Extensions;
using Application.Extensions;
using log4net.Config;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();
XmlConfigurator.Configure(new FileInfo("log4net.config"));

// Add services to the container.
builder.Services.ConfigureInfrastructureService(builder.Configuration);
builder.Services.ConfigureApplicationService();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(option => option.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(option =>
{
    option.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var ex = context.Features.Get<IExceptionHandlerFeature>();

        if (ex != null)
        {
            await context.Response.WriteAsync(ex.Error.Message);

        }
    });
});

app.UseAuthorization();

app.MapControllers();

app.Run();