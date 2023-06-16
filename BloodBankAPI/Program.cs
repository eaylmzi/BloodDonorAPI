using bloodbank.Data;
using bloodbank.Data.Repositories.BloodRequests;
using bloodbank.Data.Repositories.Hospitals;
using bloodbank.Logic.Logics.BloodRequests;
using bloodbank.Logic.Logics.Hospitals;
using bloodbank.Logic.Logics.JoinTable;
using BloodBankAPI.Services.Cipher;
using BloodBankAPI.Services.Donors;
using BloodBankAPI.Services.Jwt;
using BloodBankAPI.Services.Location;
using BloodBankAPI.Services.Mail;
using BloodBankAPI.Services.Security;
using donor.Data.Repositories.Branches;
using donor.Data.Repositories.DonationHistories;
using donor.Data.Repositories.Donors;
using donor.Logic.Logics.Brances;
using donor.Logic.Logics.DonationHistories;
using donor.Logic.Logics.Donors;
using DonorAPI.Services.Donors;
using DonorAPI.Services.Location;
using location.Data.Repositories.Cities;
using location.Data.Repositories.Geopoints;
using location.Data.Repositories.Towns;
using location.logic.Logics.Cities;
using location.logic.Logics.Geopoints;
using location.logic.Logics.Towns;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using user.Data.Repositories.Users;
using user.Logic.Logics.Users;

var builder = WebApplication.CreateBuilder(args);
//Mapper Service
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//JWT
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = "MyJwtProvider")
          .AddJwtBearer("MyJwtProvider", options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value ?? throw new ArgumentNullException())),
                  ValidateIssuer = false,
                  ValidateAudience = false
              };
          });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value ?? throw new ArgumentNullException())),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme(\"bearer{token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
//Versioning
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});
//Services dependencies

builder.Services.AddScoped<IJoinTable, JoinTable>();

builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();

builder.Services.AddScoped<IGeopointRepository, GeopointRepository>();
builder.Services.AddScoped<IGeopointLogic, GeopointLogic>();

builder.Services.AddScoped<ICipherService, CipherService>();
builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<IHospitalLogic, HospitalLogic>();

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityLogic, CityLogic>();
builder.Services.AddScoped<ITownRepository, TownRepository>();
builder.Services.AddScoped<ITownLogic, TownLogic>();

builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IBranchLogic, BranchLogic>();

builder.Services.AddScoped<IBloodRequestRepository, BloodRequestRepository>();
builder.Services.AddScoped<IBloodRequestLogic, BloodRequestLogic>();

builder.Services.AddScoped<IDonationHistoryLogic, DonationHistoryLogic>();
builder.Services.AddScoped<IDonationHistoryRepository, DonationHistoryRepository>();
// Add services to the container.

builder.Services.AddScoped<ILocationService, LocationService>();

builder.Services.AddScoped<IDonorLogic, DonorLogic>();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseApiVersioning();

// Configure the HTTP request pipeline.

app.UseSwagger();
    app.UseSwaggerUI();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
