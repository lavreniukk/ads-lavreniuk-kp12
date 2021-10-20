using System;
using static System.Console;
using static System.Math;


namespace task_2
{
    class Program
    {
        static void Main()
        {
            int d1, d2;
            int m1, m2;
            int y1, y2;
            int[] month = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            Write("Day1 = "); d1 = Convert.ToInt32(ReadLine());
            Write("Day2 = "); d2 = Convert.ToInt32(ReadLine());
            Write("Month1 = "); m1 = Convert.ToInt32(ReadLine());
            Write("Month2 = "); m2 = Convert.ToInt32(ReadLine());
            Write("Year1 = "); y1 = Convert.ToInt32(ReadLine());
            Write("Year2 = "); y2 = Convert.ToInt32(ReadLine());

            
            if (m1 <= month.Length && m2 <= month.Length && d1 <= month[m1] && d2 <= month[m2] && y1 <= y2) //перевіримо коректність введенх дат 
            {
                //перевіримо чи є рік високосним
                bool leapyear1;                
                if (y1 % 4 == 0 && y1 % 100 != 0)
                    leapyear1 = true;
                else if (y1 % 400 == 0)
                    leapyear1 = true;
                else 
                    leapyear1 = false;

                bool leapyear2;
                if (y2 % 4 == 0 && y2 % 100 != 0)
                    leapyear2 = true;
                else if (y2 % 400 == 0)
                    leapyear2 = true;
                else
                    leapyear2 = false;

                int summ1 = 0, summ2 = 0;
                for (int i = m1 - 1; i < 12; i++) //рахуємо кількість днів до нового року
                {
                    if (leapyear1)
                        month[1] = 29;   //якщо високосний рік, у 2 місяці буде 29 днів
                    else
                        month[1] = 28;
                    summ1 += month[i];      //сума днів в наступних від m1 місяцях
                }  

                for (int i = 1; i < m2 - 1; i++)
                {
                    if (leapyear2)
                        month[1] = 29;
                    else month[1] = 28;
                    summ2 += month[i];    //сума днів в наступних від m2 місяцях
                }

                int k = 0;
                if (leapyear1)  //у високосний рік виводило на 1 день більше
                    if (y1 == y2)
                        k = 1;

                int s = month[m1 - 1] - d1 + d2 + summ1 + summ2 - k;
                int j = 0;
                for (int i = y1 + 1; i <= y2 - 1; i++)
                {
                    if (i % 4 == 0 && i % 100 != 0)
                        j++;
                    else if (i % 400 == 0)
                        j++;
                }

                int years = 0;
                if (m2 > m1)
                    years = y2 - y1;
                else
                {
                    if (m2 == m1 && d2 >= d1)
                        years = y2 - y1;
                    else
                        years = y2 - y1 - 1;

                }

                Write("Number of days: ");
                int days = s + 366 * j + 365 * (y2 - 1 - y1 - j);
                WriteLine(days);
                WriteLine("Number of full years: " + years);
            }
            else
                WriteLine("Incorrect data");

                ReadKey();
        }
    }
}