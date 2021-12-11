using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using palugada_api.Helpers;
using Microsoft.Extensions.Configuration;
using palugada_api;
using Microsoft.AspNetCore.Mvc.Authorization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config =>
{

    // Ini akan mengatur supaya, by default, seluruh request harus di-authenticate terlebih dahulu menggunakan JWT Token ketika mengakses seluruh controller dan action
    // Kecuali yang telah dipasang attribute [AllowAnonymous](dalam hal ini action tersebut akan bypass authentication)
    //AuthorizationPolicy authPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
    //    .RequireAuthenticatedUser()
    //    .Build();
    //config.Filters.Add(new AuthorizeFilter(authPolicy));
});
Assembly.GetExecutingAssembly().GetTypes()
    .Where(type => type.IsClass)
    .Where(type => type.IsAbstract == false)
    .Where(type => type.FullName != null && type.FullName.EndsWith("Service")).ToList().ForEach(type => { builder.Services.AddScoped(type); });

builder.Services.AddDbContext<PalugadaDbContext>(opt =>
{
    string connString = builder.Configuration["PalugadaConnectionString"];
    opt.UseNpgsql(connString);

    opt.UseLoggerFactory(LoggerFactory.Create(cfg => cfg.AddConsole()));
});



const string key = "ini namanya secret key yang akan digunakan untuk generate token JWT hehehehehe";

builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
}).AddJwtBearer(x => {
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = System.TimeSpan.Zero,
    };
});

builder.Services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
