using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LINQFundamentals
{
    class Linq2Xml
    {
        static void Main(string[] args)
        {
            var cars = ProcessCars(@"..\debug\data\fuel.csv");
        }
        private static List<Car> ProcessCars(string path)
        {
            //query syntax
            var query = from line in File.ReadAllLines(path).Skip(1)
                        where line.Length > 1
                        select Car.ParseFromCSV(line);
            return query.ToList();
            //method syntax
            /*
            return
                File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(Car.ParseFromCSV)
                .ToList();
                */
        }
    }
}
