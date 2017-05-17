using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Security
{
    class ClaimsTransformation
    {
        static void Main(string[] args)
        {
            SetupPrincipal();
            UsePrincipal();
        }

        private static void UsePrincipal()
        {
            ShowCastle();
            //imperative check
            ClaimsPrincipalPermission.CheckAccess("Castle", "Show");
            var authz = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthorizationManager;
            AuthorizationContext context = new AuthorizationContext(ClaimsPrincipal.Current, "Castle", "Show");
            bool isAuthz = authz.CheckAccess(context);
        }

        private static void SetupPrincipal()
        {
            //Thread.CurrentPrincipal =
            //    new WindowsPrincipal(WindowsIdentity.GetCurrent());

            var p = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            Thread.CurrentPrincipal = FederatedAuthentication
                                        .FederationConfiguration
                                        .IdentityConfiguration
                                        .ClaimsAuthenticationManager.Authenticate("none", p) as IPrincipal;
            //Valid for ASP.net HTTP Context
            /*
            var sessionToken = new SessionSecurityToken(
                p, TimeSpan.FromHours(8));

            FederatedAuthentication.SessionAuthenticationModule
                                    .WriteSessionTokenToCookie(sessionToken);
              */

        }
        [ClaimsPrincipalPermission(SecurityAction.Demand,
            Operation = "Show",
            Resource = "Castle")]
        static void ShowCastle() {
            //if the user is allowed to show castle
            Console.WriteLine("Aaah Wonderful.......");


        }
    }

    class ClaimTransformer : ClaimsAuthenticationManager
    {

        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            //validate incomming claims
            var name = incomingPrincipal.Identity.Name;
            if (string.IsNullOrWhiteSpace(name)) {

                //somethings is wrong
                throw new SecurityException("Name Claim missing");
            }

            return CreatePrincipal(name);
        }

        private ClaimsPrincipal CreatePrincipal(string name)
        {
            var hasCastle = false;
            if (name.ToUpper() == "ACSIND\\30197821")
            {
                hasCastle = true;

            }
            var claims = new List<Claim> {

                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, "saurabh.jain2@conduent.com"),
                new Claim(@"http:\\myClaims\hascastle", hasCastle.ToString())
            };
            return new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));

        }
    }

    class ClaimAuthManager : ClaimsAuthorizationManager {

        public override bool CheckAccess(AuthorizationContext context)
        {
            //return base.CheckAccess(context);
            var resourse = context.Resource.First().Value;
            var action = context.Action.First().Value;

            if (action == "Show" && resourse == "Castle") {

                var hasCastle = context.Principal.HasClaim(@"http:\\myClaims\hascastle", "true");
                return true;

            }
            return false;
        }
    }
}
