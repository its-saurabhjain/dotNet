using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LINQFundamentals
{
    class FOP
    {
        static void Main(string[] args)
        {
            var cars = ProcessCars(@"..\debug\data\fuel.csv");
            var manufacturers = ProcessManufacturer(@"..\debug\data\manufacturers.csv");
            /*
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Name}");
            }
            */
            //most fuel efficient car
            var query = cars.OrderByDescending(car => car.Combined).ThenBy(car => car.Name).Take(10);
            foreach (var car in query)
            {
                Console.WriteLine($"{car.Name,-20} : {car.Combined,10:N0}");
            }
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
        private static List<Manufacturer> ProcessManufacturer(string path)
        {
            //query syntax
            /*
            var query = from line in File.ReadAllLines(path).Skip(1)
                        where line.Length > 1
                        select Manufacturer.ParseFromCSV(line);
            return query.ToList();
            */
            //method syntax

            var query = File.ReadAllLines(path).Skip(1)
                        .Where(line => line.Length > 1)
                        .Select(l =>
                        {
                            var columns = l.Split(',');
                            return new Manufacturer
                            {
                                Name = columns[0],
                                Headquarters = columns[1],
                                Year = int.Parse(columns[2])
                            };
                        });
            return query.ToList();
        }
    }
}
