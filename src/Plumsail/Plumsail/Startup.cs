using DataAccess.Sql;
using DataAccess.Sql.DbServices;
using Domain.Entities.Logic;
using Infrastructure;
using Logic.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql.Logging;

namespace Plumsail
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDAL(services);
            ConfigureIoC(services);
            services.AddCors();
            services.AddControllers();
            services.AddMvcCore(options => options.Filters.Add(typeof(GlobalActionFilter))).AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.SetIsOriginAllowed((host) => true).AllowCredentials().AllowAnyHeader().AllowAnyMethod());
            //app.UseHttpsRedirection();
            app.UseRouting();   
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureDAL(IServiceCollection services)
        {
            var sqlConnectionString = Configuration.GetConnectionString("PostgreSql");
            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(sqlConnectionString));

#if DEBUG
            NpgsqlLogManager.Provider = new ConsoleLoggingProvider(NpgsqlLogLevel.Info, true);
            NpgsqlLogManager.IsParameterLoggingEnabled = true;
#endif
        }

        private void ConfigureIoC(IServiceCollection services)
        {            
            services.AddTransient<IDatabaseService, DatabaseService>();

            #region Logic

            services.AddTransient<IFormService, FormService>();

            #endregion

        }

        public class GlobalActionFilter : IActionFilter
        {
            public void OnActionExecuted(ActionExecutedContext context)
            {
                
                if (context.Exception is ActionResultException resultException)
                {
                    context.ExceptionHandled = true;
                    context.Exception = null;
                    context.Result = resultException.Result;
                }
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
            }
        }
    }
}
