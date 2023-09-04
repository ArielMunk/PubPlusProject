using BL;
using BL.Models;
using DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Services.Auth;

namespace Services
{
    public static class Auth
    {
        public static bool IsAuthenticated(AuthorizationHandlerContext context, out UserModel user)
        {
            return IsAuthenticated(context.User, out user);
        }

        private static bool IsAuthenticated(ClaimsPrincipal contextClaimsPrincipal, out UserModel user)
        {
            user = null;
            if (contextClaimsPrincipal == null || !contextClaimsPrincipal.Identity.IsAuthenticated)
                return false;

            Claim claim = contextClaimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault();
            if (claim != null && !string.IsNullOrWhiteSpace(claim.Value))
            {
                UserEntity userEntity = BL.UserService.SelectUser(null, Guid.Parse(claim.Value));
                if (userEntity != null)
                    user = UserService.EntityToModel(userEntity);
                
                return user != null;
            }
            return false;
        }

        public static void SignIn(HttpContext httpContext, string userToken)
        {
            var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, userToken)
                };
            var Identity = new ClaimsIdentity(userClaims, "User Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { Identity });
            AuthenticationHttpContextExtensions.SignInAsync(httpContext, userPrincipal);
        }
        public static void SignOut(HttpContext httpContext)
        {
            AuthenticationHttpContextExtensions.SignOutAsync(httpContext);
        }
    }
}
