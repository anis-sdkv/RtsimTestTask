using Microsoft.AspNetCore.Authentication.Cookies;
using RtsimTestTask.Api.Extensions;
using RtsimTestTask.Infrastructure.Persistence.DbContext;
using RtsimTestTask.Infrastructure.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApi(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(14);
        options.SlidingExpiration = true;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();