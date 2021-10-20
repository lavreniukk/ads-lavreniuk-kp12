using System;
using static System.Console;
using static System.Math;

namespace ads_lab01_1
{
    class Program
    {
        static void Main(string[] args)
        {
            double x, y, z;
            double a;
            double b;

            Write(" x = "); x = Convert.ToDouble(ReadLine());
            Write(" y = "); y = Convert.ToDouble(ReadLine());
            Write(" z = "); z = Convert.ToDouble(ReadLine());

            double a1 = (Pow(x,2) - (y * Pow(z, 3.0)));

            /*через те, що функція Pow не може дати від'ємні значення на відміні від
            кубічного кореня, скористаємось додатковою змінною, щоб зберегти властивості кубічного кореня
            на блок-схемі дані дії можна упустити, оскільки використовуємо математичні дії, 
            які зберігають властивості кубічного кореня*/

            if (a1 < 0)
            {
                a1 *= (-1.0);
                a1 = Pow(a1, (1.0 / 3.0));
                a1 *= -1.0;
            }
            else
                a1 = Pow(a1, (1.0 / 3.0));

            if ((Pow(x, z) * y) - a1 != 0)
            {
                a = (x + y - z) / ((Pow(x, z) * y) - a1);
                WriteLine("Result a = " + a);

                if (a != 0)
                {
                    b = Cos((x * y + Pow(y, 2.0)) / Pow(a, 2.0));
                    WriteLine("Result b = " + b);
                }
                else
                    WriteLine("B can't be counted");
            }
            else
                WriteLine("ERROR: incorrect data");

            ReadKey();

        }
    }
}
