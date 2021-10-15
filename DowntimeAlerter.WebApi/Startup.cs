using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DowntimeAlerter.Core;
using DowntimeAlerter.Core.Services;
using DowntimeAlerter.Data;
using DowntimeAlerter.DataAccess;
using DowntimeAlerter.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DowntimeAlerter.WebApi
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<DowntimeAlerterDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DevConnection"),
                    x => x.MigrationsAssembly("DowntimeAlerter.DataAccess")));

            services.AddTransient<ISiteEmailService, SiteEmailService>();
            services.AddTransient<ISiteService, SiteService>();            
            services.AddTransient<ILogsService, LogService>();
            services.AddTransient<INotificationLogsService, NotificationLogService>();
            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();

            //services.AddScoped<LoginFilterAttribute>();->todo : onur
            services.AddSingleton<IHttpContextAccessor,
                HttpContextAccessor>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DowntimeAlerterDbContext>();
                context.Database.Migrate();
                context.Database.EnsureCreated();
                //app.UseHangfireDashboard();
                //app.UseHangfireServer();
            }
        }
    }
}
