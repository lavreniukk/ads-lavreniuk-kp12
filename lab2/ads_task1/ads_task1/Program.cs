using System;
using System.Collections.Generic;
using static System.Console;
class Program
{
    static Random rnd = new Random();
    static void Main(string[] args)
    {
        OutputEncoding = System.Text.Encoding.UTF8;
        int N, M;
        Write(" N = "); N = Convert.ToInt32(ReadLine());
        Write(" M = "); M = Convert.ToInt32(ReadLine());

        int Left = M, Up = N - 1, Right = M - 1, Down = N - 2; //змінні які зупиняють кожен виток спіралі
        int MaxValue = 0, MinValue = N * M;
        int MinRow = 0, MinCol = 0, MaxRow = 0, MaxCol = 0;   //змінні для пошуку максимуму, мінімуму та їх індексів

        int[,] matrix = new int[N, M];
        List<int> spiral = new List<int>();

        Write(" Оберіть спосіб генерації матриці: 1 - псевдовипадково, 2 - контрольний приклад ");
        int answer = Convert.ToInt32(ReadLine());
        switch (answer)
        {
            case 1:
                {
                    matrix = genRandMatrix(N, M);
                    printMatrix(matrix);
                    WriteLine();
                }
                break;
            case 2:
                {
                    matrix = genCtrlMatrix(N, M);
                    printMatrix(matrix);
                    WriteLine();
                }
                break;
        }


        int i, j, l = 0, k = 0; //l - змінна за допомогою якої витки спіралі будуть "закручуватись" всередину
        while (k < N*M) //перевірка на кількість елементів матриці
        {
            int n, m;
            i = N - 1 - l;
            j = M - 1 - l;
            for (m = 0; m < Left; m++)
            {
                if ((i < j) && (i + j > M - 1))
                {
                    MaxValue = FindingMax(matrix[i, j], MaxValue);
                    if (MaxValue == matrix[i, j])
                    {
                        MaxRow = i;
                        MaxCol = j;
                    }
                    MinValue = FindingMin(matrix[i, j], MinValue);
                    if (MinValue == matrix[i, j])
                    {
                        MinRow = i;
                        MinCol = j;
                    }
                }
                spiral.Add(matrix[i,j]);
                j--;
                k++;
            }
            if (k == N * M)
               break;       
            i--;
            Left -= 2;

            j = l;
            for (n = 0; n < Up; n++)
            {
                if ((i < j) && (i + j > M - 1))
                {
                    MaxValue = FindingMax(matrix[i, j], MaxValue);
                    if (MaxValue == matrix[i, j])
                    {
                        MaxRow = i;
                        MaxCol = j;
                    }
                    MinValue = FindingMin(matrix[i, j], MinValue);
                    if (MinValue == matrix[i, j])
                    {
                        MinRow = i;
                        MinCol = j;
                    }
                }
                spiral.Add(matrix[i, j]);
                i--;
                k++;
            }
            Up -= 2;

            i = l;
            for (m = 0; m < Right; m++)
            {
                if ((i < j) && (i + j > M - 1))
                {
                    MaxValue = FindingMax(matrix[i, j+1], MaxValue);
                    if (MaxValue == matrix[i, j+1])
                    {
                        MaxRow = i;
                        MaxCol = j+1;
                    }
                    MinValue = FindingMin(matrix[i, j+1], MinValue);
                    if (MinValue == matrix[i, j+1])
                    {
                        MinRow = i;
                        MinCol = j+1;
                    }
                }
                spiral.Add(matrix[i, j+1]);
                j++;
                k++;
            }
            Right -= 2;

            i = l+1;
            j = M-1-l;
            for (n = 0; n < Down; n++)
            {
                if ((i < j) && (i + j > M - 1))
                {
                    MaxValue = FindingMax(matrix[i, j], MaxValue);
                    if (MaxValue == matrix[i, j])
                    {
                        MaxRow = i;
                        MaxCol = j;
                    }
                    MinValue = FindingMin(matrix[i, j], MinValue);
                    if (MinValue == matrix[i, j])
                    {
                        MinRow = i;
                        MinCol = j;
                    }
                }
                spiral.Add(matrix[i, j]);
                i++;
                k++;
            }
            Down -= 2;
            l++;
        }

        foreach(int elem in spiral)
        {
            Write(elem + ", ");
        }
        WriteLine();
        WriteLine("Максимальне значення = " + MaxValue + $" ({MaxRow+1},{MaxCol+1})" + 
                  " Мінімальне значення = " + MinValue + $" ({MinRow+1},{MinCol+1})");
        double summ = (MaxValue + MinValue) / 2.0;
        WriteLine("ПІВСУММА: " + summ.ToString());
        ReadKey();
    }
    static int[,] genCtrlMatrix(int N, int M)
    {
        int[,] arr = new int[N, M];
        int num = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                arr[i, j] = num;
                num++;
            }
        }
        return arr;
    }
    static int[,] genRandMatrix(int N, int M)
    {
        int[,] arr = new int [N, M];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                arr[i, j] = rnd.Next(0, 101);
            }
        }
        return arr;
    }
    static void printMatrix(int[,] arr)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
                Write(arr[i, j].ToString() + "\t");
            WriteLine();
        }
    }
    static int FindingMin(int matrixElem, int MinValue)
    {
        if (matrixElem < MinValue)
            MinValue = matrixElem;
        return MinValue;
    }
    static int FindingMax(int matrixElem, int MaxValue)
    {
        if (matrixElem > MaxValue)
            MaxValue = matrixElem;
        return MaxValue;
    }
}
    
