using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetFundamentals
{
    public interface IAnimal
    {

    }
    public interface IHerbivorous
    {
    }
    public interface ICarnivorous
    {

    }
    public class Mamals
    {
        public Mamals()
        {
            Console.WriteLine("Mamals created");
        }
    }
    public class Lion : Mamals
    {
        public Lion() {
            Console.WriteLine("Lion Created");
        }
    }
    public class Planet { }
}
