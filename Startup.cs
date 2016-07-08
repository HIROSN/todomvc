using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using TodoMvc.Models;
using Microsoft.AspNetCore.Mvc;
using TodoMvc.Services;
using TodoMvc.Attributes;
using Microsoft.Extensions.PlatformAbstractions;

namespace TodoMvc
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddMvc(config =>
            {
                if (Configuration["Data:UseSecureConnection"] == "true")
                {
                    config.Filters.Add(new RequireHttpsAttribute());
                }

                config.Filters.Add(new HandleExceptionAttribute());
            })
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddIdentity<TodoUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<TodoContext>();
            services.AddAuthentication();
            services.AddDbContext<TodoContext>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ISignInService, SignInService>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            bool allowInsecureHttp = (Configuration["Data:UseSecureConnection"] != "true");
#if DEBUG
            loggerFactory.AddDebug(LogLevel.Information);
#else
            loggerFactory.AddDebug(env.IsProduction() ? LogLevel.Critical : LogLevel.Warning);
#endif
            Mapper.Initialize(config =>
            {
                config.CreateMap<Todo, TodoModel>().ReverseMap();
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc();
        }
    }
}
