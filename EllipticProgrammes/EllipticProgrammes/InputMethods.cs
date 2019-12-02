using System;
using System.Collections.Generic;
using System.Text;

namespace EllipticProgrammes
{
    class InputMethods
    {
        public static void ProductChoice(int a2, int a4, int a6)
        {
            int productChoice;
            Console.WriteLine("Please select which program you would like to use.");
            Console.WriteLine("For SumCalculator, enter {0}", 1);
            Console.WriteLine("For MultipleCalculator, enter {0}", 2);
            Console.WriteLine("For TorsionCalculator, enter {0}", 3);
            productChoice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Thank you.");

            do
            {
                if (productChoice == 1)
                {
                    EllipticProgrammes.SumCalculator.Method(a2, a4, a6);
                }
                else if (productChoice == 2)
                {
                    EllipticProgrammes.MultipleCalculator.Method(a2, a4, a6);
                }
                else if (productChoice == 3)
                {
                    EllipticProgrammes.TorsionCalculator.Method(a2, a4, a6);
                }
                else
                {
                    Console.WriteLine("Invalid product choice; please try again.");
                }
            }
            while (productChoice != 1 && productChoice != 2 && productChoice != 3);
        }







        public static bool NewProductChoice(int a2, int a4, int a6)
        {
            int newProductChoice;
            do
            {

                Console.WriteLine("If you would like to use another product with the same curve, press {0}", 1);
                Console.WriteLine("If you would like to use another produce with a different curve, press {0}", 2);
                Console.WriteLine("To exit the program, press {0}", 3);
                newProductChoice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Thank you.");

                if (newProductChoice == 1)
                {
                    EllipticProgrammes.InputMethods.ProductChoice(a2, a4, a6);
                    return false;
                }
                else if (newProductChoice == 2)
                {
                    EllipticProgrammes.Program.Main();
                    return false;
                }
                else if (newProductChoice == 3)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid product choice; please try again.");
                    return false;
                }
            }
            while (newProductChoice != 1 && newProductChoice != 2 && newProductChoice != 3);
        }
    }
}