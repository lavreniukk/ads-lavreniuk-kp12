using System;
using System.Collections.Generic;
using static System.Console;

namespace ads_lab4_task
{
    class SLList
    {
        public Node head;
        public Node tail;

        public class Node
        {
            public int data;
            public Node next;

            public Node(int data)
            {
                this.data = data;
            }

            public Node(int data, Node next)
            {
                this.data = data;
                this.next = next;
            }
        }
        public SLList(int data)
        {
            head = new Node(data);
            tail = head;
            head.next = tail;
        }
        public SLList()
        {
            head = null;
            tail = null;
        }
        public void AddFirst(int data)
        {
            if (head != null)
            {
                Node current = new Node(data);
                current.next = head;
                head = current;
            }
            else
            {
                Node current = new Node(data);
                current.next = head;
                head = current;
                tail = head;
            }
        }
        public void AddToPosition(int data, int position, int count)
        {
            if (head == null && position == 1)
            {
                head = new Node(data);
                AddFirst(data);
            }
            else if (position > count)
                AddLast(data);
            else
            {
                Node current = head;
                for (int i = 2; i < position; i++)
                    current = current.next;
                Node addedNode = new Node(data);
                addedNode.next = current.next;
                current.next = addedNode;
            }
        }
        public void AddLast(int data)
        {
            if (head == null)
            {
                WriteLine("Список пустий: додано першим");
                AddFirst(data);
            }
            else
            {
                Node current = new Node(data);
                current.next = null;
                tail.next = current;
                tail = current;
            }
        }
        public void DeleteFirst()
        {
            if (head == null)
                WriteLine("Список пустий");
            else
            {
                Node deletedNode = head;
                head = deletedNode.next;
                deletedNode = null;
            }
        }
        public void DeleteAtPosition(int position, int count)
        {
            if (head == null)
                WriteLine("Список пустий");
            else if (position > count)
                DeleteLast();
            else
            {
                Node current = head;
                for (int i = 2; i < position; i++)
                    current = current.next;
                current.next = current.next.next;
                current = null;
            }
        }
        public int CountList(SLList list)
        {
            if (head == null)
            {
                WriteLine("Список пустий");
                return 0;
            }
            else
            {
                int count = 0;
                Node current = head;
                while (current.next != tail.next)
                {
                    current = current.next;
                    count++;
                }
                count++;
                return count;
            }
        }
        public void DeleteLast()
        {
            Node current = head;
            while (current.next.next != null)
            {
                current = current.next;
            }
            current.next = null;
            tail = current;
        }
        public void Print()
        {
            if (head == null)
                WriteLine("Список пустий");
            else
            {
                Node current = head;
                while (current != tail)
                {
                    Write(current.data + ", ");
                    current = current.next;
                }
                WriteLine(current.data + ".");
                ReadKey();
            }
        }
        public SLList RandomGen(int quant)
        {
            SLList list = new SLList();
            Random rnd = new Random();
            if (quant <= 0)
                return list;
            head = new Node(rnd.Next(5,100));
            tail = head;
            for(int i = 1; i < quant; i++)
                AddLast(rnd.Next(5,100));
            return list;
        }
        public void TaskFunction(SLList list, int data)
        {
            int count = CountList(list);
            if (count == 0)
            {
                WriteLine("Список пустий");
            }
            else if (count % 2 == 0)
            {
                AddToPosition(data, 2, count);
            }
            else
            {
                AddToPosition(data, count / 2 + 1, count);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = System.Text.Encoding.UTF8;
            SLList list = new SLList();
            Write("Оберіть розмір списку: ");

            try
            {
                int size = Convert.ToInt32(ReadLine());
                list.RandomGen(size);
                WriteLine("Сгенерований список: ");
                list.Print();
                int choice;
                int repeat = 0;
                int data, pos;
                do
                {
                    WriteLine("               МЕНЮ");
                    WriteLine("1. Додавання нового вузла у голову списку \n" +
                              "2. Додавання нового вузла у хвіст списку \n" +
                              "3. Додавання нового вузла на визначену позицію \n" +
                              "4. Видалення голови списку \n" +
                              "5. Видалення хвоста списку \n" +
                              "6. Видалення вузла з визначеної позиції \n" +
                              "7. Додати новий вузол перед середнім вузлом, якщо у списку непарна кількість вузлів, інакше – після голови списку");
                    try
                    {
                        choice = Convert.ToInt32(ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Clear();
                                Write("Введіть значення вузла: "); data = Convert.ToInt32(ReadLine());
                                list.AddFirst(data);
                                list.Print();
                                break;
                            case 2:
                                Clear();
                                Write("Введіть значення вузла: "); data = Convert.ToInt32(ReadLine());
                                list.AddLast(data);
                                list.Print();
                                break;
                            case 3:
                                Clear();
                                Write("Введіть значення вузла: "); data = Convert.ToInt32(ReadLine());
                                Write("Введіть позиція вузла: "); pos = Convert.ToInt32(ReadLine());
                                list.AddToPosition(data, pos, list.CountList(list));
                                list.Print();
                                break;
                            case 4:
                                Clear();
                                list.DeleteFirst();
                                list.Print();
                                break;
                            case 5:
                                Clear();
                                list.DeleteLast();
                                list.Print();
                                break;
                            case 6:
                                Clear();
                                Write("Введіть позиція вузла: "); pos = Convert.ToInt32(ReadLine());
                                list.DeleteAtPosition(pos, list.CountList(list));
                                list.Print();
                                break;
                            case 7:
                                Clear();
                                Write("Введіть значення вузла: "); data = Convert.ToInt32(ReadLine());
                                list.TaskFunction(list, data);
                                list.Print();
                                break;
                            default:
                                Clear();
                                WriteLine(" Помилка ");
                                break;
                        }
                    }
                    catch { WriteLine(" Помилка "); }
                    WriteLine();
                    WriteLine("  *** Щоб повернутись в Меню натисніть 1 ***");
                    try
                    {
                        repeat = Convert.ToInt32(ReadLine());
                    }
                    catch { WriteLine("  ПОМИЛКА  "); repeat = 0; }
                } while (repeat == 1);
            }
            catch
            {
                WriteLine(" Помилка: некоректне значення ");
                ReadKey();
            }
        }
    }   
}