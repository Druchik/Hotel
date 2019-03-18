using System;

namespace Hotel
{
    /// <summary>
    /// Хэш-таблица
    /// </summary>
    public class Hashtable
    {
        /// <summary>
        /// Массив постояльцев
        /// </summary>
        Guest[] table;

        /// <summary>
        /// Размар массива
        /// </summary>
        const int size = 10;

        /// <summary>
        /// Таблица пуста?
        /// </summary>
        bool isEmpty = true;
        int count = 0;

        public bool IsEmpty { get { return isEmpty; } }

        /// <summary>
        /// Алгоритм поиска
        /// </summary>
        /// <param name="pat"></param>
        public void SearchString(string pat)
        {
            Program.guests.Clear();
            Guest current = Program.table[0];
            int count = 0;
            int m = pat.Length;

            int[] badChar = new int[256];

            BadCharHeuristic(pat, m, ref badChar);
            SearchString(current, m, badChar, pat, count);
        }

        // алгоритм поиска БМ
        private void SearchString(Guest current, int m, int[] badChar, string pat, int count)
        {

            if (current != null)
            {
                int n = current.FIO.Length;

                bool flag = false;
                int s = 0;
                while (s <= (n - m))
                {
                    int j = m - 1;

                    while (j >= 0 && pat[j] == current.FIO[s + j])
                        --j;

                    if (j < 0)
                    {
                        flag = true;
                        s += (s + m < n) ? m - badChar[current.FIO[s + m]] : 1;
                    }
                    else
                    {
                        s += Math.Max(1, j - badChar[current.FIO[s + j]]);
                    }
                }
                if (flag)
                    Program.guests.Add(current);
                if (current.GetNextNode() != null)
                {
                    current = current.GetNextNode();
                    SearchString(current, m, badChar, pat, count);
                }
            }
            else
                while (count < size - 1)
                {
                    count++;
                    current = table[count];
                    if (current != null)
                        SearchString(current, m, badChar, pat, count);
                } 
            
        }

        private void BadCharHeuristic(string str, int size, ref int[] badChar)
        {
            int i;

            for (i = 0; i < 256; i++)
                badChar[i] = -1;

            for (i = 0; i < size; i++)
                badChar[(int)str[i]] = i;
        }
        // Вставка в хештаблицу
        public void Insert(Guest guest)
        {
            table = Program.table;
            //Guest newGuest = new Guest();
            long hash = guest.GetKey() % Convert.ToInt64(size);

            while (table[hash] != null && table[hash].GetKey() % size != guest.GetKey() % size)
            {
                hash = (hash + 1) % size;
            }
            if (table[hash] != null && hash == table[hash].GetKey() % size)
            {
                guest.SetNextNode(table[hash].GetNextNode());
                table[hash].SetNextNode(guest);
                isEmpty = false;
                count++;
                return;
            }
            else
            {
                table[hash] = guest;
                isEmpty = false;
                count++;
                return;
            }
        }

        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(long key)
        {
            table = Program.table;

            long hash = key % size;
            if (isEmpty)
                return false;
            else
            {
                while (table[hash] != null && table[hash].GetKey() % size != key % size)
                {
                    hash = (hash + 1) % size;
                }
                Guest current = table[hash];
                Guest previous = null;

                if (current != null)
                {
                    while (current.GetKey() != key && current.GetNextNode() != null)
                    {
                        previous = current;
                        current = current.GetNextNode();
                    }
                    if (current.GetKey() == key)
                    {
                        if (previous != null)
                        {
                            previous.next = current.next;
                            if (current.next == null)
                                previous.next = null;
                        }
                        else
                        {
                            table[hash] = null;
                        }
                        count--;
                        if (count == 0)
                            isEmpty = true;
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Поиск в хэш-таблице
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Find(long key)
        {
            table = Program.table;

            long hash = key % size;
            if (!isEmpty)
            {
                while (table[hash] != null && table[hash].GetKey() % size != key % size)
                {
                    hash = (hash + 1) % size;
                }
                Guest current = table[hash];
                if (current != null)
                {
                    while (current.GetKey() != key && current.GetNextNode() != null)
                    {
                        current = current.GetNextNode();
                    }
                    if (current.GetKey() == key)
                    {
                        return true;
                    }
                    /*else
                    {
                        return false;
                    }*/
                }
            }
            return false;
        }

        /// <summary>
        /// Поиск в хэш-таблице
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Guest FindGuest(long key)
        {
            table = Program.table;

            long hash = key % size;

                while (table[hash] != null && table[hash].GetKey() % size != key % size)
                {
                    hash = (hash + 1) % size;
                }
                Guest current = table[hash];
                if (current == null)
                    return null;
                else
                {
                    while (current.GetKey() != key && current.GetNextNode() != null)
                    {
                        current = current.GetNextNode();
                    }
                    if (current.GetKey() == key)
                    {
                        return current;
                    }
                    else
                    {
                        return null;
                    }
                }
        }

        /// <summary>
        /// Вывод всех элементов в консоль
        /// </summary>
        public void Print()
        {
            Guest current = null;
            Console.WriteLine("Зарегистрированные постояльцы: ");
            for (int i = 0; i < size; i++)
            {
                current = table[i];
                while (current != null)
                {
                    Console.WriteLine("\nПаспорт: {0}", current.passport_number);
                    Console.WriteLine("ФИО: {0}", current.FIO);
                    Console.WriteLine("Год рождения: {0}", current.birth_year);
                    Console.WriteLine("Адрес: {0}", current.adress);
                    Console.WriteLine("Цель прибытия: {0}", current.purpose_of_visit);
                    current = current.GetNextNode();
                }
            }
        }
    }
}
