using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Demo.Autofac;
using Demo.Autofac.Controllers;
using RegistrationExtensions = Autofac.Extras.DynamicProxy.RegistrationExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureServices((context, services) =>
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    })
    .ConfigureContainer<ContainerBuilder>(autofacBuilder =>
    {
        autofacBuilder.RegisterType<MethodExecutionMetricsLoggerAspect>();
        autofacBuilder.RegisterType<WeatherForecastController>()
            .EnableClassInterceptors().InterceptedBy(typeof(MethodExecutionMetricsLoggerAspect));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();