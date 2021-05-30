using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace CarSystem.Controllers.Helpers
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User  = "User";
    }

    public struct CustomClaim
    {
        public const string IsOrganizer = "IsOrganizer";
    }

    public class AppUserState
    {
        private readonly IHttpContextAccessor _context;

        public AppUserState(IHttpContextAccessor context)
        {
            _context = context;
        }

        public bool IsAuthenticated => _context.HttpContext.User.Identity.IsAuthenticated;

        public Guid? UserId
        {
            get
            {
                if (Guid.TryParse(Get(ClaimTypes.NameIdentifier), out var id))
                    return id;
                return null;
            }
        }

        public string UserName => Get(ClaimTypes.Name);

        public string UserEmail => Get(ClaimTypes.Email);

        public bool IsOrganizer => Get(CustomClaim.IsOrganizer) == "true";

        public bool IsInRole(params string[] roles)
        {
            return roles.Any(role => _context.HttpContext.User.IsInRole(role));
        }

        private string Get(string key)
        {
            var user = _context.HttpContext.User;

            if (user == null)
                return string.Empty;

            var claims = user.Claims.ToList();

            var claim = claims.FirstOrDefault(c => c.Type == key);

            if (claim != null)
            {
                return claim.Value;
            }

            return string.Empty;
        }

        //public async Task Login(BaseUser user)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //        new Claim(ClaimTypes.Name, user.FirstName),
        //        new Claim(ClaimTypes.Email, user.Email),
        //        new Claim(ClaimTypes.Role, user is AdminUser ? Roles.Admin : Roles.User),
        //    };

        //    if (user is User u)
        //    {
        //        if (u.Organizers.Any())
        //            claims.Add(new Claim(CustomClaim.IsOrganizer, "true"));
        //    }

        //    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        //    identity.AddClaims(claims);

        //    await _context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity),
        //        new AuthenticationProperties
        //        {
        //            ExpiresUtc   = DateTime.UtcNow.AddHours(4),
        //            IsPersistent = false,
        //            AllowRefresh = false,
        //        });
        //}

        public async Task Logout()
        {
            await _context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }

}
