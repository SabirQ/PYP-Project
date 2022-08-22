using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PYP_Project_API.Application.Interfaces.Repositories;
using PYP_Project_API.Persistance.Context;
using PYP_Project_API.Persistance.Repositories;
using PYP_Project_API.Persistance.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//);
builder.Services.AddControllers().AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; options.SerializerSettings.Converters.Add(new StringEnumConverter()); options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc; });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddPersistenceRegistration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
