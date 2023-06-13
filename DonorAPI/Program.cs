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
using DonorAPI.Services.Security;
using location.Data.Repositories.Cities;
using location.Data.Repositories.Towns;
using location.logic.Logics.Cities;
using location.logic.Logics.Towns;
using LocationAPI.Services.Locations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
//Services dependencies
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ICipherService, CipherService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IJoinTable, JoinTable>();
//User

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserLogic, UserLogic>();

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

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//First Authentication then Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
