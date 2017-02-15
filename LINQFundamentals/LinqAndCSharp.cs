using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQFundamentals
{
    class LinqAndCSharp
    {
        static void Main(string[] args)
        {
            //Employee[] developers = new Employee[]
            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee { ID= "1", Name="Scott"},
                new Employee { ID= "2", Name="Peter"}
            };
            //List<Employee> sales = new List<Employee>()
            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee { ID= "3", Name="Alex"}
            };

            IEnumerator<Employee> enumerator = developers.GetEnumerator();
            Console.WriteLine("**********Extension Method********");
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Name.ToDouble());

            }
            //Named method--> anonymous method--> Lambda
            Console.WriteLine("**********Named Method**********");
            IEnumerable<Employee> developers1 = developers.Where(startswithP);
            foreach (Employee e in developers1)
            {
                Console.WriteLine($"{e.ID, -3}:{e.Name , 10}");
            }
            Console.WriteLine("**********Anonymous Method******");
            foreach (Employee e in developers.Where(delegate(Employee arg)
                                                {
                                                    return arg.Name.StartsWith("S");
                                                }))
            {
                Console.WriteLine($"{e.ID,-3}:{e.Name,10}");
            }
            Console.WriteLine("**********Lambda Expression******");
            foreach (Employee e in developers.Where(e=>e.Name.StartsWith("S")))
            {
                Console.WriteLine($"{e.ID,-3}:{e.Name,10}");
            }


            Console.ReadLine();

        }

        private static bool startswithP(Employee arg)
        {
            return arg.Name.StartsWith("P");
        }
    }

    //Creating Extension Method
    public static class StringExtension
    {
        public static string ToDouble(this string data)
        {
            return data + " -" + DateTime.Today;
        }

    }
}
