using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetFundamentals
{
    class CovaInArray {

        public CovaInArray() {
            //Implicit conversion of array of more derived to a less derived type.
            Mamals[] mArray = new Lion[3];
            mArray[0] = new Lion();
            Console.WriteLine(mArray[0].GetType());
            object[] o = new string[2];
            o[0] = "123";
            //Not type safe the statement below will throw an exception
            o[1] = 123;

        }
    }
    class CovarAndContravarDelegates
    {

        //2. Covariance and Contravariance in delegates
        //Covariance deals with return types and Contravariance deals with method arguments
        //Covariance preserve the type assignment compatibility and Contravariance revereses it
        //Contravar:- You can declare a delegate with less derive return type and can assign a method which returns a more derive type to the delegate
        delegate Mamals MamalsFactoryDelegate();
        delegate Lion LionFactoryDelegate();
        delegate void MoveLionDelegate(Lion a);
        delegate void MoveMamalsDelegate(Mamals m);

        public void Execute()
        {
            MamalsFactoryDelegate af = CreateLion;
            af.Invoke();
            ////Below will not compile as the return type of method is less derived than the return type of delegate
            //LionFactoryDelegate lf = CreateMamal;
            ////Contravariance
            MoveLionDelegate mldel = MamalsMove;
            ////If delegate has input parameter which is less derived type than the method input paramenett it will be compilation error
            //MoveMamalsDelegate mmdel = LionMove;
        }
        public Mamals CreateMamal() {
            return new Mamals();
        }
        public Lion CreateLion() {
            return new Lion();
        }
        public void MamalsMove(Mamals m){}
        public void LionMove(Lion l){}
    }
    class CovarAndContravarGeneric {

        /* Rules
         1. Cannot be used for ref and out parameters and also with value type
         2. for interface to be covariance out keyword is ued with the generic type parameter. The type parameter should be used only with return types and not with 
         input type arguments, with one exception if the method takes an input generic delegate which is contravariant. It cannot be used as method constraint
         3. for interface to be contravariant use in keyword for generic type parameter. Type should be used as input to method and cannot beused as return type. 
         The type can act as constraint to method.
         4. The interface can be covariant and contravariant at the same time but for different type parameter
         5. The classes derived from variant interfaces are invariant
         6. Extend Interface rule
            6a. To extend an interface from variant interfaces explicitly specify in and out keywords in the extended interface. Framework doesn't infer 
            6b. If an interface extends both covariant and contravariant interfaces with same type, the resultant interface is invariant
            6c. If a generic interface is decl covariant in 1 interface it cannot be declared as contravariant in another interface.
        7. Avoid ambiguity in geneic interfaces: 
        if you explicitly implement the same variant generic interface with different generic type parameters in one class, it can create ambiguity
         */

    }
}
