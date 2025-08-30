using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmBeachApp.Data;
using SmBeachApp.Extensions;
using SmBeachApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization();

// Adding services to the container
builder.Services.AddServices();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Appsettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Appsettings:Audience"],
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Appsettings:Token"]!)),
        ValidateIssuerSigningKey = true,
    };
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SmBeachDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.EnableTryItOutByDefault();
    });
    
    app.UseDeveloperExceptionPage();
}

app.UseRequestLocalization(opt =>
{
    opt.SetDefaultCulture("en");
    opt.AddSupportedCultures("en", "it", "es");
    opt.AddSupportedUICultures("en", "it", "es");
    opt.RequestCultureProviders = new List<IRequestCultureProvider>()
    {
        new AcceptLanguageHeaderRequestCultureProvider()
    };
});

app.UseGlobalExceptionHandler();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();