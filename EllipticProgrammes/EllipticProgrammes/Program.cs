using System;

namespace EllipticProgrammes
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("This programme is used to perform several operations on elliptic curves.");
            Console.WriteLine("The Elliptic curve will be defined in the following form:");
            Console.WriteLine("y^2 = x^3 + a2 x^2 + a4 x + a6.");

            int a2; int a4; int a6;
            int ellipticCurveCheck;
            do
            {
                Console.WriteLine("Please input elliptic coeffieients");
                Console.WriteLine("a2:");
                a2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("a4:");
                a4 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("a6:");
                a6 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Thank you.");
                ellipticCurveCheck = -4 * a2 * a2 * a2 * a6 + a2 * a2 * a4 * a4 + 18 * a2 * a4 * a6 - 4 * a4 * a4 * a4 - 27 * a6 * a6;

                // Code to eliminate null terms                      (Could be shortened I'm sure).
                if (a4 == 0 && a6 == 0)
                {
                    Console.WriteLine("The elliptic curve equation is:");
                    Console.WriteLine("y^2 = x^3 + {0}x^2", a2);
                }
                else if (a2 == 0 && a6 == 0)
                {
                    Console.WriteLine("The elliptic curve equation is:");
                    Console.WriteLine("y^2 = x^3 + {0}x", a4);
                }
                else if (a2 == 0 && a4 == 0)
                {
                    Console.WriteLine("The elliptic curve equation is:");
                    Console.WriteLine("y^2 = x^3 + {0}", a6);
                }
                else if (a2 == 0)
                {
                    Console.WriteLine("The elliptic curve equation is:");
                    Console.WriteLine("y^2 = x^3 + {0}x + {1}", a4, a6);
                }
                else if (a4 == 0)
                {
                    Console.WriteLine("The elliptic curve equation is:");
                    Console.WriteLine("y^2 = x^3 + {0}x^2 + {1}", a2, a6);
                }
                else if (a6 == 0)
                {
                    Console.WriteLine("The elliptic curve equation is:");
                    Console.WriteLine("y^2 = x^3 + {0}x^2 + {1}x", a2, a4);
                }
                else
                {
                    Console.WriteLine("The elliptic curve equation is:");
                    Console.WriteLine("y^2 = x^3 + {0}x^2 + {1}x + {2}", a2, a4, a6);
                }

                if (ellipticCurveCheck == 0)
                {
                    Console.WriteLine("Invalid coefficient choice; please try again.");
                }
            }
            while (ellipticCurveCheck == 0);
            bool endCondition;
            EllipticProgrammes.InputMethods.ProductChoice(a2, a4, a6);
            do
            {
                endCondition = EllipticProgrammes.InputMethods.NewProductChoice(a2, a4, a6);
            }
            while (endCondition == false);
        }
    }
}