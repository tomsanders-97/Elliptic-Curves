using System;

namespace EllipticProgrammes
{
    class Program
    {
        static void Main()
        {
            // Initialising variables before loop to avoid errors.
            var ellipticCurveCheck = 1;
            var a2 = 1;
            var a4 = 1;
            var a6 = 1;

            // Input of coefficients, avoiding curves which have singular points.
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

                Console.WriteLine("The point P is ({0}, {1})", px, py);
                if (curveCheckP != 0)
                {
                    Console.WriteLine("Invalid coordinate choice; please try again.");
                }
            }
            while (curveCheckP != 0);

            // Initialising variables before loop to avoid errors.
            var curveCheckQ = 1;
            var qx = 1;
            var qy = 1;

            do
            {
                Console.WriteLine("Please input coordinates of point Q");
                Console.WriteLine("Q(x):");
                qx = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Q(y):");
                qy = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Thank you.");
                curveCheckQ = (qx * qx * qx) + a2 * (qx * qx) + a4 * (qx) + a6 - (qy * qy);

                Console.WriteLine("The point Q is ({0}, {1})", qx, qy);
                if (curveCheckQ != 0)
                {
                    Console.WriteLine("Invalid coordinate choice; please try again.");
                }
            }
            while (curveCheckQ != 0);

            float m = 1;
            float c = 1;
            // Initial checks and sum calculation:
            if (px == qx && py == 0 && qy == 0) // Case of point of order 2.
            {
                Console.WriteLine("This is the case of doubling the point P({0}, {1})", px, py);
                Console.WriteLine("This point has order 2.");
                Console.WriteLine("Therefore, the point 2P is precisely the point at infinity.");
            }
            else if (px == qx && py == -1 * qy) // Case of adding elements which are inverses of one another.
            {
                Console.WriteLine("The points P({0}, {1}) and Q({2}, {3}) are inverses of one another.", px, py, qx, qy);
                Console.WriteLine("Therefore, their sum is the point at infinity.");
            }
            else
            {
                if (px == qx && py == qy) // Case of doubling a point P.
                {
                    Console.WriteLine("This is the case of doubling the point P({0}, {1})", px, py);
                    m = (3 * px * px + 2 * a2 * px + a4) / (2 * py);
                    c = py - m * px;
                }
                else // Case of ordinary point addition.
                {
                    m = (qy - py) / (qx - px);
                    c = (py * qx - px * qy) / (qx - px);
                }

                float pPlusqx = m * m - a2 - px - qx;
                float pPlusqy = -(m * pPlusqx + c);

                Console.WriteLine("The coordinates of the point P+Q are ({0}, {1})", pPlusqx, pPlusqy);
            }
        }
    }
}