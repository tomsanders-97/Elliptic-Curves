# Elliptic Operations tool
Version 1.0

This is a tool which automates the task of performing operations on a given elliptic curve. It does this much faster, and with fewer 
mistakes than calculating the same operations by hand.
The tool has three main functionalities:
1.	 Adding any two points together
2.	Multiplying any point by a positive number
3.	Finding the torsion subgroup of the curve

Currently, the tool is limited in its input to curves in medium Weierstrass form; however, future versions will be improved to take
input of curves in long Weierstrass form.



# Table of Contents
1.  [System Requirements](#system-requirements)
2.  [Usage](#usage)
3.  [Why Is This Necessary](#why-is-this-necessary)
4.  [Further Development](#further-development)
5.  [Method Breakdown](#method-breakdown)



## System Requirements
This program runs on the .NET framework. Hence to run it, you must have a version of the .NET framework runtime. Ideally, the latest
version - version 4.8 will be used.

[Download .NET framework 4.8 runtime](https://dotnet.microsoft.com/download/dotnet-framework)


## Usage
This program is a standalone tool, though each operation is coded in as a method in its own right. Therefore, you would be able to call
particular methods using the following code:

```c#

                    EllipticProgrammes.SumCalculator.Method(a2, a4, a6);

                    EllipticProgrammes.MultipleCalculator.Method(a2, a4, a6);

                    EllipticProgrammes.TorsionCalculator.Method(a2, a4, a6);
```

Note that these methods take input of curve coefficients only, so additional input for SumCalculator and MultipleCalculator will be
required - currently this would take place on the console.


## Why Is This Necessary
This tool is designed to speed up the time taken when performing operations on elliptic curves. In my experience, calculating these
operations by hand can be notoriously prone to errors, and even using a computer to calculate each step can be tedious if many
calculations are required. Therefore, I decided to create this tool to speed up the process by automating each operation, and creating a simple interface to frame
the input and output effectively.


## Further Development
In the future, I hope to improve this tool, both in terms of accuracy and speed. I also plan to expand the possible input to allow
input of long Weierstrass form curves. For a better idea of my plans, here is a list of the potential changes I hope to make:
1.  QOL upgrade to code to improve readability and clean up output messages for ease of use.
2.  Improved method to MultipleCalculator to speed up process and catch points of finite order.
3.  Expansion of input and methods to work with long Weierstrass form curves.


## Method Breakdown
This tool has three main methods; one for each functionality. I will explain each method here, to make clear what is going on behind
the scenes.

### 1. Standard Point Addition
This method is made up of two applications of coordinate geometry. We take input of two points, P(x,y) and Q(x,y), and an elliptic curve
y^2 = x^3 + a_2 x^2 + a_4 + x + a_6.

First, we find the intersections between the curve, and the line connecting P and Q. By the design of the elliptic curve, there will be
precisely three intersections; P, Q, and a third intersection we will call R. Note, if we are faced with the case when P = Q, then we
define the line passing through P with multiplicity 2 to be the tangent to the curve at the point P. After this, we proceed as usual.
Note also that if the line passing through P and Q is a vertical line, then we say that the sum of P and Q is the point at infinity,
which is the null element of the group.

Then, all that is required is to find the intersections between the curve, and the vertical line passing through this point R. We will
call this point -R. It is this point which is equal to the sum of P and Q.

With this operation, we define a binary operation which defines an abelian group consisting of the points on the elliptic curve.

For more information, [click here](http://mathworld.wolfram.com/EllipticCurveGroupLaw.html)

### 2. Point Multiplication by a Number
In this case, we take input of one point, P(x,y), one positive integer, n, and an elliptic curve y^2 = x^3 + a_2 x^2 + a_4 + x + a_6.
This method is essentially a repeated application of the first method, beginning with the case where P = Q, to double the point P, to
give 2P. From here, we use the Standard Point Addition method to add P and 2P, then P and 3P etc, until we
find the coordinates of the point nP.

We can note here, that if at any point we find m such that mP = -P, such that mP and P are vertically opposite one another, then we can
say that the point P has order m. As such, it will be true that for another number x == n (mod m), nP = xP. This fact is not yet 
considered by the program. As such, if we try to consider nP for n greater than the order of P, the program will return an error.

### 3. Calculation of the Torsion Subgroup
This functionality takes input only of the elliptic curve equation y^2 = x^3 + a_2 x^2 + a_4 + x + a_6. This method is far more
complicated than the other two methods. The torsion subgroup is essentially the subgroup containing all points on an elliptic curve
which have the property of finite order (i.e. there exists some n such that (n-1)P = -P). It can be proven that on a rational elliptic
curve, such a point must have integer coordinates.

First, we calculate the discriminant of the curve, using the following formula:

[Delta](http://mathworld.wolfram.com/EllipticDiscriminant.html) = -a_2^2 a_8 - 8 a_4^3 - 27 a_6^2 + 9 a_2 a_4 a_6

 
Then, we find the highest integer d such that d^2 | Delta, and find all factors of d. This gives us a list of y-coordinates of potential
points of finite order. We then look for integer values of x such that (x,y) is a point on the curve with integer coordinates. This
gives us a list of potential points of finite order.

Finally, we must attempt to find the order of these points. To do this, we use the method of Point Multiplication by a Number repeatedly
until we find either
1.  n such that (n-1)P = -P, which means that the point P(x,y) has order n, or
2.  n such that nP has non-integer coordinates in either x or y, in which case we say the point P has infinite order.

Therefore, at the end we are left with a set of points with integer coordinates, and we know their respective orders. From here, we can
also calculate the overall order of the subgroup.
