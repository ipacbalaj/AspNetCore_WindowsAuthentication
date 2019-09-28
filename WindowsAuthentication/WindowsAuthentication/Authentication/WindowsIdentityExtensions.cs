using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WindowsAuthentication.Authentication
{
    public static class WindowsIdentityExtensions
    {
        public static bool IsValid(this WindowsIdentity identity)
        {
            if (identity is null)
            {
                return false;
            }

            if (!identity.IsAuthenticated)
            {
                return false;
            }

            return !(identity.IsAnonymous || identity.IsSystem || identity.IsGuest);
        }
    }
}
