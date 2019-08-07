using ElementarySchoolProject.Models;
using ElementarySchoolProject.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Unity;

namespace ElementarySchoolProject.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private UnityContainer container;

        public SimpleAuthorizationServerProvider(UnityContainer container)
        {
            this.container = container;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            IdentityUser user = null;
            IEnumerable<string> roles = null;
            IAuthRepository _repo = container.Resolve<IAuthRepository>();

            user = await _repo.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                logger.Warn("Unsuccessfull login attempt with username {0}", context.UserName);
                return;                
            }

            roles = await _repo.FindRoles(user.Id);

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Email, ((ApplicationUser)user)?.Email));
            identity.AddClaim(new Claim("UserName", user.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Name, ((ApplicationUser)user)?.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, ((ApplicationUser)user)?.LastName));
            identity.AddClaim(new Claim(ClaimTypes.Role, string.Join(",", roles)));
            identity.AddClaim(new Claim("UserId", user.Id));
            //TODO 4: ***DONE*** Need to add various claims!

            string loggerMessage = "Grabbed token for roles:";

            foreach (var item in roles)
            {
                if (item == roles.Last())
                {
                    loggerMessage = loggerMessage + " " + item;
                }
                else
                {
                    loggerMessage = loggerMessage + " " + item + ",";
                }
                
            }

            logger.Info(loggerMessage);

            context.Validated(identity);
            _repo.Dispose();
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            Dictionary<string, string> additionalUserInfo = new Dictionary<string, string>();
            foreach (Claim claim in context.Identity.Claims)
            {
                context.AdditionalResponseParameters.Add(claim.Type.Split('/').Last(), claim.Value);
            }

            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }            

            return Task.FromResult<object>(null);
        }
        
    }
}