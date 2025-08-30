using SmBeachApp.Services;

namespace SmBeachApp.Extensions;

public static class ServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<AuthService>();
        services.AddScoped<RoleService>();
        services.AddScoped<GroupService>();
    }
}