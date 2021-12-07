using System;
using static System.Console;
using static System.Math;

namespace ads_lab3_shell
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            OutputEncoding = System.Text.Encoding.UTF8;
            int N;
            WriteLine("Сортування Шелла");
            Write("Виберіть розмір послідовності: "); N = Convert.ToInt32(ReadLine());
            int[] arr = new int[N];         //створюємо два масиви, один з яких відсортуємо
            int[] arrChange = new int[N];
            bool[] color = new bool[N];     //створюємо масив для того, щоб відстежити, які елменти зафарбувати

            Write("Оберіть спосіб генерації (1 - псевдовипадково, 2 - контрольний приклад): ");
            int choice = Convert.ToInt32(ReadLine());

            switch(choice)
            {
                case 1:
                    arrChange = generateNums(arrChange);
                    break;
                case 2:
                    arrChange = generateCtrl(arrChange);
                    break;
                default:
                    WriteLine("ПОМИЛКА: введіть значення 1 або 2");
                    ReadKey();
                    return;
            }

            arrChange.CopyTo(arr, 0);   //копіюємо масив, який будемо змінювати, у інший з 0-го елементу

            arrChange = SortArray(arrChange);   //сортуємо методом Шелла
            color = consoleColor(arrChange, arr, color);    //визначаєм колір кожного елементу послідовності

            WriteLine("Невідсортована послідовність");
            printArr(arr, color);
            WriteLine();
            ForegroundColor = ConsoleColor.White;
            WriteLine("Відсортована послідовність");
            printArr(arrChange, color);
            ReadKey();

        }
        static int[] generateNums(int[] arr)    //функція для генерації прикладу із псевдовипадковими числами
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr = check(arr, i);
            }
            return arr;
        }
        static int[] generateCtrl(int[] arr)    //функція для генерації контрольного прикладу
        {
            int k = -20;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = k;
                k++;
            }
            return arr;
        }
        static int[] check(int[] arr, int i)    //рекурсивна функція, яка запобігає повторенню значенб елементів
        {
            arr[i] = rnd.Next(-100, 101);
            if (i > 0)
            {
                for (int j = 0; j < i; j++)
                {
                    if (arr[j] == arr[i])
                        check(arr, i);
                }
            }
            return arr;
        }
        static int[] SortArray(int[] arr)
        {
            int gap = arr.Length/2;
            int buff;

            while (gap >= 1)
            {
                for (int i = gap; i < arr.Length; i++)
                {
                    for (int j = i; j >= gap; j-=gap)
                    {
                        if ((Abs(arr[j - gap]) > Abs(arr[j])) &&
                            arr[j - gap] < 0 && arr[j] < 0 &&
                            arr[j - gap] % 2 == 0 && arr[j] % 2 == 0)
                        {
                            buff = arr[j];
                            arr[j] = arr[j - gap];
                            arr[j - gap] = buff;
                        }
                        else
                            break;
                    }
                }
                gap /= 2;
            }
            return arr;
        }
        static bool[] consoleColor(int[] arr1, int[] arr2, bool[] color)    //функція для з'ясування, які елементи повинні зафарбуватись
        {
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == arr2[i])
                {
                    color[i] = false;
                }
                else
                    color[i] = true;
            }
            return color;    
        }
        static void printArr(int[] arr, bool[] color)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (color[i] == true)
                {
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write(arr[i] + "  ");
                }
                else
                {
                    ForegroundColor = ConsoleColor.DarkRed;
                    Write(arr[i] + "  ");
                }
            }
        }
    }
}
