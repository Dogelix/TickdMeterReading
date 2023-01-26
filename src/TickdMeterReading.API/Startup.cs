using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TickdMeterReading.API.Extensions.Middleware;
using TickdMeterReading.Application.Handlers;
using TickdMeterReading.Application.Mappers;
using TickdMeterReading.Application.Services;
using TickdMeterReading.Infrastructure.Factories;
using TickdMeterReading.Infrastructure.Repositories;
using FluentMediator;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTracing;
using OpenTracing.Util;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using TickdMeterReading.Domain.Accounts.Commands;
using TickdMeterReading.Domain.Accounts.Events;
using TickdMeterReading.Domain.Accounts;
using TickdMeterReading.Domain.MeterReadings;
using TickdMeterReading.Domain.MeterReadings.Commands;
using MySqlConnector;
using TickdMeterReading.Infrastructure;

namespace TickdMeterReading.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<TickdTechTestDb>(_ => new TickdTechTestDb(Configuration.GetConnectionString("Default")));


            // Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddTransient<IMeterReadingService, MeterReadingService>();

            // Mappers
            services.AddSingleton<AccountViewModelMapper>();
            services.AddSingleton<AccountViewModelMapper>();

            // Factories
            services.AddTransient<IAccountFactory, AccountEntityFactory>();
            services.AddTransient<IMeterReadingFactory, MeterReadingEntityFactory>();

            // Handlers
            services.AddScoped<MeterReadingCommandHandler>();
            services.AddScoped<AccountCommandHandler>();
            services.AddScoped<AccountEventHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddFluentMediator(builder =>
            {
                builder.On<CreateNewAccountCommand>().PipelineAsync().Return<Domain.Accounts.Account, AccountCommandHandler>((handler, request) => handler.HandleNewAccount(request));

                builder.On<AccountCreatedEvent>().PipelineAsync().Call<AccountEventHandler>((handler, request) => handler.HandleTaskCreatedEvent(request));

                builder.On<RemoveAccountCommand>().PipelineAsync().Call<AccountCommandHandler>((handler, request) => handler.HandleDeleteAccount(request));

                builder.On<AccountDeletedEvent>().PipelineAsync().Call<AccountEventHandler>((handler, request) => handler.HandleTaskDeletedEvent(request));

                builder.On<AddMeterReadingCommand>().PipelineAsync().Call<MeterReadingCommandHandler>((handler, request) => handler.HandleNewMeterReading(request));
            });

            services.AddSingleton(serviceProvider =>
            {
                var serviceName = Assembly.GetEntryAssembly().GetName().Name;

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(true);

                ITracer tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            Log.Logger = new LoggerConfiguration().CreateLogger();

            services.AddOpenTracing();

            services.AddOptions();

            services.AddMvc();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tickd Technical Test API V1");
            });
        }
    }
}
