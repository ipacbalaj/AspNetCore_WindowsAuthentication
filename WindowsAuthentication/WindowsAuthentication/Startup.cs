using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WindowsAuthentication.Authentication;

namespace WindowsAuthentication
{
    public class Startup
    {
        private string CUSTOM_SCHEME = "MY_SCHEME";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAuthenticationServices(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<WindowsAuthenticationManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureAuthenticationServices(IServiceCollection services)
        {
            services.AddAuthentication((options) =>
            {
                options.DefaultScheme = IISDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = SecurityOptions.AuthenticationSchema;
                options.DefaultChallengeScheme = SecurityOptions.AuthenticationSchema;
                options.DefaultSignInScheme = SecurityOptions.AuthenticationSchema;
                options.DefaultSignOutScheme = SecurityOptions.AuthenticationSchema;
                options.DefaultForbidScheme = SecurityOptions.AuthenticationSchema;
            }).AddDefaultAuthentication();

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
                options.AutomaticAuthentication = false;
            });
        }
    }
}
