using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using WindowsAuthentication.Authentication;

namespace WindowsAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WindowsAuthenticationController : ControllerBase
    {
        private readonly WindowsAuthenticationManager _windowsAuthenticationManager;

        public WindowsAuthenticationController(WindowsAuthenticationManager windowsAuthenticationManager)
        {
            this._windowsAuthenticationManager = windowsAuthenticationManager;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var identity = await _windowsAuthenticationManager.GetWindowsIdentity();
            if (!identity.IsValid())
            {
                return Challenge(new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    IsPersistent = false
                },
               IISDefaults.AuthenticationScheme);
            }

            return Ok("Windows authentication succesfull for user " + identity.Name);
        }
       
    }
}
