using System;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
using System.Numerics;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            OutputEncoding = System.Text.Encoding.UTF8;
            HashTable hashTable = new HashTable(5);
            try
            {
                int choice, repeat = 0;
                do
                {
                    WriteLine("               МЕНЮ");
                    WriteLine("1. Згенерувати контрольний приклад");
                    WriteLine("2. Режим адміністратора");
                    WriteLine("3. Авторизуватись");
                    try
                    {
                        choice = Convert.ToInt32(ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Clear();
                                hashTable = generateControl(hashTable);
                                hashTable.printEntries();
                                break;
                            case 2:
                                do
                                {
                                    Clear();
                                    int choiceAdmin;
                                    WriteLine("               МЕНЮ АДМІНА");
                                    WriteLine("1. Додати користувача");
                                    WriteLine("2. Видалити користувача");
                                    WriteLine("3. Переглянути всіх користувачів");
                                    try
                                    {
                                        choiceAdmin = Convert.ToInt32(ReadLine());
                                        switch (choiceAdmin)
                                        {
                                            case 1:
                                                Clear();
                                                try {
                                                    WriteLine(" Ім'я користувача:");
                                                    string firstNameAdd = ReadLine();

                                                    WriteLine(" Прізвище користувача:");
                                                    string surnameAdd = ReadLine();

                                                    WriteLine(" Пароль користувача");
                                                    string passwordAdd = ReadLine();

                                                    WriteLine(" Пошта користувача:");
                                                    string emailAdd = ReadLine();

                                                    hashTable.insertEntry(new HashTable.Key(firstNameAdd, surnameAdd),
                                                                          new HashTable.Value(passwordAdd, emailAdd, new LinkedList<HashTable.Key>()));
                                                    WriteLine("Додано успішно"); }
                                                catch { WriteLine("ПОМИЛКА"); }
                                                hashTable.printEntries();
                                                break;
                                            case 2:
                                                Clear();
                                                try
                                                {
                                                    WriteLine(" Ім'я користувача:");
                                                    string firstNameDel = ReadLine();

                                                    WriteLine(" Прізвище користувача:");
                                                    string surnameDel = ReadLine();

                                                    hashTable.removeEntry(new HashTable.Key(firstNameDel, surnameDel));
                                                    WriteLine("Видалено успішно");
                                                }
                                                catch { WriteLine("ПОМИЛКА"); }
                                                hashTable.printEntries();
                                                break;
                                            case 3:
                                                Clear();
                                                hashTable.printEntries();
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        WriteLine(" ПОМИЛКА ");
                                    }

                                    WriteLine();
                                    WriteLine("  *** Щоб повернутись в Меню Адміна натисніть 1 / в Загальне Меню - Enter ***");

                                    try
                                    {
                                        repeat = Convert.ToInt32(ReadLine());
                                    }
                                    catch
                                    {
                                        WriteLine("  ПОМИЛКА  "); repeat = 0;
                                    }
                                } while (repeat == 1);
                                break;
                            case 3:
                                
                                Clear();
                                WriteLine("               УВІЙТИ ДО АККАУНТУ");
                                WriteLine(" Ім'я користувача:");
                                string firstNameUser = ReadLine();

                                WriteLine(" Прізвище користувача:");
                                string surnameUser = ReadLine();

                                WriteLine(" Уведіть пароль:");
                                string passwordUser = hashPassword(ReadLine()).ToString();
                                HashTable.Value user;

                                user = hashTable.findEntry(new HashTable.Key(firstNameUser, surnameUser));

                                if (user == null)
                                    break;

                                if (passwordUser == user.password)
                                {
                                    do
                                    {
                                        Clear();
                                        int choiceUser;
                                        WriteLine("               КОРИСТУВАЧ " + surnameUser);
                                        WriteLine("1. Додати друга");
                                        WriteLine("2. Видалити друга");
                                        WriteLine("3. Переглянути друзів");
                                        try
                                        {
                                            choiceUser = Convert.ToInt32(ReadLine());
                                            switch(choiceUser)
                                            {
                                                case 1:
                                                    Clear();
                                                    WriteLine(" Додавання друга");
                                                    WriteLine(" Ім'я друга:");
                                                    string friendNameAdd = ReadLine();

                                                    WriteLine(" Прізвище друга:");
                                                    string friendSurAdd = ReadLine();

                                                    hashTable.addFriendToUser(new HashTable.Key(firstNameUser, surnameUser),
                                                                              new HashTable.Key(friendNameAdd, friendSurAdd));
                                                    WriteLine("Список друзів");
                                                    hashTable.printFriends(new HashTable.Key(firstNameUser, surnameUser));
                                                    break;
                                                case 2:
                                                    Clear();
                                                    WriteLine(" Видалення друга");
                                                    WriteLine(" Ім'я друга:");
                                                    string friendNameDel = ReadLine();

                                                    WriteLine(" Прізвище друга:");
                                                    string friendSurDel = ReadLine();

                                                    hashTable.removeFriendFromUser(new HashTable.Key(firstNameUser, surnameUser),
                                                                              new HashTable.Key(friendNameDel, friendSurDel));
                                                    WriteLine("Список друзів");
                                                    hashTable.printFriends(new HashTable.Key(firstNameUser, surnameUser));
                                                    break;
                                                case 3:
                                                    Clear();
                                                    WriteLine("Список друзів");
                                                    hashTable.printFriends(new HashTable.Key(firstNameUser, surnameUser));
                                                    break;
                                            }
                                        }
                                        catch
                                        {
                                            WriteLine(" ПОМИЛКА некоректне значення");
                                        }

                                        WriteLine();
                                        WriteLine("  *** Щоб повернутись в Меню Користувача натисніть 1 / в Загальне Меню - Enter ***");

                                        try
                                        {
                                            repeat = Convert.ToInt32(ReadLine());
                                        }
                                        catch
                                        {
                                            WriteLine("  ПОМИЛКА  некоректне значення "); repeat = 0;
                                        }
                                    } while (repeat == 1);
                                }
                                else
                                    WriteLine(" Неправильний пароль ");
                                break;
                        }
                    }
                    catch
                    {
                        WriteLine(" ПОМИЛКА некоректне значення ");
                    }

                    WriteLine();
                    WriteLine("  *** Щоб повернутись в Загальне Меню натисніть 1 ***");

                    try
                    {
                        repeat = Convert.ToInt32(ReadLine());
                    }
                    catch
                    {
                        WriteLine("  ПОМИЛКА  некоректне значення "); repeat = 0;
                    }

                } while (repeat == 1);
            }
            catch
            {
                WriteLine(" ПОМИЛКА  некоректне значення ");
                ReadKey();
            }
            ReadKey();
        }

        public static HashTable generateControl(HashTable table)
        {
            table.insertEntry(new HashTable.Key("Andrii", "Lavreniuk"), 
                              new HashTable.Value("qwerty", "lavre@gmail.com", new LinkedList<HashTable.Key>()));
            table.insertEntry(new HashTable.Key("Kirillo", "Saevich"),
                              new HashTable.Value("12345", "kirsaev@gmail.com", new LinkedList<HashTable.Key>()));
            table.insertEntry(new HashTable.Key("Dmitro", "Kotliar"),
                              new HashTable.Value("psswrd", "kotliardi@gmail.com", new LinkedList<HashTable.Key>()));
            table.insertEntry(new HashTable.Key("Mikita", "Koval"),
                              new HashTable.Value("abcde", "mikkov@ukr.net", new LinkedList<HashTable.Key>()));
            table.insertEntry(new HashTable.Key("Liza", "Orlianska"),
                              new HashTable.Value("qwerty12", "orliz@gmail.com", new LinkedList<HashTable.Key>()));
            table.insertEntry(new HashTable.Key("Vitalii", "Grab"),
                              new HashTable.Value("qwerty", "vitgrab@gmail.com", new LinkedList<HashTable.Key>()));

            table.addFriendToUser(new HashTable.Key("Andrii", "Lavreniuk"),
                                  new HashTable.Key("Liza", "Orlianska"));
            table.addFriendToUser(new HashTable.Key("Andrii", "Lavreniuk"),
                                  new HashTable.Key("Mikita", "Koval"));
            table.addFriendToUser(new HashTable.Key("Andrii", "Lavreniuk"),
                                  new HashTable.Key("Vitalii", "Grab"));

            table.addFriendToUser(new HashTable.Key("Kirillo", "Saevich"),
                                  new HashTable.Key("Mikita", "Koval"));
            table.addFriendToUser(new HashTable.Key("Kirillo", "Saevich"),
                                  new HashTable.Key("Andrii", "Lavreniuk"));
            table.addFriendToUser(new HashTable.Key("Kirillo", "Saevich"),
                                  new HashTable.Key("Vitalii", "Grab"));

            table.addFriendToUser(new HashTable.Key("Dmitro", "Kotliar"),
                                  new HashTable.Key("Vitalii", "Grab"));
            table.addFriendToUser(new HashTable.Key("Dmitro", "Kotliar"),
                                 new HashTable.Key("Kirillo", "Saevich"));

            table.addFriendToUser(new HashTable.Key("Mikita", "Koval"),
                                 new HashTable.Key("Kirillo", "Saevich"));
            table.addFriendToUser(new HashTable.Key("Mikita", "Koval"),
                                 new HashTable.Key("Andrii", "Lavreniuk"));
            table.addFriendToUser(new HashTable.Key("Mikita", "Koval"),
                                 new HashTable.Key("Liza", "Orlianska"));

            table.addFriendToUser(new HashTable.Key("Liza", "Orlianska"),
                                 new HashTable.Key("Kirillo", "Saevich"));
            table.addFriendToUser(new HashTable.Key("Liza", "Orlianska"),
                                 new HashTable.Key("Andrii", "Lavreniuk"));
            table.addFriendToUser(new HashTable.Key("Liza", "Orlianska"),
                                 new HashTable.Key("Vitalii", "Grab"));

            table.addFriendToUser(new HashTable.Key("Vitalii", "Grab"),
                                 new HashTable.Key("Liza", "Orlianska"));
            table.addFriendToUser(new HashTable.Key("Vitalii", "Grab"),
                                new HashTable.Key("Mikita", "Koval"));
            return table;
        }

        public static BigInteger hashPassword(string password)
        {
            BigInteger summ = 0;
            int n = password.Length;

            for (int i = 0; i < password.Length; i++)
            {
                n--;
                summ += (int)password[i] * (BigInteger)(Pow(31, n));
            }

            return summ;
        }
    }

    class HashTable
    {
        public class Entry
        {
            public Key key { get; set; }

            public Value value { get; set; }

            public Entry(Key key, Value value)
            {
                this.key = key;
                this.value = value;
            }

        }

        public class Key
        {
            public string firstName { get; set; }

            public string surname { get; set; }

            public Key(string firstName, string surname)
            {
                this.firstName = firstName;
                this.surname = surname;
            }

        }

        public class Value
        {
            public string password { get; set; }

            public string emailAddress { get; set; }

            public LinkedList<Key> friends { get; set; }

            public Value(string password, string emailAddress, LinkedList<Key> friends) 
            {
                this.password = password;
                this.emailAddress = emailAddress;
                this.friends = friends;
            }

            public void addFriend(Key key)
            {
                foreach (Key friend in friends)
                    if (friend.firstName == key.firstName && friend.surname == key.surname)
                        return;

                friends.AddLast(key);
            }

            public void removeFriend(Key key)
            {
                foreach(Key friend in friends)
                {
                    if (friend.firstName == key.firstName && friend.surname == key.surname)
                    {
                        friends.Remove(friend);
                        return;
                    }
                }
            }

        }

        Entry[] EntryTable;
        private int size = 0;
        private double loadness;
        public int Count
        {
            get { return size; }
        }

        public HashTable(int Size)
        {
            EntryTable = new Entry[Size];
            for (int i = 0; i < Size; i++)
                EntryTable[i] = null;
        }

        public void insertEntry(Key key, Value value)
        {
            size++;
            loadness = size / EntryTable.Length;

            if (loadness > 0.5)
                rehashing();

            BigInteger keyCode = hashCode(key);
            int hash = (int)(keyCode % EntryTable.Length);
            
            while (EntryTable[hash] != null)
                hash = (hash + 1) % EntryTable.Length;

            value.password = hashPassword(value.password).ToString();
            EntryTable[hash] = new Entry(key, value);
        }

        public void removeEntry(Key key)
        {
            BigInteger keyCode = hashCode(key);
            int hash = (int)(keyCode % EntryTable.Length);

            for (int i = 0; i < EntryTable.Length; i++)
            {
                if (EntryTable[hash] != null &&
                    (EntryTable[hash].key.firstName == key.firstName && EntryTable[hash].key.surname == key.surname))
                    break;
                hash = (hash + 1) % EntryTable.Length;
            }

            for (int i = 0; i < EntryTable.Length; i++)
                if (EntryTable[i] != null)
                    EntryTable[i].value.removeFriend(key);

            EntryTable[hash] = null;
            size--;
        }

        public Value findEntry(Key key)
        {
            BigInteger keyCode = hashCode(key);
            int hash = (int)(keyCode % EntryTable.Length);

            for (int i = 0; i < EntryTable.Length; i++)
            {
                if (EntryTable[hash] != null && 
                    (EntryTable[hash].key.firstName == key.firstName && EntryTable[hash].key.surname == key.surname))
                    break;
                hash = (hash + 1) % EntryTable.Length;
            }

            if (EntryTable[hash].key.firstName != key.firstName && EntryTable[hash].key.surname != key.surname)
            {
                WriteLine("Користувача не знайдено");
                return null;
            }

            return EntryTable[hash].value;
        }

        private bool IsPrimaryNum(int num)
        {
            int m = num / 2;

            for (int i = 2; i <= m; i++)
                if (num % i == 0)
                    return false;

            return true;
        }

        private int ChooseSize()
        {
            int newSize = (int)Ceiling(EntryTable.Length * 1.5);

            while (!IsPrimaryNum(newSize))
                newSize++;

            return newSize;
        }

        private void rehashing()
        {
            int newSize = ChooseSize();
            Entry[] newTable = new Entry[newSize];

            for (int i = 0; i < EntryTable.Length; i++)
            {
                if (EntryTable[i] != null)
                {
                    BigInteger keyCode = hashCode(EntryTable[i].key);
                    int hash = (int)(keyCode % newTable.Length);

                    while (newTable[hash] != null)
                        hash = (hash + 1) % newTable.Length;
                    
                    newTable[hash] = EntryTable[i];
                }
            }

            EntryTable = newTable;
        }

        private BigInteger hashCode(Key key)
        {
            BigInteger summ = 0;
            string keyValue = key.firstName.ToString() + key.surname.ToString();
            int n = keyValue.Length;

            for (int i = 0; i < keyValue.Length; i++)
            {
                n--;
                summ += (int)keyValue[i] * (BigInteger)(Pow(31, n));
            }

            return summ;
        }

        public static BigInteger hashPassword(string password)
        {
            BigInteger summ = 0;
            int n = password.Length;

            for (int i = 0; i < password.Length; i++)
            {
                n--;
                summ += (int)password[i] * (BigInteger)(Pow(31, n));
            }

            return summ;
        }

        public void addFriendToUser(Key userKey, Key friendKey)
        {
            //знаходимо людину, яку додаємо в друзі, у геш-таблиці
            Value friend = findEntry(friendKey);

            if (friend == null)
                return;

            //знаходимо індекс користувача, якому додаємо друга
            BigInteger keyCode = hashCode(userKey);
            int hash = (int)(keyCode % EntryTable.Length);

            for (int i = 0; i < EntryTable.Length; i++)
            {
                if (EntryTable[hash] != null &&
                    (EntryTable[hash].key.firstName == userKey.firstName && EntryTable[hash].key.surname == userKey.surname))
                    break;
                hash = (hash + 1) % EntryTable.Length;
            }

            if (EntryTable[hash] == null)
            {
                Console.WriteLine("Користувач відсутній");
            }
            else
            {
                EntryTable[hash].value.addFriend(friendKey);
            }
        }

        public void removeFriendFromUser(Key userKey, Key friendKey)
        {
            //знаходимо людину, яку видаляємо з друзів, у геш-таблиці
            Value friend = findEntry(friendKey);

            if (friend == null)
                return;

            //знаходимо індекс користувача, у якого видаляємо друга
            BigInteger keyCode = hashCode(userKey);
            int hash = (int)(keyCode % EntryTable.Length);

            for (int i = 0; i < EntryTable.Length; i++)
            {
                if (EntryTable[hash] != null &&
                    (EntryTable[hash].key.firstName == userKey.firstName && EntryTable[hash].key.surname == userKey.surname))
                    break;
                hash = (hash + 1) % EntryTable.Length;
            }

            if (EntryTable[hash] == null)
            {
                Console.WriteLine("Користувач відсутній");
            }
            else
            {
                EntryTable[hash].value.removeFriend(friendKey);
            }
        }

        public void printFriends(Key userKey)
        {
            //знаходимо індекс користувача, друзів якого виводимо
            BigInteger keyCode = hashCode(userKey);
            int hash = (int)(keyCode % EntryTable.Length);

            for (int i = 0; i < EntryTable.Length; i++)
            {
                if (EntryTable[hash] != null &&
                    (EntryTable[hash].key.firstName == userKey.firstName && EntryTable[hash].key.surname == userKey.surname))
                    break;
                hash = (hash + 1) % EntryTable.Length;
            }

            if (EntryTable[hash] == null)
            {
                Console.WriteLine("Користувач відсутній");
                return;
            }

            if (EntryTable[hash].value.friends.Count == 0)
            {
                Console.WriteLine("Друзі відсутні");
                return;
            }

            foreach(Key friend in EntryTable[hash].value.friends)
                Console.WriteLine(friend.firstName + " " + friend.surname);
        }

        public void printEntries()
        {
            foreach(Entry user in EntryTable)
            {
                if (user != null)
                    Console.WriteLine(user.key.firstName + " " + user.key.surname + " " + user.value.password + " " + user.value.emailAddress);
            }
        }

    }
}
