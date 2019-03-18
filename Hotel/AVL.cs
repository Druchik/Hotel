using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel
{
    class AVL
    {
        /// <summary>
        /// Список комнат в отеле
        /// </summary>
        public static List<Hotel_room> h_rooms = new List<Hotel_room>();

        /// <summary>
        /// Класс номер(комната) в отеле
        /// </summary>
        public class Hotel_room
        {
            public string room_number; // номер отеля
            public int rooms; // число комнат
            public int places; // число свободных мест
            public string equipment; // оборудование номера
            public bool toilet; // наличие санузла
            public Hotel_room left; // левое поддерево
            public Hotel_room right; // правое поддерево
        }
        Hotel_room root;

        /// <summary>
        /// Алгоритм поиска
        /// </summary>
        /// <param name="pat"> Слово для поиска </param>
        public void SearchString(string pat)
        {
            h_rooms.Clear();
            int m = pat.Length;
            
            int[] badChar = new int[256];

            BadCharHeuristic(pat, m, ref badChar);
            SearchString(root, m, badChar, pat);
        }

        /// <summary>
        /// Алгоритм поиска БМ
        /// </summary>
        /// <param name="current"> Комната отеля </param>
        /// <param name="m"></param>
        /// <param name="badChar"></param>
        /// <param name="pat"> Слово для поиска </param>
        private void SearchString(Hotel_room current, int m, int[] badChar, string pat)
        {
            
            if (current != null)
            {
                int n = current.equipment.Length;

                bool flag = false;
                int s = 0;
                while (s <= (n - m))
                {
                    int j = m - 1;

                    while (j >= 0 && pat[j] == current.equipment[s + j])
                        --j;

                    if (j < 0)
                    {
                        flag = true;
                        s += (s + m < n) ? m - badChar[current.equipment[s + m]] : 1;
                    }
                    else
                    {
                        s += Math.Max(1, j - badChar[current.equipment[s + j]]);
                    }
                }
                if(flag)
                    h_rooms.Add(current);
                SearchString(current.left, m, badChar, pat);
                SearchString(current.right, m, badChar, pat);
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

        /// <summary>
        /// Ввод только цифр
        /// </summary>
        /// <returns></returns>
        public int InputParseDigital()
        {
            int num;
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out num))
                    break;
                else
                    Console.WriteLine("Неверный ввод");
            }
            return num;
        }

        /// <summary>
        /// Ввод № гостиничного номера
        /// </summary>
        /// <returns></returns>
        public string InputRoomNumber()
        {
            string result = "";
            int i = 0;
            char[] ch = { 'Л', 'л', 'П', 'п', 'О', 'о', 'М', 'м'};
            do
            {
                var k = Console.ReadKey(true);
                switch (k.Key)
                {
                    case ConsoleKey.Backspace:
                        if (result.Length > 0)
                        {
                            result = result.Remove(startIndex: result.Length - 1, count: 1);
                            Console.Write(value: $"{k.KeyChar} {k.KeyChar}");
                            i--;
                        }
                        break;
                    default:
                        if (char.IsDigit(c: k.KeyChar) && i != 0)
                        {
                            Console.Write(value: k.KeyChar);
                            result += k.KeyChar;
                            i++;
                        }
                        else if (i == 0 && ch.Contains(k.KeyChar))
                        {
                            Console.Write(k.KeyChar);
                            result += k.KeyChar;
                            i++;
                        }
                        break;
                }
            } while (i != 4);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="room_number"></param>
        /// <returns></returns>
        public int Convertum(string room_number)
        {
            string rn = room_number.Remove(0, 1);
            return Convert.ToInt32(rn);
        }

        /// <summary>
        /// Прямой обход дерева
        /// </summary>
        public void PostOrderTraversal()
        {
            PostOrderTraversal(root);
        }

        /// <summary>
        /// Прямой обход дерева
        /// </summary>
        /// <param name="node"> Комната отеля </param>
        private void PostOrderTraversal(Hotel_room node)
        {
            //Console.WriteLine("Номер комнаты\t Количество комнат\t Количество мест\t Наличие санузла");
            if (node != null)
            {
                Console.WriteLine("\nГостиничный номер: {0}", node.room_number);
                Console.WriteLine("Количество комнат: {0}", node.rooms);
                Console.WriteLine("Количество мест: {0}", node.places);
                Console.WriteLine("Оборудование: {0}", node.equipment);
                Console.WriteLine("Санузел: {0}", node.toilet);
                PostOrderTraversal(node.left);
                PostOrderTraversal(node.right);
            }
        }

        /// <summary>
        /// Ввод данных
        /// </summary>
        /// <returns></returns>
        public Hotel_room InputData()
        {
            Hotel_room new_room = new Hotel_room();
            char[] ch = { 'О', 'о'};
            string toilet;
            bool flag = false;
            do
            {
                Console.WriteLine("Введите номер отеля (в формате ANNN: где A – буква (Л – люкс, П – полулюкс, О – одноместный, М – многоместный), NNN – цифры): ");
                new_room.room_number = InputRoomNumber();
                if (!IsEmpty())
                {
                    flag = ContainsNum(new_room.room_number);
                    if (flag)
                        Console.WriteLine("\nНе может быть одинаковых номеров!");
                }
            } while (flag);
            if (new_room.room_number.Contains("о") || new_room.room_number.Contains("О"))
            {
                new_room.rooms = 1;
                new_room.places = 1;
                Console.WriteLine("\nВведите наличие санузла (y/n): ");
                toilet = Program.InputLetter();
                if (toilet.Equals("y"))
                    new_room.toilet = true;
                else
                    new_room.toilet = false;
                Console.WriteLine("Введите оборудование номера: ");
                new_room.equipment = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nВведите количество комнат: ");
                new_room.rooms = InputParseDigital();
                Console.WriteLine("Введите количество мест: ");
                new_room.places = InputParseDigital();
                Console.WriteLine("Введите наличие санузла (y/n): ");
                toilet = Program.InputLetter();
                if (toilet.Equals("y"))
                    new_room.toilet = true;
                else
                    new_room.toilet = false;
                Console.WriteLine("Введите оборудование номера: ");
                new_room.equipment = Console.ReadLine();
            }
            return new_room;
        }

        /// <summary>
        /// Добавление данных в структуру
        /// </summary>
        /// <param name="newItem"> Новый номер отеля </param>
        public void Add(Hotel_room newItem/*string room_number, int rooms, int places, string equipment, bool toilet*/)
        {
            //Hotel_room newItem = new Hotel_room();
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }

        /// <summary>
        /// Рекурсивная вставка
        /// </summary>
        /// <param name="current"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private Hotel_room RecursiveInsert(Hotel_room current, Hotel_room n)
        {
            if (current == null)
            {
                current = n;
                return current;
            }
            else if (Convertum(n.room_number) < Convertum(current.room_number))
            {
                current.left = RecursiveInsert(current.left, n);
                current = Balance_Tree(current);
            }
            else if (Convertum(n.room_number) > Convertum(current.room_number))
            {
                current.right = RecursiveInsert(current.right, n);
                current = Balance_Tree(current);
            }
            return current;
        }

        /// <summary>
        /// Балансировка дерева
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private Hotel_room Balance_Tree(Hotel_room current)
        {
            int b_factor = Balance_Factor(current);
            if (b_factor > 1)
            {
                if (Balance_Factor(current.left) > 0)
                {
                    current = RotateLL(current);
                }
                else
                {
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                if (Balance_Factor(current.right) > 0)
                {
                    current = RotateRL(current);
                }
                else
                {
                    current = RotateRR(current);
                }
            }
            return current;
        }

        /// <summary>
        /// Проверка на пустоту
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (root == null)
                return true;
            return false;
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="target"></param>
        public void Delete(string target)
        {
            root = Delete(root, target);
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private Hotel_room Delete(Hotel_room current, string target)
        {
            Hotel_room parent;
            if (current == null)
            { return null; }
            else
            {
                //левое поддерево
                if (Convertum(target) < Convertum(current.room_number))
                {
                    current.left = Delete(current.left, target);
                    if (Balance_Factor(current) == -2)//here
                    {
                        if (Balance_Factor(current.right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                // правое поддерево
                else if (Convertum(target) > Convertum(current.room_number))
                {
                    current.right = Delete(current.right, target);
                    if (Balance_Factor(current) == 2)
                    {
                        if (Balance_Factor(current.left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                //если нашли
                else
                {
                    if (current.right != null)
                    {
                        parent = current.right;
                        while (parent.left != null)
                        {
                            parent = parent.left;
                        }
                        current.room_number = parent.room_number;
                        current.right = Delete(current.right, parent.room_number);
                        if (Balance_Factor(current) == 2) // балансируем
                        {
                            if (Balance_Factor(current.left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   //if current.left != null
                        return current.left;
                    }
                }
            }
            return current;
        }

        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Hotel_room Find(string str)
        {
            return Find(root, str);
        }
        private Hotel_room Find(Hotel_room current, string room_num)
        {
            if (current != null)
            {
                if (Convertum(room_num) < Convertum(current.room_number))
                {
                    if (room_num == current.room_number)
                        return current;
                    else
                        return Find(current.left, room_num);
                }
                else
                {
                    if (room_num == current.room_number)
                        return current;
                    else
                        return Find(current.right, room_num);
                }
            }
            return null;
        }

        /// <summary>
        /// Поиск введенного № комнаты в дереве
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool Contains(string str)
        {
            return Contains(root, str);
        }

        private bool Contains(Hotel_room current, string room_num)
        {
            if (current != null)
            {
                if (Convertum(room_num) < Convertum(current.room_number))
                {
                    if (room_num == current.room_number)
                        return true;
                    else
                        return Contains(current.left, room_num);
                }
                else
                {
                    if (room_num == current.room_number)
                        return true;
                    else
                        return Contains(current.right, room_num);
                }
            }
            return false;
        }

        /// <summary>
        /// Поиск введенного № комнаты с числовым значением в дереве
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool ContainsNum(string str)
        {
            return ContainsNum(root, str);
        }

        private bool ContainsNum(Hotel_room current, string room_num)
        {
            if (current != null)
            {
                if (Convertum(room_num) < Convertum(current.room_number))
                {
                    if (Convertum(current.room_number) == Convertum(room_num))
                        return true;
                    else
                        return ContainsNum(current.left, room_num);
                }
                else
                {
                    if (Convertum(current.room_number) == Convertum(room_num))
                        return true;
                    else
                        return ContainsNum(current.right, room_num);
                }
            }
            return false;
        }

        /// <summary>
        /// Уменьшение свободных мест
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool ChangeFreePlaces(string str)
        {
            return ChangeFreePlaces(root, str);
        }

        private bool ChangeFreePlaces(Hotel_room current, string room_num)
        {
            if (current != null)
            {
                if (Convertum(room_num) < Convertum(current.room_number))
                {
                    if (current.room_number == room_num)
                    {
                        if (current.places > 0)
                        {
                            current.places--;
                            return true;
                        }
                    }
                    else
                        return ChangeFreePlaces(current.left, room_num);
                }
                else
                {
                    if (current.room_number == room_num)
                    {
                        if (current.places > 0)
                        {
                            current.places--;
                            return true;
                        }
                    }
                    else
                        return ChangeFreePlaces(current.right, room_num);
                }
            }
            return false;
        }

        /// <summary>
        /// Получение максимального
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private int Max(int l, int r)
        {
            return l > r ? l : r;
        }

        /// <summary>
        /// Получение "веса"
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private int GetHeight(Hotel_room current)
        {
            int height = 0;
            if (current != null)
            {
                int l = GetHeight(current.left);
                int r = GetHeight(current.right);
                int m = Max(l, r);
                height = m + 1;
            }
            return height;
        }
        //
        private int Balance_Factor(Hotel_room current)
        {
            int l = GetHeight(current.left);
            int r = GetHeight(current.right);
            int b_factor = l - r;
            return b_factor;
        }
        private Hotel_room RotateRR(Hotel_room parent)
        {
            Hotel_room pivot = parent.right;
            parent.right = pivot.left;
            pivot.left = parent;
            return pivot;
        }
        private Hotel_room RotateLL(Hotel_room parent)
        {
            Hotel_room pivot = parent.left;
            parent.left = pivot.right;
            pivot.right = parent;
            return pivot;
        }
        private Hotel_room RotateLR(Hotel_room parent)
        {
            Hotel_room pivot = parent.left;
            parent.left = RotateRR(pivot);
            return RotateLL(parent);
        }
        private Hotel_room RotateRL(Hotel_room parent)
        {
            Hotel_room pivot = parent.right;
            parent.right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
}
