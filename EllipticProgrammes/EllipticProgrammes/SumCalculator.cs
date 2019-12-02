using System;

namespace EllipticProgrammes
{
    class SumCalculator
    {
        public static void Method(int a2, int a4, int a6)
        {
            Console.WriteLine("This programme is used to add two points on an the following elliptic curve:");
            Console.WriteLine("y^2 = x^3 + {0} x^2 + {1} x + {2}.", a2, a4, a6);
            Console.WriteLine("The programme will take the points P(px,py) and Q(qx,qy), and add them to find the coordinates of the point P+Q.");

            // Initialising variables before loop to avoid errors.
            int curveCheckP = 1;
            int px = 1;
            int py = 1;

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

                Console.WriteLine("The point P is ({0},{1})", px, py);
                if (curveCheckP != 0)
                {
                    Console.WriteLine("Invalid coordinate choice; please try again.");
                }
            }
            while (curveCheckP != 0);

            // Initialising variables before loop to avoid errors.
            int curveCheckQ = 1;
            int qx = 1;
            int qy = 1;

            do
            {
                Console.WriteLine("Please input coordinates of point Q");
                Console.WriteLine("Q(x):");
                qx = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Q(y):");
                qy = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Thank you.");
                curveCheckQ = (qx * qx * qx) + a2 * (qx * qx) + a4 * (qx) + a6 - (qy * qy);

                Console.WriteLine("The point Q is ({0},{1})", qx, qy);
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
                Console.WriteLine("This is the case of doubling the point P({0},{1})", px, py);
                Console.WriteLine("This point has order 2.");
                Console.WriteLine("Therefore, the point 2P is precisely the point at infinity.");
            }
            else if (px == qx && py == -1 * qy) // Case of adding elements which are inverses of one another.
            {
                Console.WriteLine("The points P({0},{1}) and Q({2},{3}) are inverses of one another.", px, py, qx, qy);
                Console.WriteLine("Therefore, their sum is the point at infinity.");
            }
            else
            {
                if (px == qx && py == qy) // Case of doubling a point P.
                {
                    Console.WriteLine("This is the case of doubling the point P({0},{1})", px, py);
                    m = (3 * px * px + 2 * a2 * px + a4) / (2 * py);
                    c = py - m * px;
                }
                else // Case of ordinary point addition.
                {
                    m = (qy - py) / (qx - px);
                    c = (py * qx - px * qy) / (qx - px);
                }

                float pPlusqx = m * m - a2 - px - qx;
                float pPlusqy;
                if (m * pPlusqx + c == 0)
                {
                    pPlusqy = 0;
                }
                else
                {
                    pPlusqy = -1 * (m * pPlusqx + c);
                }

                Console.WriteLine("The coordinates of the point P+Q are ({0},{1})", pPlusqx, pPlusqy);
            }
        }
    }
}