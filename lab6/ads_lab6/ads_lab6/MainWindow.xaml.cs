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

namespace ads_lab6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool flag = true;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void WriteDown_Click(object sender, RoutedEventArgs e)
        {
            TextWin.IsEnabled = true;
            StopStart.IsEnabled = true;
            CntrlGen.IsEnabled = false;
        }
        private void CntrlGen_Click(object sender, RoutedEventArgs e)
        {
            string htmlCode = "<html>" + "\n"+
                        "<head> <title> Hello </title> </head>" + "\n" + 
                        "<body> <p> This appears in the" + "\n" + 
                        "<i> browser </i> . </p> </body>" + "\n" + 
                        "</html>";
            TextWin.IsEnabled = false;
            WriteDown.IsEnabled = false;
            StopStart.IsEnabled = true;
            TextWin.Text = htmlCode;
        }
        public void CheckCode(TextBox TextWin)
        {
            if (TextWin.Text == "")
            {
                TextWin.Text += "No code was found";
                return;
            }

            StackUsingSLList Stack = new StackUsingSLList();
            string[] code = TextWin.Text.Split();

            TextWin.Text += "\n" + "\n";

            for (int i = 0; i < code.Length; i++)
            {
                if (code[i].Contains("<") && !code[i].Contains("/"))
                {
                    Stack.Push(code[i]);
                    TextWin.Text += Stack.Print();
                }
                else if (code[i].Contains("/"))
                {
                    code[i] = code[i].Remove(1, 1);

                    if (Stack.Peek(TextWin) == code[i])
                    {
                        Stack.Pop(TextWin);
                        TextWin.Text += Stack.Print();
                    }
                    else
                    {
                        TextWin.Text += "Wrong tag have been met" + "\n" + "\n" + "Code is wrong";
                        return;
                    }
                }
            }

            if (Stack.Count != 0)
            {
                TextWin.Text += "The stack is not empty!" + "\n" + "\n" + "Code is wrong";
            }
            else
                TextWin.Text += "Code is right";
        }
        private void StopStart_Click(object sender, RoutedEventArgs e)
        {
            if (flag)
            {
                CntrlGen.IsEnabled = false;
                WriteDown.IsEnabled = false;
                TextWin.IsEnabled = true;
                CheckCode(TextWin);
                flag = false;
            }
            else
            {
                TextWin.Text = "";
                TextWin.IsEnabled = false;
                CntrlGen.IsEnabled = true;
                WriteDown.IsEnabled = true;
                flag = true;
            }
        }
    }

    class StackUsingSLList
    {
        private Node top;

        public class Node
        {
            public string data;
            public Node link;

            public Node(string data)
            {
                this.data = data;
            }

            public Node(string data, Node next)
            {
                this.data = data;
                this.link = next;
            }
        }

        private int count = 0;
        public int Count
        {
            get { return count; }
        }

        public StackUsingSLList(string data)
        {
            top = new Node(data);
        }

        public StackUsingSLList()
        {
            top = null;
        }

        public void Push(string data)
        {
            Node current = new Node(data);
            count++;
            current.link = top;
            top = current;
        }

        public void Pop(TextBox Box)
        {
            if (top == null)
            {
                Box.Text = "The stack is empty!";
            }
            else
            {
                Node deletedNode = top;
                count--;
                top = deletedNode.link;
                deletedNode = null;
            }
        }

        public string Peek(TextBox Box)
        {
            if (top != null)
            {
                return top.data;
            }
            else
            {
                Box.Text = "The stack is empty!";
                return null;
            }
        }

        public string Print()
        {
            string text = "";
            if (top == null)
            {
                text = "The stack is empty!" + "\n" + "\n";
                return text;
            }
            else
            {
                Node current = top;
                while (current != null)
                {
                    text += "⎯⎯⎯⎯⎯⎯⎯⎯⎯";
                    current = current.link;
                }
                text += "\n";

                current = top;
                while (current != null)
                {
                    text += $"|{current.data, 10}";
                    current = current.link;
                }

                text += "|" + "\n";
                current = top;
                while (current != null)
                {
                    text += "⎯⎯⎯⎯⎯⎯⎯⎯⎯";
                    current = current.link;
                }

                text += "\n" + "\n";
                return text;
            }
        }
    }
}
