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

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
