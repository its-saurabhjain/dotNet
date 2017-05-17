using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Security
{
    class Program
    {
        static void Main(string[] args){
            var id = WindowsIdentity.GetCurrent();
            Console.WriteLine(id.Name);
            Console.WriteLine($"Authentication Type {id.AuthenticationType}");

            var account = new NTAccount(id.Name);
            var sid = account.Translate(typeof(SecurityIdentifier));
            Console.WriteLine(sid.Value);
            //S-1-5-21 prefix
            //next are identifier of domain
            //last one relative identifier or the user

            foreach (var grp in id.Groups) {

                Console.WriteLine(grp.Value);   ///windows identity is wrapping the windows native token, and windows token are stored as ids and not as group names
                //Console.WriteLine(grp.Translate(typeof(NTAccount)));
            }

            WindowsPrincipal principal = new WindowsPrincipal(id);

            var localAdmin = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            var domainAdmin = new SecurityIdentifier(WellKnownSidType.AccountDomainAdminsSid, id.User.AccountDomainSid);
            Console.WriteLine($"Is Local Admin: {principal.IsInRole(localAdmin)}");
            Console.WriteLine($"Is Domain Admin: {principal.IsInRole(domainAdmin)}");

            
            SetPrincipal();
            UsePrincipal();
            Console.ReadLine();
        }

        private static void UsePrincipal()
        {
            var p = Thread.CurrentPrincipal;
            Console.WriteLine($"{p.Identity.Name}:{Thread.CurrentPrincipal.IsInRole("Developer")}");

            // PrincipalPermission
            new PrincipalPermission(null, "Developer").Demand();  //Throws an exception
            DoDeveloperWork();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Developer")]
        private static void DoDeveloperWork()
        {
            try{
                Console.WriteLine("Development Role");
             }catch(Exception ex){
                Console.WriteLine(ex.Message);

            }
        }

        private static void SetPrincipal()
        {
            GenericIdentity identity = new GenericIdentity("Bob");
            string[] roles = new string[] { "Developer", "Marketing" };

            GenericPrincipal principal = new GenericPrincipal(identity, roles);
            Thread.CurrentPrincipal = principal;
        }
    }
}
