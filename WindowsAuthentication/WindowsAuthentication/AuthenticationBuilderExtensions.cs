using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;

namespace WindowsAuthentication
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddDefaultAuthentication(this AuthenticationBuilder builder)
        {
            if (builder is null)
            {
                return null;
            }

            builder.AddCookie(SecurityOptions.AuthenticationSchema, (options) =>
            {
                options.Cookie.SameSite = SameSiteMode.None;
                options.AddDefaultCookieEvents();

            });

            return builder;
        }

        public static void AddDefaultCookieEvents(this CookieAuthenticationOptions options)
        {
            if (options is null)
            {
                return;
            }

            options.Events = new CookieAuthenticationEvents()
            {
                OnRedirectToLogin = (context) =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return Task.CompletedTask;
                }
            };
        }
    }
}
