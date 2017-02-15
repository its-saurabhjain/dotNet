using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetFundamentals
{
    class CastingExample
    {
        public void Sample() {

            //Boxing and Unboxing (Is/As/Cast)
            Lion _lion = new Lion();
            Mamals _mamals1;
            Mamals _mamals2;
            //Use the is operator to verify the type before performing a cast.
            if (_lion is Mamals)
            {
                _mamals1 = (Mamals)_lion;
                Console.WriteLine(_mamals1.GetType());
            }

            _mamals2 = _lion as Mamals;
            if (_mamals2 != null)
            {
                Console.WriteLine(_mamals2.ToString());
            }

        }
    }
}
