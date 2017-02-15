using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQFundamentals
{
    class Movie
    {

        public string Title { get; set;}
        public double Ratings { get; set; }
        int _year;
        public int Year {
            get {
                Console.WriteLine($"Returning Year {_year}: for title {Title}");
                return _year;
            }
            set { _year = value; }
        }
    }
}
