using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PhoneBook.Core.MqServices.RabbitMq;
using PhoneBook.Service.Abstract;
using PhoneBook.Service.Concrete;
using PhoneBookApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookApp
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
            services.AddDependencyToService();

            services.Configure<RabbitMqSettings>(Configuration.GetSection("RabbitMqSettings"));

            services.AddCap(x =>
            {
                //x.FailedRetryCount = 3;
                //x.FailedRetryInterval = 600;
                x.UseInMemoryStorage();
                var rabbitMqSettings = services.BuildServiceProvider().GetService<IOptions<RabbitMqSettings>>().Value;
                x.UseRabbitMQ(settings =>
                {
                    settings.HostName = rabbitMqSettings.Hostname;
                    settings.Port = rabbitMqSettings.Port;
                    settings.UserName = rabbitMqSettings.Username;
                    settings.Password = rabbitMqSettings.Password;
                });
            });

            services.AddSwaggerGen(c =>
            {
                // c.EnableAnnotations();
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "PhoneBook",
                    Description = "Api",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact() { Name = "Sezgin �olak", Email = "sezgincolak90@gmail.com", Url = new Uri("https://github.com/sezginclk") }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
