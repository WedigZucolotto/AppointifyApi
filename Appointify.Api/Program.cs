using Appointify.Api.Filters;
using Appointify.Application.Commands.Events.Create;
using Appointify.Application.Queries.Companies;
using Appointify.Domain.Notifications;
using Appointify.Domain.Repositories;
using Appointify.Infrastructure;
using Appointify.Infrastructure.Notifications;
using Appointify.Infrastructure.Repositories;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options => options.Filters.Add<NotificationFilter>())
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(
    cfg => cfg.RegisterServicesFromAssembly(typeof(GetCompanyByIdQueryHandler).Assembly));

builder.Services.AddDbContext<DataContext>();

builder.Services.AddScoped<INotificationContext, NotificationContext>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineValidationBehavior<,>));
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateEventCommandValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
