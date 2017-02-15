using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LINQFundamentals
{
    class JGA
    {
        static void Main(string[] args)
        {
            var cars = ProcessCars(@"..\debug\data\fuel.csv");
            var manufacturers = ProcessManufacturer(@"..\debug\data\manufacturers.csv");

            //Join operators
            /*
           var query = from car in cars
                       join m in manufacturers on car.Manufacturer equals m.Name
                       orderby car.Combined descending, car.Name ascending
                       select new
                       {
                           m.Headquarters,
                           car.Name,
                           car.Combined
                       };

           //Composite query
           var query1 = from car in cars
                       join m in manufacturers on
                       new { car.Manufacturer, car.Year } 
                       equals 
                       new { Manufacturer = m.Name, m.Year }
                       orderby car.Combined descending, car.Name ascending
                       select new
                       {
                           m.Headquarters,
                           car.Name,
                           car.Combined
                       };

           var query2 = cars.Join(manufacturers,
                                   c => c.Manufacturer,
                                   m => m.Name,
                                   (c, m) => new
                                   {
                                       m.Headquarters,
                                       c.Name,
                                       c.Combined
                                   })
                             .OrderByDescending(c => c.Combined)
                             .ThenBy(c => c.Name);
           //Composite
           var query21 = cars.Join(manufacturers,
                                   c => new { c.Manufacturer, c.Year },
                                   m => new { Manufacturer = m.Name, m.Year },
                                   (c, m) => new
                                   {
                                       m.Headquarters,
                                       c.Name,
                                       c.Combined
                                   })
                             .OrderByDescending(c => c.Combined)
                             .ThenBy(c => c.Name);
           foreach (var group in query)
            {
                Console.WriteLine($"{group.Key} has {group.Count()} cars");
            }


           */
            //Grouping
            /*
            var query = from car in cars
                        group car by car.Manufacturer.ToUpper() into m
                        orderby m.Key
                        select m;
            //Group join methos syntax
            var query2 = cars.GroupBy(c => c.Manufacturer.ToUpper())
                            .OrderBy(g => g.Key);
            foreach (var group in query)
            {
                //Console.WriteLine($"{group.Key} has {group.Count()} cars");
                Console.WriteLine(group.Key);
                foreach (var item in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\t{item.Name}: {item.Year}");
                }
            }
            */

            //Group Join
            var query = from manufacturer in manufacturers
                        join car in cars on manufacturer.Name equals car.Manufacturer
                        into carGroup
                        select new
                        {
                            Manufacturer = manufacturer,
                            Cars = carGroup
                        };
            var query2 = manufacturers.GroupJoin(cars, 
                                                    m => m.Name, 
                                                    c => c.Manufacturer, 
                                                    (m,g) =>
                                                    new
                                                    {
                                                       Manufacturer = m,
                                                       Cars = g
                                                    }).OrderBy(m=> m.Manufacturer.Name);

            foreach (var group in query2)
            {
                Console.WriteLine($"{group.Manufacturer.Name}: {group.Manufacturer.Headquarters}");
                foreach (var item in group.Cars.OrderByDescending(c=> c.Name).Take(2))
                {
                    Console.WriteLine($"\t{item.Name}: {item.Year}");
                }
            }

            Console.ReadLine();


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
