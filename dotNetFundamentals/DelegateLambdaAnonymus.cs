using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetFundamentals
{
    class DelegateLambdaAnonymus
    {

        delegate Mamals MyMamalDelegate(Lion l);
        public DelegateLambdaAnonymus(){

            //calling a delegate
            MyMamalDelegate mydel = Method1;
            mydel.Invoke(new Lion());

            //Anonymous method
            MyMamalDelegate mydel1 = delegate (Lion l)
            {
                return new Mamals();
            };
            //Lambda
            //1. remove delegate and parameter type and add lambda =>
            //MyMamalDelegate mydel1 = (l) => {return new Mamals();};
            //2.remove curly braces, returns and semicolon if it is single statement
            //MyMamalDelegate mydel2 = (l) => new Mamals();
            //3. remove parenthesi around parameter if it is only 1 parameter
            MyMamalDelegate mydel2 = l => new Mamals();
        }
        public Mamals Method1(Lion l)
        {
            return new Mamals();
        }

    }
}
