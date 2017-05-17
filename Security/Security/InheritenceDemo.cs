using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    class InheritenceDemo
    {
        static void Main(string[] args) {

            var wid = WindowsIdentity.GetCurrent();
            var gid = new GenericIdentity("Bob");


        }
    }

    class CorpIdentity : ClaimsIdentity {

        public CorpIdentity(string name, string retportsTo, string office)
        {
            AddClaim(new Claim(ClaimTypes.Name, name));
            AddClaim(new Claim("office", office));
            AddClaim(new Claim("reportsto", retportsTo));

        }

        //user can do a find claim or we can returns strongly typed properties.
        public string ReportsTo
        {
            get{
                return FindFirst("reportsto").Value;
            }
        }
        public string Office
        {
            get
            {
                return FindFirst("office").Value;
            }
        }

    }
}
