using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotnetCoreTest.Data;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetCoreTest
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            ConfigureCache(services);
        }

        private void ConfigureCache(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.Configure<MvcOptions>(options =>
            {
                options.CacheProfiles.Add("Cache1Hour", new CacheProfile()
                {
                    Duration = 3600,
                    VaryByHeader = "Accept"
                });
            });


            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = @"Data Source=(localdb)\v11.0;Initial Catalog=DistCache;Integrated Security=True;";
                options.SchemaName = "dbo";
                options.TableName = "TestCache";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                
                //Affiche des erreur si on a un 404 ou un 500
                app.UseStatusCodePages("text/plain", "Response, code: {0}");
                app.UseStatusCodePagesWithRedirects("~/errors/{0}");
                
                /*
                var feature = context.Features.Get<IStatusCodePagesFeature>(); 
                if (feature!= null) 
                { 
                    feature.Enabled = false; 
                }
                */
                
                ConfigureCacheIIS(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// ELMAH (Error Logging Module and Handlers)
        /// /elm : permet d'acceder au logs IIS 
        /// </summary>
        /// <param name="app"></param>
        private void ConfigureCacheIIS(IApplicationBuilder app)
        {
            app.UseElmPage();
            app.UseElmCapture();
        }
    }


    class RouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            throw new NotImplementedException();
        }
    }
}