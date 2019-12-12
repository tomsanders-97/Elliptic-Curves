using System;

namespace EllipticProgrammes
{
    class TorsionCalculator
    {
        public static void Method(int a2, int a4, int a6)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("This programme is used to perform several operations on elliptic curves.");
            Console.WriteLine("The Elliptic curve will be defined in the following form:");
            Console.WriteLine("y^2 = x^3 + a2 x^2 + a4 x + a6.");

            // Initialising variables before loop to avoid errors.
            var ellipticCurveCheck = 1;
            ellipticCurveCheck = -4 * a2 * a2 * a2 * a6 + a2 * a2 * a4 * a4 + 18 * a2 * a4 * a6 - 4 * a4 * a4 * a4 - 27 * a6 * a6;

            /// This ends the input from the major program.
            // Here, we are looking for the largest d such that d^2 divides ellipticCurveCheck.

            int n = 3;
            int delta = ellipticCurveCheck;
            int d = 1;
            if (delta < 0)
            {
                delta = -1 * delta;
            }
            float sqrt = (float)Math.Sqrt(delta);
            int lim = Convert.ToInt32(Math.Ceiling(sqrt));

            while (delta % 4 == 0) // If delta is a multiple of 4, then ellipticCurveCheck divides 2^2, so we include this.
            {
                d = 2 * d;
                delta = delta / 4;
            }

            while (n <= lim || delta <= (n * n)) // Here we check for multiples of odd squares in delta.
            {
                if (delta % (n * n) == 0)
                {
                    d = n * d;
                    delta = delta / (n * n);
                }
                else
                {
                    n = n + 2;
                }
            }

            ///            Console.WriteLine("The highest square factor of the discriminant of the curve is {0}", d);

            /* I chose to find each potential point of finite order in its totality, rather than save each potential y value,
             * and perform the operation in two chunks. 
             * This means there are two methods going on at the same time, which will limit readability of
             * the code. However, I believe this is a simpler solution for me to write.
             * To do this, we need to find all the factors of d.
             * For each factor of d, we name the factor y0, and try to find integer valued solutions x0 which solve the equation. 
             * If we find such an x0, then both the coordinates (x0,y0) and (x0,-y0) are potential points of finite order.
             * We will store these points in an array, and work out which are true points of finite order after.
             */

            // We must first check for points of finite order with y coordinate y0 = 0.
            float[] potentialPoints = new float[4 * d];
            bool[] confirmation = new bool[2 * d];
            int y0 = 0; int torsionCheck; int counter = 0; int constant;
            if (a6 == 0)
            {
                constant = a4;
            }
            else
            {
                constant = a6;
            }
            constant = Math.Max(constant, -1 * constant);
            for (int x0 = -1 * constant; x0 <= constant; x0++)
            {
                torsionCheck = (x0 * x0 * x0) + (a2 * x0 * x0) + (a4 * x0) + (a6);
                if (torsionCheck == 0)
                {
                    ///                    Console.WriteLine("Potential point of finite order: ({0},{1})", x0, 0);
                    potentialPoints[counter] = x0;
                    potentialPoints[counter + 1] = 0;
                    confirmation[counter / 2] = true;
                    counter = counter + 2;
                }
            }

            for (n = 1; n <= d; n++)
            {
                if (d % n == 0)
                {
                    y0 = n; // y0 is a factor of d, and potentially the y coordinate of a point of finite order.
                    constant = a6 - (y0 * y0);
                    constant = Math.Max(constant, -1 * constant);

                    for (int x0 = -1 * constant; x0 <= constant; x0++)
                    {
                        // Checking if x0 solves the elliptic equation at y coordinate y0.
                        torsionCheck = (x0 * x0 * x0) + (a2 * x0 * x0) + (a4 * x0) + (a6 - (y0 * y0));

                        if (torsionCheck == 0)
                        {
                            ///                            Console.WriteLine("Potential point of finite order: ({0},{1})", x0, y0);
                            potentialPoints[counter] = x0;
                            potentialPoints[counter + 1] = y0;
                            confirmation[counter / 2] = true;
                            counter = counter + 2;
                            ///                            Console.WriteLine("Potential point of finite order: ({0},{1})", x0, -1*y0);
                        }
                    }

                }
            }

            /* The next step is to elliminate any impossible points of finite order. We do this by using the fact that points of
             * finite order have integer coordinates. Hence, if for a point P(x0,y0) with x0,y0 integers, if nP(xn,yn) for any n has
             * non-integer coordinates, then P is of infinite order, and we can elliminate P from consideration.
             */
            float px; float py;
            float m;
            float c;
            float xInterim;
            float yInterim;
            int a = 0;
            bool check = false;
            int overallOrder = 1; int doubleOrder = 1;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("------------------------------------");
            Console.ForegroundColor = ConsoleColor.Gray;
            for (counter = 0; counter < 4 * d; counter = counter + 2)
            {
                if (confirmation[counter / 2] == true)
                {
                    if (potentialPoints[counter + 1] == 0)
                    {
                        ///                        Console.WriteLine("Now considering the point ({0},0).", potentialPoints[counter]);
                        Console.WriteLine("The point ({0},0) has order 2.", potentialPoints[counter]);
                        if (overallOrder == 1)
                        {
                            overallOrder = 2;
                        }
                        else if (overallOrder == 2)
                        {
                            doubleOrder = 2;
                        }
                    }
                    else
                    {
                        ///                        Console.WriteLine("Now considering the points ({0},{1}) and ({2},{3}).",
                        ///                        potentialPoints[counter], potentialPoints[counter + 1],
                        ///                        potentialPoints[counter], -1 * potentialPoints[counter + 1]);
                        px = potentialPoints[counter];
                        py = potentialPoints[counter + 1];
                        float[] multiples = new float[800]; // An array of 800 floats, so we can calculate as many multiples of P
                        multiples[0] = px;                  // as required.
                        multiples[1] = py;
                        m = (3 * px * px + 2 * a2 * px + a4) / (2 * py);
                        c = py - m * px;

                        float pPluspx = m * m - a2 - (2 * px);
                        float pPluspy = -(m * pPluspx + c);
                        multiples[2] = pPluspx;             // Coordinates of the point 2P.
                        multiples[3] = pPluspy;

                        if (multiples[2] != Math.Floor(multiples[2]) || multiples[3] != Math.Floor(multiples[3]))
                        {
                            Console.WriteLine("The points ({0},{1}) and ({2},{3}) are of infinte order.",
                            potentialPoints[counter], potentialPoints[counter + 1],
                            potentialPoints[counter], -1 * potentialPoints[counter + 1]);
                        }
                        else if (multiples[0] == multiples[2] && multiples[1] == -1 * multiples[3])
                        {
                            Console.WriteLine("The points ({0},{1}) and ({2},{3}) have order 3.",
                            potentialPoints[counter], potentialPoints[counter + 1],
                            potentialPoints[counter], -1 * potentialPoints[counter + 1]);
                            overallOrder = 3;
                        }
                        else
                        {
                            do
                            {
                                check = false; a = a + 2;
                                ///                                Console.WriteLine("{0}P = ({1},{2})", (a / 2) + 1, multiples[a], multiples[a + 1]);

                                m = (multiples[a + 1] - multiples[1]) / (multiples[a] - multiples[0]);
                                c = (multiples[1] * multiples[a] - multiples[0] * multiples[a + 1]) / (multiples[a] - multiples[0]);

                                xInterim = m * m - a2 - multiples[0] - multiples[a];
                                yInterim = -1 * (m * xInterim + c);

                                multiples[a + 2] = xInterim;
                                if (yInterim == 0)
                                {
                                    multiples[a + 3] = 0;
                                }
                                else
                                {
                                    multiples[a + 3] = yInterim;
                                }
                                ///                                Console.WriteLine("{0}P = ({1},{2})", (a / 2) + 2, multiples[a + 2], multiples[a + 3]);

                                if (multiples[0] == multiples[a] && multiples[1] == -1 * multiples[a + 1])
                                { // If nP = -P, then the order of P is n+1.
                                    Console.WriteLine("The points ({0},{1}) and ({2},{3}) have order {4}.",
                                    potentialPoints[counter], potentialPoints[counter + 1],
                                    potentialPoints[counter], -1 * potentialPoints[counter + 1],
                                    (a / 2) + 2);
                                    overallOrder = (a / 2) + 2;
                                    check = true;
                                }

                                if ((xInterim - Math.Floor(xInterim) > 0.00001) || (yInterim - Math.Floor(yInterim) > 0.00001))
                                { // If xInterim or yInterim are not integers.
                                    Console.WriteLine("The points ({0},{1}) and ({2},{3}) are of infinte order.",
                                    potentialPoints[counter], potentialPoints[counter + 1],
                                    potentialPoints[counter], -1 * potentialPoints[counter + 1]);
                                    check = true;

                                }
                            }
                            while (check == false);
                        }
                    }
                }
                else
                {
                    counter = 4 * d;
                }
            }
            overallOrder = overallOrder * doubleOrder;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("The order of the elliptic curve is {0}", overallOrder);
            Console.WriteLine("------------------------------------");
            Console.ForegroundColor = ConsoleColor.Gray;


            /*
             * This code will shorten the program, but will make the output look sloppy, so for now I'll leave 
             * it to be slower, but the potential points of finite order will be ordered by |y0|.
            if (m != d / m)
            {
                Console.WriteLine(d / m);
                Console.WriteLine(-1 * d / m);
            }
             */
        }
    }
}