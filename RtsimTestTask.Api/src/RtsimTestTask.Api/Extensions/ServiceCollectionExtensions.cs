using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RtsimTestTask.Api.DataMappers;

namespace RtsimTestTask.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfigurationRoot config)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        
        return services;
    }
}