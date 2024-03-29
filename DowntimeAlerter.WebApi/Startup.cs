using DowntimeAlerter.Core;
using DowntimeAlerter.Core.Services;
using DowntimeAlerter.Core.Utilities;
using DowntimeAlerter.Data;
using DowntimeAlerter.DataAccess;
using DowntimeAlerter.Services;
using DowntimeAlerter.WebApi.ActionFilters;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            services.AddTransient<ISiteService, SiteService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<INotificationLogsService, NotificationLogService>();
            services.AddTransient<IUserService, UserService>();
            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();

            services.AddScoped<LoginFilterAttribute>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHangfire(x =>
                x.UseSqlServerStorage(
                    "Server=(localdb)\\MSSQLLocalDB;Database=DowntimeAlerterForInvicti;Trusted_Connection=True;MultipleActiveResultSets=true"));
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
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
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DowntimeAlerterDbContext>();
                context.Database.Migrate();
                context.Database.EnsureCreated();
                app.UseHangfireDashboard();
                app.UseHangfireServer();
            }
        }
    }
}