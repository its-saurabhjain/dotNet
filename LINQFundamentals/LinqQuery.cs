using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQFundamentals
{
    class LinqQuery
    {
        static void Main(string[] args)
        {

            var movies = new List<Movie>()
            {
                new Movie() {Title="The Dark Night", Ratings=8.9f,  Year=2008},
                new Movie() {Title="The King's Speech", Ratings=8.0f,  Year=2010},
                new Movie() {Title="Casablanca", Ratings=8.5f,  Year=1942},
                new Movie() {Title="Star War V", Ratings=8.7f,  Year=1980},

            };
            //var query = movies.Where(m => m.Year > 2000);
            //var query = movies.Filter(m => m.Year > 2000);
            var query = movies.Filter2(m => m.Year > 2000);

            //later
            /*
            query = query.Take(1);
            foreach (var mov in query)
            {
                Console.WriteLine($"{mov.Title}");
            }
            */
            //above for loop using enumerator sample
            //Console.WriteLine(query.Count()); //executes the query 2 times, in order to avoid this use immediate execution/materiliazion using ToList() on the query
            var enumerator = query.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine($"{enumerator.Current.Title}");
            }
            //Streaming operators and non streaming operator (orderby and orderby descending)
            Console.WriteLine("**************Orderby***********************");
            var query1 = movies.Where(m => m.Year > 2000)
                        .OrderBy(m => m.Year);
            var enumerator1 = query1.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                Console.WriteLine($"{enumerator1.Current.Title}");
            }

            Console.ReadLine();

        }
    }

    public static class CustomLinqFilter {

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            var result = new List<T>();
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }
             return result;
        }
        //Sample using yield return
        //Yield helps to build an IEnumerable, any method that uses yield return should have a return type of IEnumerable or IEnumerable<T>
        //This gives a method for deffere execution
        public static IEnumerable<T> Filter2<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
           foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}
