using Application;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security;
using Core.Security.DependencyResolvers;
using Core.Security.Encryption;
using Core.Security.IoC;
using Core.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Persistence;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSecurityServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer= tokenOptions.Issuer,
            ValidAudience= tokenOptions.Audience,
            ValidateIssuerSigningKey= true,
            IssuerSigningKey= SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });
builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
    app.ConfigureCustomExceptionMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
