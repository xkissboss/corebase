using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using common.Mysql.Utils;
using db.DAL.Impl;
using db.DAL;
using db.BLL;
using db.BLL.Impl;
using NLog.Extensions.Logging;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using common.CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;

namespace corebase
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            this.Env = env;
        }

        public IConfigurationRoot Configuration { get; }

        public IHostingEnvironment Env { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            // 数据库连接注入
            services.AddScoped<IDbConn, MySqlDbConnection>();

            services.AddScoped<IStudentDAL, StudentDAL>();

            services.AddScoped<IStudentBLL, StudentBLL>();

            RedisHelper.InitializeConfiguration(Configuration);

            if (this.Env.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Api", Version = "v1" });
                });
            }


            services.AddSingleton<IDistributedCache>(new RedisCache());

            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromDays(30);
                option.CookieHttpOnly = true;
                option.CookieName = "session";
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.OutputEncoding = Encoding.GetEncoding("GB2312");
            Console.InputEncoding = Encoding.GetEncoding("GB2312");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddNLog().AddDebug();
            env.ConfigureNLog("nlog.config");

            app.UseSession();
            app.UseMvc();

            if (this.Env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api");
                });
            }
        }
    }
}
