using Mch.MainManagerSrv.Extensions;
using Microsoft.AspNetCore.Localization;
using SmBeachApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization();
builder.Services.AddScoped<AuthService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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