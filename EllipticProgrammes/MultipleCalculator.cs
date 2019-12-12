using System;

namespace EllipticProgrammes
{
    class MultipleCalculator
    {
        public static void Method(int a2, int a4, int a6)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("This programme is used to multiply a point on an elliptic curve by a number.");
            Console.WriteLine("The Elliptic curve will be defined in the following form:");
            Console.WriteLine("y^2 = x^3 + a2 x^2 + a4 x + a6.");
            Console.WriteLine("The programme will take the point P(x,y) and the number n, and calculate the coordinates of the point nP.");

            // Initialising variables before loop to avoid errors.
            var curveCheckP = 1;
            var px = 1;
            var py = 1;

            // Input of the point P, avoiding points which are not on the curve.
            do
            {
                Console.WriteLine("Please input coordinates of point P");
                Console.WriteLine("P(x):");
                px = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("P(y):");
                py = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Thank you.");
                curveCheckP = (px * px * px) + a2 * (px * px) + a4 * (px) + a6 - (py * py);

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("The point P is ({0},{1})", px, py);
                if (curveCheckP != 0)
                {
                    Console.WriteLine("Invalid coordinate choice; please try again.");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            while (curveCheckP != 0);

            Console.WriteLine("Please input the number n");
            Console.WriteLine("n:");
            int n = Convert.ToInt32(Console.ReadLine());
            float[] multiples = new float[2 * (n)]; // An array of 2n integers, for n multiples of P(x,y).
            multiples[0] = px;
            multiples[1] = py;

            float m;
            float c;
            float xInterim;
            float yInterim;

            // Need to calculate 2P first.
            m = (3 * px * px + 2 * a2 * px + a4) / (2 * py);
            c = py - m * px;

            float pPluspx = m * m - a2 - (2 * px);
            float pPluspy = -(m * pPluspx + c);
            multiples[2] = pPluspx;
            multiples[3] = pPluspy;



            for (int a = 2; a < 2 * n - 2; a = a + 2)
            {
///                Console.WriteLine("Step {0}", (a / 2) + 1);
///                Console.WriteLine("{0}P = ({1},{2})", (a / 2) + 1, multiples[a], multiples[a + 1]);

                m = (multiples[a + 1] - multiples[1]) / (multiples[a] - multiples[0]);
                c = (multiples[1] * multiples[a] - multiples[0] * multiples[a + 1]) / (multiples[a] - multiples[0]);

                xInterim = m * m - a2 - multiples[0] - multiples[a];
                yInterim = -1 * (m * xInterim + c);

                multiples[a + 2] = (float)xInterim;
                multiples[a + 3] = (float)yInterim;
///                Console.WriteLine("{0}P = ({1},{2})", (a / 2) + 2, multiples[a + 2], multiples[a + 3]);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("------------------------------------");
            Console.WriteLine("{0}P({1},{2}) = ({3},{4})", n, px, py, multiples[(2 * n) - 2], multiples[(2 * n) - 1]);
            Console.WriteLine("------------------------------------");
            Console.ForegroundColor = ConsoleColor.Gray;

            /*                  Rough code to repeatedly double a point. Will use later.
            for (int a = 0; a < 2 * n; a = a + 2)
            {
                Console.WriteLine("Step {0}", a / 2);
                Console.WriteLine("{0}P = ({1},{2})", (a / 2) + 1, multiples[a], multiples[a + 1]);
                m = (3 * multiples[a] * multiples[a] + 2 * a2 * multiples[a] + a4) / (2 * multiples[a + 1]);
                c = multiples[a + 1] - m * multiples[a];

                xInterim = (m * m) - a2 - (2 * multiples[a]);
                yInterim = -((m * xInterim) + c);

                multiples[a + 2] = (float)xInterim;
                multiples[a + 3] = (float)yInterim;
                Console.WriteLine("{0}P = ({1},{2})", (a/2) + 2, multiples[a + 2], multiples[a + 3]);
            }
            */
        }
    }
}
