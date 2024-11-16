using AutoMapper;
using RtsimTestTask.Api.DataMappers;
using RtsimTestTask.Api.Extensions;
using RtsimTestTask.Api.Middleware;
using RtsimTestTask.Application;
using RtsimTestTask.Infrastructure.Persistence.DataMappers;
using RtsimTestTask.Infrastructure.Persistence.InfrastructureConfigureExtensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDomain(builder.Configuration);
builder.Services.AddApi(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

Action<IMapperConfigurationExpression> action = config =>
{
    config.AddProfile<ApiMappingProfile>();
    config.AddProfile<InfrastructureMappingProfile>();
};

var config = new MapperConfiguration(action);
config.AssertConfigurationIsValid();
builder.Services.AddAutoMapper(action);

var app = builder.Build();
await app.CreateAdminAccountAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<DefaultExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();