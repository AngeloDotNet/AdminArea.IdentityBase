using AdminArea_IdentityBase.Customizations.Identity;
using AdminArea_IdentityBase.Models.Entities;
using AdminArea_IdentityBase.Models.Enums;
using AdminArea_IdentityBase.Models.Options;
using AdminArea_IdentityBase.Models.Services.Application;
using AdminArea_IdentityBase.Models.Services.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AdminArea_IdentityBase
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
            services.AddMvc();

            services.AddRazorPages(options =>
            {
                options.Conventions.AllowAnonymousToPage("/Privacy");
            });

            var identityBuilder = services.AddDefaultIdentity<ApplicationUser>(options =>
            {

                // Criteri di validazione della password
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 4;

                // Conferma dell'account
                options.SignIn.RequireConfirmedAccount = true;

                // Blocco dell'account
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>()
            .AddPasswordValidator<CommonPasswordValidator<ApplicationUser>>();

            var persistence = Persistence.EfCore;
            switch (persistence)
            {
                case Persistence.EfCore:
                    identityBuilder.AddEntityFrameworkStores<AdminAreaDbContext>();

                    services.AddTransient<IAdminService, EfCoreAdminService>();

                    services.AddDbContextPool<AdminAreaDbContext>(optionsBuilder =>
                    {
                        string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                        optionsBuilder.UseSqlite(connectionString);
                    });
                    break;
            }

            services.AddSingleton<IEmailSender, MailKitEmailSender>();
            services.AddSingleton<IEmailClient, MailKitEmailSender>();

            // Options
            services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));
            services.Configure<UsersOptions>(Configuration.GetSection("Users"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandlingPath = "/Error",
                    AllowStatusCode404Response = true
                });
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCaching();
            app.UseEndpoints(routeBuilder =>
            {
                routeBuilder.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();
                routeBuilder.MapRazorPages().RequireAuthorization();
            });
        }
    }
}