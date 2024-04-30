using CollegeApp.AutoMapperConfig;
using CollegeApp.Data;
using CollegeApp.MyLogging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using CollegeApp.Repository;


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();
builder.Logging.AddLog4Net();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddDbContext<CollegeDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDbConnection")));

#region Serilog Configuration
//Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
//    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute)
//    .CreateLogger();

////builder.Host.UseSerilog(); // For serilog only and over ride in built loging
//builder.Logging.AddSerilog(); // for both builten and Serilog
#endregion




// Add services to the container.

builder.Services.AddControllers(options =>options.ReturnHttpNotAcceptable = true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMyLogger, LogToDB>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddScoped (typeof(ICollegeRepository<>)  , typeof(CollegeRepository<>));

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
