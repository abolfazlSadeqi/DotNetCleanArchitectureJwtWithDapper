
using Application.Common.Interfaces;
using Infrastructure_Dapper.Persistence;
using Infrastructure_Dapper.Services;
using Infrastructure_Dapper.Services.Custom.CustomerCommand;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure_Dapper;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure_Dapper(this IServiceCollection services, IConfiguration configuration)
    {

        var DefaultConnection = configuration.GetConnectionString("DefaultConnection");
        services.AddSingleton<DapperDBContext>(provider => new DapperDBContext(DefaultConnection));


      //  services.AddSingleton<DapperDBContext>(provider => new DapperDBContext(configuration.GetConnectionString("DefaultConnection")));


     //   services.AddSingleton<DapperDBContext>();

        services.AddScoped<ICustomerService, CustomerService>();

        services.AddScoped<IDomainEventService, DomainEventService>();

        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}