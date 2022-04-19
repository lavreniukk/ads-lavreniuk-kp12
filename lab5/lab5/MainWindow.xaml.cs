using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ads_lab5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random rnd = new Random();
        static List<int> WriteDownArray = new List<int>();
        static int countForWritingDown = 0;
        static string line = "";
        static int M = 5;
        static int N = 5;
        static bool Restart = false;
        public MainWindow() 
        {
            InitializeComponent();
        }
        static int[,] genRandMatrix(int[,] matrix) //function to generate example with random numbers
        {
            List<int> usedNums = new List<int>();

            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                {
                    int num = rnd.Next(-matrix.Length, matrix.Length);
                    while (usedNums.Contains(num))
                        num = rnd.Next(-matrix.Length, matrix.Length);
                    usedNums.Add(num);
                    matrix[i, j] = num;
                }

            return matrix;
        }
        static int[,] genCtrlMatrix(int[,] arr)    //function to generate control example
        {
            int k = -arr.Length/2;
            for (int i = 0; i < M; i++)
                for (int j = 0; j < N; j++)
                {
                    arr[i, j] = k;
                    k++;
                }
            return arr;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void Msize_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { M = Convert.ToInt32(Msize.Text); }
            catch { Msize.Text = ""; }
            if (Msize != null && Nsize != null)
                if (Msize.Text != "" && Nsize.Text != "")
                {
                    RandomGen.IsEnabled = true;
                    CntrlGen.IsEnabled = true;
                    WriteDown.IsEnabled = true;
                }
                else
                {
                    RandomGen.IsEnabled = false;
                    CntrlGen.IsEnabled = false;
                    WriteDown.IsEnabled = false;
                }
        }
        private void Nsize_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { N = Convert.ToInt32(Nsize.Text); }
            catch { Nsize.Text = "";  }
            if (Msize != null && Nsize != null)
                if (Msize.Text != "" && Nsize.Text != "")
                {
                    RandomGen.IsEnabled = true;
                    CntrlGen.IsEnabled = true;
                    WriteDown.IsEnabled = true;
                }
                else
                {
                    RandomGen.IsEnabled = false;
                    CntrlGen.IsEnabled = false;
                    WriteDown.IsEnabled = false;
                }
        }
        private void WriteDown_Click(object sender, RoutedEventArgs e)
        {
            WriteDown.IsEnabled = false;
            RandomGen.IsEnabled = false;
            CntrlGen.IsEnabled = false;
            Msize.IsEnabled = false;
            Nsize.IsEnabled = false;
            StopStart.IsEnabled = true;
            AddElem.IsEnabled = false;

            int[,] arr = new int[M, N];
            int k = 0;
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    arr[i, j] = WriteDownArray[k];
                    k++;
                }
            }
            line = printMatrix(arr);

            TextWin.Text = line;
            line = SumUpFunction(arr);
        }
        private void AddElem_Click(object sender, RoutedEventArgs e)
        {
            WriteDown.IsEnabled = false;
            RandomGen.IsEnabled = false;
            CntrlGen.IsEnabled = false;
            Msize.IsEnabled = false;
            Nsize.IsEnabled = false;

            if (countForWritingDown + 1 == M * N)
            {
                WriteDown.IsEnabled = true;
                AddElem.IsEnabled = false;
            }

            try { WriteDownArray.Add(Convert.ToInt32(ElemOfMatrix.Text)); countForWritingDown++; }
            catch { ElemOfMatrix.Text = ""; }
        }
        private void RandomGen_Click(object sender, RoutedEventArgs e)
        {
            WriteDown.IsEnabled = false;
            RandomGen.IsEnabled = false;
            CntrlGen.IsEnabled = false;
            Msize.IsEnabled = false;
            Nsize.IsEnabled = false;
            StopStart.IsEnabled = true;
            AddElem.IsEnabled = false;

            int[,] arr = new int[M, N];
            arr = genRandMatrix(arr);
            line = printMatrix(arr);

            TextWin.Text = line;
            line = SumUpFunction(arr);
        }
        static string SumUpFunction(int[,] arr)
        {
            line = "\n";

            (int[] AscendingOrder, int[] DescendingOrder) = BreakMatrix(arr);

            Recursive_QuickSort(AscendingOrder, 0, AscendingOrder.Length - 1, true);
            Recursive_QuickSort(DescendingOrder, 0, DescendingOrder.Length - 1, false);

            line += " Sorting in ascending order: ";
            foreach (int elem in AscendingOrder)
                line += $"{elem, 5}" + " ";
            line += "\n" + " Sorting in descending order: ";
            foreach (int elem in DescendingOrder)
                line += $"{elem,5}" + " ";
            line += "\n" + "\n";

            arr = CombineMatrix(AscendingOrder, DescendingOrder);
            line += printMatrix(arr);
            return line;
        }
        static string printMatrix(int[,] arr)
        {
            string matrix= "";

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (i % 2 == 1 && j % 2 == 0)
                        matrix += "[" + $"{arr[i, j],5}" + "] ";
                    else
                        matrix += $"{arr[i, j],7}" + " ";
                }
                matrix += "\n";
            }

            return matrix;
        }
        static void InsertSort(int[] arr, bool flag)
        {
            int key;
            if (flag)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                        key = arr[i];
                        int j = i - 1;
                        while (j >= 0 && arr[j] > key)
                        {
                            arr[j + 1] = arr[j];
                            j--;
                        }
                        arr[j + 1] = key;
                    }
                }
            else
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    key = arr[i];
                    int j = i - 1;
                    while (j >= 0 && arr[j] < key)
                    {
                        arr[j + 1] = arr[j];
                        j--;
                    }
                    arr[j + 1] = key;
                }
            }
        }
        static int[,] CombineMatrix(int[] arrA, int[] arrD)
        {
            int[,] matrixSorted = new int[M, N];
            int k = 0;
            int l = 0;

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i % 2 == 1 && j % 2 == 0)
                    {
                        matrixSorted[i, j] = arrA[k];
                        k++;
                    }
                    else
                    {
                        matrixSorted[i, j] = arrD[l];
                        l++;
                    }
                }
            }

            return matrixSorted;
        }
        private void CntrlGen_Click(object sender, RoutedEventArgs e)
        {
            WriteDown.IsEnabled = false;
            RandomGen.IsEnabled = false;
            CntrlGen.IsEnabled = false;
            Msize.IsEnabled = false;
            Nsize.IsEnabled = false;
            AddElem.IsEnabled = false;
            StopStart.IsEnabled = true;

            int[,] arr = new int[M, N];
            arr = genCtrlMatrix(arr);
            line = printMatrix(arr);

            TextWin.Text = line;
            line = SumUpFunction(arr);
        }
        //Quick Sort (Hoare Partition)
        static void Recursive_QuickSort(int[] arr, int low, int high, bool flag)
        {
            if ( high - low < M)
            {
                InsertSort(arr, flag);
            }
            else if (low < high)
            {
                int PartIndex = HoarePartition(arr, low, high, flag);

                Recursive_QuickSort(arr, low, PartIndex - 1, flag);
                Recursive_QuickSort(arr, PartIndex + 1, high, flag);
            }
        }
        static int HoarePartition(int[] arr, int low, int high, bool flag)
        {
            int pivot = arr[low];
            int i = low + 1;
            int j = high;

            if (flag)
            {
                while (true)
                {
                    while (i < high && arr[i] < pivot)
                        i++;

                    while (arr[j] > pivot)
                        j--;

                    if (i >= j)
                        break;

                    Swap(arr, i++, j--);
                }
                Swap(arr, low, j);
            }
            else
            {
                while (true)
                {
                    while (i < high && arr[i] > pivot)
                        i++;

                    while (arr[j] < pivot)
                        j--;

                    if (i >= j)
                        break;

                    Swap(arr, i++, j--);
                }
                Swap(arr, low, j);
            }

            return j;
        }
        static (int[], int[]) BreakMatrix(int[,] arr)
        {
            double a = arr.GetLength(0);
            a = Math.Floor(a / 2);
            double b = arr.GetLength(1);
            b = Math.Ceiling(b / 2);

            int[] AscendingOrder = new int[(int)(a * b)];
            int[] DescendingOrder = new int[arr.Length - AscendingOrder.Length];
            int k = 0;
            int l = 0;

            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (i % 2 == 1 && j % 2 == 0)
                    {
                        AscendingOrder[k] = arr[i, j];
                        k++;
                    }
                    else
                    {
                        DescendingOrder[l] = arr[i, j];
                        l++;
                    }
                }

            return (AscendingOrder, DescendingOrder);
        }
        public static void Swap(int[] arr, int i, int j)
        {
            int buff = arr[i];
            arr[i] = arr[j];
            arr[j] = buff;
        }
        private void StopStart_Click(object sender, RoutedEventArgs e)
        {
            if (!Restart)
            {
                TextWin.Text += line;
                Restart = true;
            }
            else
            {
                TextWin.Text = "";
                line = "";
                WriteDownArray.Clear();
                countForWritingDown = 0;
                WriteDown.IsEnabled = true;
                RandomGen.IsEnabled = true;
                CntrlGen.IsEnabled = true;
                Msize.IsEnabled = true;
                Nsize.IsEnabled = true;
                StopStart.IsEnabled = false;
                AddElem.IsEnabled = true;
                Restart = false;
            }
        }
    }
}
