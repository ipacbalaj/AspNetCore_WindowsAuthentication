using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IISIntegration;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace WindowsAuthentication.Authentication
{
    public class WindowsAuthenticationManager
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public WindowsAuthenticationManager(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        public async Task<WindowsIdentity> GetWindowsIdentity()
        {
            var windows = await _contextAccessor.HttpContext.AuthenticateAsync(IISDefaults.AuthenticationScheme);

            return windows?.Principal?.Identity as WindowsIdentity;
        }
    }
}
