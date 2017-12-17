using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ShBazmool.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ShBazmool
{
    public class Startup
    {
        private readonly IConfigurationRoot configuration;

        public Startup(IHostingEnvironment env)
        {
            configuration = new ConfigurationBuilder()
                                .AddEnvironmentVariables()
                                .AddJsonFile(env.ContentRootPath + "/config.json",true)
                                .AddJsonFile(env.ContentRootPath + "/config.development.json")
                                .Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie( options=>
                    {
                        options.AccessDeniedPath = new PathString("/Article/AdminIndex");
                        options.AccessDeniedPath = new PathString("/Article/Edit");
                        options.AccessDeniedPath = new PathString("/Article/Insert");
                        options.AccessDeniedPath = new PathString("/Explanation/AdminIndex");
                        options.AccessDeniedPath = new PathString("/Explanation/Edit");
                        options.AccessDeniedPath = new PathString("/Explanation/Insert");
                        options.AccessDeniedPath = new PathString("/Lecture/AdminIndex");
                        options.AccessDeniedPath = new PathString("/Lecture/Edit");
                        options.AccessDeniedPath = new PathString("/Lecture/Insert");
                        options.AccessDeniedPath = new PathString("/Writing/AdminIndex");
                        options.AccessDeniedPath = new PathString("/Writing/Edit");
                        options.AccessDeniedPath = new PathString("/Writing/Insert");
                        options.AccessDeniedPath = new PathString("/Home/Messages");
                        options.AccessDeniedPath = new PathString("/Home/ReadMessage");
                        options.AccessDeniedPath = new PathString("/AudioClip/AdminIndex");
                        options.AccessDeniedPath = new PathString("/AudioClip/Insert");
                        options.AccessDeniedPath = new PathString("/AudioClip/Edit");
                        options.AccessDeniedPath = new PathString("/Fatawa/AdminIndex");
                        options.AccessDeniedPath = new PathString("/Fatawa/Insert");
                        options.AccessDeniedPath = new PathString("/Fatawa/Edit");
                        options.LoginPath = new PathString("/Account/Login");
                    }
                );
            services.AddMvc();
            services.AddMvcCore();
            services.AddDbContext<ShBazmoolDbContext>(
                options =>
                {
                    var connectionString = configuration.GetConnectionString("ShBazmoolDbContext");
                    options.UseSqlServer(connectionString);
                }
                   
                );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseStaticFiles();


        }
    }
}
