using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Security
{
    class ClaimDemo
    {

        static void Main(string[] args)
        {
            SetUpPrincipal();
            UsePrincipalLegacy();
            UsePrincipalNew();

            Console.ReadLine();


        }

        private static void UsePrincipalNew()
        {
            //var p = Thread.CurrentPrincipal;
            //var cp = p as ClaimsPrincipal;
            //The above 2 lines of code has been encapsulated by the below ClaimsPrincipal
            var cp = ClaimsPrincipal.Current;
            var email = cp.FindFirst(ClaimTypes.Email);

            Console.WriteLine($"Claim Issuer:{email.Issuer}; Claim: {email.Value}; {email.OriginalIssuer}");

            //quering specific identity a ClaimPrincipla can have multiple identities.
            Console.WriteLine(cp.Identities.First().IsAuthenticated);

        }

        private static void UsePrincipalLegacy()
        {
            var p = Thread.CurrentPrincipal;
            Console.WriteLine(p.Identity.Name);
            Console.WriteLine(p.IsInRole("geek"));
        }

        private static void SetUpPrincipal()
        {
            //var claim = new Claim("name", "saurabh");
            //var claim = new Claim(ClaimTypes.Name, "30197821");

            var claimList = new List<Claim>{
                new Claim(ClaimTypes.Name, "30197821"),
                new Claim(ClaimTypes.Email, "saurabh.jain2@condunet.com"),
                new Claim(ClaimTypes.Role, "geek"),
                new Claim("http://myClaims/location", "Philadelphia")
            };
            var id = new ClaimsIdentity(claimList);
            Console.WriteLine($"Authenticated User: {id.IsAuthenticated}");


            //var id2 = new ClaimsIdentity(claimList, "Console App");
            //Remapping name claim to email
            //var id2 = new ClaimsIdentity(claimList, "Console App", ClaimTypes.Email, ClaimTypes.Role);
            var id2 = new ClaimsIdentity(claimList, "Console App", ClaimTypes.Name, ClaimTypes.Role);
            Console.WriteLine($"Authenticated User: {id2.IsAuthenticated}");

            var p = new ClaimsPrincipal(id);
            p.AddIdentity(id2);

            Thread.CurrentPrincipal = p;
        }


    }
    
}
