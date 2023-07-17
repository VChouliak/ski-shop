using API.Errors;
using API.Extensions;
using API.Helpers;
using API.Middleware;
using Core.Entities.Identity;
using Core.Interfaces.Data.Repository;
using Core.Interfaces.Data.Specification;
using Core.Interfaces.Service.Data;
using Core.Interfaces.Service.Identity;
using Data.Repositories;
using Data.Specifications;
using Infrastructure.Data;
using Infrastructure.Data.SeedData;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Data;
using Service.Identity;
using Settings;
using StackExchange.Redis;
//TODO: refactor
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins(StoreSetting.Instance.CORSHttpOrigin, StoreSetting.Instance.CORSHttpsOrigin);
    });
});

builder.Services.AddDbContext<AppIdentityDbContext, AppIdentityDbContext>();
builder.Services.AddDbContext<DbContext, StoreContext>();
builder.Services.AddScoped(typeof(IBaseAsyncRepository<>), typeof(BaseAsyncRepository<>));
builder.Services.AddTransient(typeof(ISpecification<>), typeof(BaseSpecification<>));
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBaseAsyncDataService, BaseAsyncDataService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddIdentityServices();

builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
{
    var configuration = ConfigurationOptions.Parse(StoreSetting.Instance.RedisConnectionString, true);
   // configuration.AbortOnConnectFail = false;
   // configuration.Ssl = true;
    
    return ConnectionMultiplexer.Connect(configuration);
});


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState.Where(error => error.Value.Errors.Count > 0)
        .SelectMany(x => x.Value.Errors)
        .Select(x => x.ErrorMessage)
        .ToArray();

        var errorResponse = new ApiValidationErrorResponse
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        try
        {
            var context = services.GetRequiredService<StoreContext>();
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context, loggerFactory);

            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var identityContext = services.GetRequiredService<AppIdentityDbContext>();
            await identityContext.Database.MigrateAsync();
            await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occured during migration");
        }
    }
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
