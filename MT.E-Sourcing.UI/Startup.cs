using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MT.E_Sourcing.UI.Clients;
using MT.E_Sourcing.WebApp.Core.Entities;
using MT.E_Sourcing.WebApp.Core.Repositories.Base;
using MT.E_Sourcing.WebApp.Infrastructure.Data;
using MT.E_Sourcing.WebApp.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.E_Sourcing.UI
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

            services.AddDbContext<WebAppContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddIdentity<AppUser, IdentityRole>(opt=> {
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
            
            
            }).AddDefaultTokenProviders()
                .AddEntityFrameworkStores<WebAppContext>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMvc();
            services.AddRazorPages();

            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            services.ConfigureApplicationCookie(opt=>
            {
                opt.LoginPath = $"/Home/Login";
                opt.LogoutPath =$"/Home/LogOut";
                opt.ExpireTimeSpan = TimeSpan.FromDays(3);
                opt.SlidingExpiration = false;

            });

            services.AddHttpClient();
            services.AddHttpClient<ProductClient>();

            services.AddHttpClient<AuctionClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                
                    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

                    endpoints.MapRazorPages();
                
                
                });
           
        }
    }
}
