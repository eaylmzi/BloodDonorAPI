using bloodbank.Data.Repositories.Hospitals;
using bloodbank.Logic.Logics.Hospitals;
using donor.Data.Repositories.Branches;
using donor.Data.Repositories.DonationHistories;
using donor.Data.Repositories.Donors;
using donor.Logic.Logics.Brances;
using donor.Logic.Logics.DonationHistories;
using donor.Logic.Logics.Donors;
using donor.Logic.Logics.JoinTable;
using DonorAPI.Services.Cipher;
using DonorAPI.Services.Donors;
using DonorAPI.Services.Jwt;
using DonorAPI.Services.Location;
using DonorAPI.Services.Security;
using location.Data.Repositories.Cities;
using location.Data.Repositories.Towns;
using location.logic.Logics.Cities;
using location.logic.Logics.Towns;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

/*
string authenticationProviderKey = "MyJwtProvider";
SymmetricSecurityKey signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AppSettings:Token"));
builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = authenticationProviderKey)
    .AddJwtBearer(authenticationProviderKey, options =>
    {
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signInKey,
            ValidateIssuer = true,
            ValidIssuer = "donorIssuer",
            ValidateAudience = true,
            ValidAudience = "donorAudience",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RequireExpirationTime = true
        };
    });

*/

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
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ICipherService, CipherService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IJoinTable, JoinTable>();
//User

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserLogic, UserLogic>();

builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<IHospitalLogic, HospitalLogic>();
builder.Services.AddScoped<ITownRepository, TownRepository>();
builder.Services.AddScoped<ITownLogic, TownLogic>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityLogic, CityLogic>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IBranchLogic, BranchLogic>();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<IDonorLogic, DonorLogic>();
builder.Services.AddScoped<IDonationHistoryRepository, DonationHistoryRepository>();
builder.Services.AddScoped<IDonationHistoryLogic, DonationHistoryLogic>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseApiVersioning();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//First Authentication then Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors();

app.Run();
