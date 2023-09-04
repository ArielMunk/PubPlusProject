using BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PoliciesAuthorizationHandler : AuthorizationHandler<AuthorizeRequireSection>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeRequireSection requirement)
        {
            UserModel user;
            if (Services.Auth.IsAuthenticated(context, out user))
            {
                new HttpContextAccessor().HttpContext.Items.Add("ConnectedUser", user);
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }

    public class AuthorizeRequireSection : IAuthorizationRequirement
    {
        public string SectionName { get; }
        public AuthorizeRequireSection(string sectionName)
        {
            this.SectionName = sectionName;
        }
    }

    public static class AuthorizationPolicyBuilderExtension
    {
        public static AuthorizationPolicyBuilder AuthorizeRequireSection(this AuthorizationPolicyBuilder builder, string sectionName)
        {
            builder.AddRequirements(new AuthorizeRequireSection(sectionName));
            return builder;
        }
    }
}
