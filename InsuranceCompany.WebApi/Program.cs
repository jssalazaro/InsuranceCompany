using FluentAssertions.Common;
using InsuranceCompany.Services.Configuration;
using InsuranceCompany.WebApi.Configuration;
using InsuranceCompnay.Abstractions;
using InsuranceCompnay.Application.Implementation;
using InsuranceCompnay.Application.Interfaces;
using InsuranceCompnay.DataAccess;
using InsuranceCompnay.Repository.Implementation;
using InsuranceCompnay.Repository.Interfaces;
using InsuranceCompnay.Services.JwtConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configurationConnection = builder.Configuration.GetValue<string>("ConnectionStrings:InsuranceCompanyConnection");
var configurationJwtConfig = builder.Configuration.GetSection("JwtConfig");


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injection
builder.Services.AddScoped(typeof(IRepository<>), (typeof(Repository<>)));
builder.Services.AddScoped(typeof(IApplication<>), (typeof(Application<>)));
builder.Services.AddScoped(typeof(IDbContext<>), (typeof(DbContext<>)));
builder.Services.AddScoped(typeof(ITokenHandlerService), (typeof(TokenHandlerService)));
builder.Services.Configure<JwtConfig>(configurationJwtConfig);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt => {
    var key = Encoding.ASCII.GetBytes(configurationJwtConfig.GetValue<string>("Secret"));

    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApiDbContext>();

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlServer(configurationConnection,
            b => b.MigrationsAssembly("InsuranceCompany.WebApi")
        )
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
