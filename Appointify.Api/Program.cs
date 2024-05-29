using Appointify.Api.Filters;
using Appointify.Application.Commands.Events.Create;
using Appointify.Application.Commands.Services.Create;
using Appointify.Application.Queries.Companies.ToSchedule;
using Appointify.Domain.Authentication;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using Appointify.Infrastructure;
using Appointify.Infrastructure.Notifications;
using Appointify.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Appointify.Api.Extensions;
using Appointify.Infrastructure.Authentication;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { cultureInfo };
    options.DefaultRequestCulture = new RequestCulture(cultureInfo);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services
    .AddControllers(options => options.Filters.Add<NotificationFilter>())
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddCorsExtension();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

//builder.Services.AddMediatR(
//    cfg => cfg.RegisterServicesFromAssembly(typeof(GetCompanyByIdQueryHandler).Assembly));

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(new[] {
    typeof(GetCompanyToScheduleQueryHandler).Assembly,
    typeof(CreateServiceCommandHandler).Assembly,
}));

builder.Services.AddDbContext<DataContext>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

builder.Services.AddScoped<IHttpContext, Appointify.Infrastructure.Authentication.HttpContext>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<INotificationContext, NotificationContext>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineValidationBehavior<,>));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IServiceRepository, ServiceRepository>();
builder.Services.AddTransient<IPlanRepository, PlanRepository>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateEventCommandValidator>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddAuthenticationExtension(builder.Configuration);
builder.Services.AddSwaggerAuthentication();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("companies:getById", policy => policy.RequireClaim("permissions", "companies:getById"));
    options.AddPolicy("companies:update", policy => policy.RequireClaim("permissions", "companies:update"));
    options.AddPolicy("companies:services", policy => policy.RequireClaim("permissions", "companies:services"));
    options.AddPolicy("services:getById", policy => policy.RequireClaim("permissions", "services:getById"));
    options.AddPolicy("services:create", policy => policy.RequireClaim("permissions", "services:create"));
    options.AddPolicy("services:update", policy => policy.RequireClaim("permissions", "services:update"));
    options.AddPolicy("services:delete", policy => policy.RequireClaim("permissions", "services:delete"));
    options.AddPolicy("users:getById", policy => policy.RequireClaim("permissions", "users:getById"));
    options.AddPolicy("users:update", policy => policy.RequireClaim("permissions", "users:update"));
    options.AddPolicy("users:getDay", policy => policy.RequireClaim("permissions", "users:getDay"));
    options.AddPolicy("users:getWeek", policy => policy.RequireClaim("permissions", "users:getWeek"));
    options.AddPolicy("users:getMonth", policy => policy.RequireClaim("permissions", "users:getMonth"));
    options.AddPolicy("events:getById", policy => policy.RequireClaim("permissions", "events:getById"));
    options.AddPolicy("events:delete", policy => policy.RequireClaim("permissions", "events:delete"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

var supportedCultures = new[] { cultureInfo };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultureInfo),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
app.UseRequestLocalization(localizationOptions);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
