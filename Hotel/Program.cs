using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    class Program
    {
        public static Guest[] table;
        static public List<Guest> guests = new List<Guest>();
        public static List<string> rooms = new List<string>();

        public static long Convert(string num)
        {
            num = num.Remove(4, 1);
            return long.Parse(num);
        }

        public static string InputLetter()
        {
            string result = "";
            int i = 0;
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
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        return result;
                    default:
                        if (char.IsLetter(c: k.KeyChar) && i == 0 && (k.KeyChar == 'y' || k.KeyChar == 'n'))
                        {
                            Console.Write(value: k.KeyChar);
                            result += k.KeyChar;
                            i++;
                        }
                        break;
                }
            } while (i != 2);
            return result;
        }
    
        static void Main(string[] args)
        {
            AVL avl = new AVL();
            Hashtable hashtable = new Hashtable();
            CircularDoublyLinkedList list = new CircularDoublyLinkedList();
            const int size = 10;
            table = new Guest[size];
            for (int i = 0; i < size; i++)
                table[i] = null;

            int n = -1;
            while (n != 0)
            {
                Console.WriteLine("\n***Регистрация постояльцев в гостинице***");
                Console.WriteLine("\n     ************ Меню ************\n");
                Console.WriteLine("1 - Добавить гостиничный номер");
                Console.WriteLine("2 - Вывести список всех номеров");
                Console.WriteLine("3 - Зарегистрировать постояльца в гостинице");
                Console.WriteLine("4 - Вывести всех постояльцев");
                Console.WriteLine("5 - Удалить гостиничный номер");
                Console.WriteLine("6 - Удалить запись о постояльце");
                Console.WriteLine("7 - Поиск постояльца по № паспорта");
                Console.WriteLine("8 - Поиск постояльца по ФИО");
                Console.WriteLine("9 - Поиск гостиничного номера по №");
                Console.WriteLine("10 - Поиск гостиничного номера по наличию оборудования");
                Console.WriteLine("11 - Регистрация вселения постояльца");
                Console.WriteLine("12 - Регистрация выселения постояльца");
                Console.WriteLine("0 - Выход");
                Console.WriteLine("Выберите нужный пункт меню: ");
                
                while (true)
                {
                    if (Int32.TryParse(Console.ReadLine(), out n))
                        break;
                    else
                        Console.WriteLine("Неверный ввод");
                }

                switch (n)
                {
                    case 1: //Добавление гостиничного номера
                        {
                            avl.Add(avl.InputData());
                            break;
                        }
                    case 2: //Вывод списка номеров
                        {
                            if(avl.IsEmpty())
                                Console.WriteLine("Список номеров пуст!");
                            else
                            {
                                Console.WriteLine("Список номеров: ");
                                avl.PostOrderTraversal();
                            }
                            Console.ReadLine();
                            break;
                        }
                    case 3: //Регистрация постояльца в гостинице
                        {
                            Guest newGuest = new Guest();
                            hashtable.Insert(newGuest.Input(newGuest, hashtable));
                            break;
                        }
                    case 4: // Вывод списка постояльцев
                        {
                            if (hashtable.IsEmpty)
                                Console.WriteLine("База пуста. Заполните ее!");
                            else
                                hashtable.Print();
                            Console.ReadLine();
                            break;
                        }
                    case 5: // Удаление гостиничного номера
                        {
                            if (avl.IsEmpty())
                                Console.WriteLine("Список номеров пуст!");
                            else
                            {
                                Console.WriteLine("Введите гостиничный номер для удаления (в формате ANNN: где A – буква (Л – люкс, П – полулюкс, О – одноместный, М – многоместный), NNN – цифры): ");
                                string word = avl.InputRoomNumber();
                                if (list.ContainsRoom(word))
                                    Console.WriteLine("\nНельзя удалять гостиничный номер с вселенными постояльцами!");
                                else
                                    avl.Delete(word);
                            }
                            Console.ReadLine();
                            break;
                        }
                    case 6: // Удаление записи о постояльце
                        {
                            Guest current = new Guest();
                            Registration reg = new Registration();
                            if (hashtable.IsEmpty)
                                Console.WriteLine("База пуста. Заполните ее!");
                            else
                            {
                                Console.WriteLine("Введите № паспорта постояльца для удаления: ");
                                current.passport_number = current.InputPassportNumber();
                                if (!list.IsEmpty)
                                {
                                    reg = list.FindPassport(current.passport_number);
                                    if (reg != null && reg.chekOut == null)
                                        Console.WriteLine("Нельзя удалять вселенных постояльцев!");
                                }
                                else
                                {
                                    bool isPresent = hashtable.Remove(current.GetKey());
                                    Console.WriteLine(isPresent == true ? "\nЗапись успешно удалена!" : "\nПостоялец с таким номером паспорта не зарегистрирован в гостинице!");
                                }
                                
                            }
                            Console.ReadLine();
                            break;
                        }
                    case 7: // Поиск постояльца по номеру паспорта
                        {
                            Guest current = new Guest();
                            if (hashtable.IsEmpty)
                                Console.WriteLine("База пуста. Заполните ее!");
                            else
                            {
                                Console.WriteLine("Введите № паспорта постояльца для поиска: ");
                                current.passport_number = current.InputPassportNumber();
                                current = hashtable.FindGuest(current.GetKey());
                                if (current == null)
                                    Console.WriteLine("Постоялец с таким номером паспорта не зарегистрирован в гостинице!");
                                else
                                {
                                    if (list.ContainsResident(current.passport_number))
                                    {
                                        Console.WriteLine("\nНайден паспорт: {0}", current.passport_number);
                                        Console.WriteLine("ФИО постояльца: {0}", current.FIO);
                                        Console.WriteLine("Год рождения: {0}", current.birth_year);
                                        Console.WriteLine("Адрес проживания: {0}", current.adress);
                                        Console.WriteLine("Цель визита: {0}", current.purpose_of_visit);
                                        Console.WriteLine("Постоялец живет в номере {0}.", list.FindPassport(current.passport_number).room_num);
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nНайден паспорт: {0}", current.passport_number);
                                        Console.WriteLine("ФИО постояльца: {0}", current.FIO);
                                        Console.WriteLine("Год рождения: {0}", current.birth_year);
                                        Console.WriteLine("Адрес проживания: {0}", current.adress);
                                        Console.WriteLine("Цель визита: {0}", current.purpose_of_visit);
                                        Console.WriteLine("Постоялец не проживает в гостинице!");
                                    }
                                }
                            }
                            Console.ReadLine();
                            break;
                        }
                    case 8: // Поиск постояльца по ФИО
                        {
                            string FIO;
                            if (hashtable.IsEmpty)
                                Console.WriteLine("База пуста. Заполните ее!");
                            else
                            {
                                Console.WriteLine("Введите ФИО для поиска: ");
                                FIO = Console.ReadLine();
                                hashtable.SearchString(FIO);
                            
                                Console.WriteLine("\nНайденные постояльцы: ");
                                foreach (var item in guests)
                                {
                                    Console.Write("\nФИО постояльца: {0}", item.FIO);
                                    Console.Write("\nПаспорт постояльца: {0}\n", item.passport_number);
                                }
                            }
                            Console.ReadLine();
                            break;
                        }
                    case 9: // Поиск гостиничного номера по его №
                        {
                            AVL.Hotel_room hm = new AVL.Hotel_room();
                            guests.Clear();
                            if (avl.IsEmpty())
                                Console.WriteLine("Список номеров пуст!");
                            else
                            {
                                Console.WriteLine("Введите гостиничный номер для поиска: ");
                                string str = avl.InputRoomNumber();
                                hm = avl.Find(str);
                                if (hm == null)
                                    Console.WriteLine("\nИскомый номер не обнаружен!");
                                else
                                {
                                    Console.WriteLine("\nНомер найден:");
                                    Console.WriteLine("\nГостиничный номер: {0}", hm.room_number);
                                    Console.WriteLine("Количество комнат: {0}", hm.rooms);
                                    Console.WriteLine("Количество мест: {0}", hm.places);
                                    Console.WriteLine("Оборудование: {0}", hm.equipment);
                                    Console.WriteLine("Санузел: {0}", hm.toilet);

                                    if (list.IsEmpty)
                                        Console.WriteLine("В номере никто не проживает.");
                                    else
                                    {
                                        list.FindRooms(hm.room_number);
                                        if (rooms.Count > 0)
                                        {
                                            foreach (var item in rooms)
                                                guests.Add(hashtable.FindGuest(Convert(item)));
                                            Console.WriteLine("\nВ номере проживают: ");
                                            foreach (var item in guests)
                                            {
                                                Console.Write("ФИО постояльца: {0}", item.FIO);
                                                Console.Write("\nПаспорт постояльца: {0}\n", item.passport_number);
                                            }
                                        }
                                    }
                                }
                            }
                            Console.ReadLine();
                            break;
                        }
                    case 10: // Поиск гостиничных номеров по оборудованию
                        {
                            if (avl.IsEmpty())
                                Console.WriteLine("Список номеров пуст!");
                            else
                            {
                                Console.WriteLine("Введите оборудование для поиска: ");
                                avl.SearchString(Console.ReadLine());
                                if (AVL.h_rooms.Count != 0)
                                {
                                    Console.WriteLine("\nНайденные номера: ");
                                    foreach (var item in AVL.h_rooms)
                                    {
                                        Console.WriteLine("\nГостиничный номер: {0}", item.room_number);
                                        Console.WriteLine("Количество комнат: {0}", item.rooms);
                                        Console.WriteLine("Количество мест: {0}", item.places);
                                        Console.WriteLine("Оборудование: {0}", item.equipment);
                                        Console.WriteLine("Санузел: {0}", item.toilet);
                                    }
                                }
                                else
                                    Console.WriteLine("\nИскомое оборудование не встречается в номерах!");
                            }
                            Console.ReadLine();
                            break;
                        }
                    case 11: // Регистрация вселения
                        {
                            Registration reg = new Registration();
                            Guest newGuest = new Guest();
                            string date;
                            DateTime dateChekIn = new DateTime();
                            dateChekIn = DateTime.Now;
                            Console.WriteLine("Введите № паспорта постояльца: ");
                            newGuest.passport_number = newGuest.InputPassportNumber();
                            bool isPresent = hashtable.Find(newGuest.GetKey());
                            bool flag = false; 
                            bool freeBeds = false;
                            if (!isPresent)
                                Console.WriteLine("\nПостоялец с таким номером паспорта не зарегистрирован в гостинице! Необходимо пройти регистрацию!");
                            else if (list.ContainsResident(newGuest.passport_number) && !list.IsEmpty)
                                Console.WriteLine("\nПостоялец уже живет в гостинице!");
                            else
                            {
                                reg.pass_num = newGuest.passport_number;
                                Console.WriteLine("Введите гостиничный номер для регистрации(в формате ANNN: где A – буква (Л – люкс, П – полулюкс, О – одноместный, М – многоместный), NNN – цифры): ");
                                reg.room_num = reg.InputRoomNumber();
                                flag = avl.Contains(reg.room_num);
                                if (avl.IsEmpty())
                                    Console.WriteLine("\nСписок номеров пуст! Сначала заполните его.");
                                else if (flag)
                                {
                                    if (avl.Find(reg.room_num).places > 0)
                                    {
                                        freeBeds = avl.ChangeFreePlaces(reg.room_num);
                                        Console.WriteLine("\nВселить постояльца? (y/n)");
                                        date = InputLetter();
                                        if (date.Equals("y"))
                                        {
                                            reg.chekIn = dateChekIn.ToString();
                                            Console.WriteLine("Дата: " + reg.chekIn);
                                            Console.WriteLine("Успешно!");
                                            list.Add(reg);
                                        }
                                        else
                                            Console.WriteLine("Отмена.");
                                    }
                                    else
                                        Console.WriteLine("\nВ номере нет свободных мест!");
                                }
                                else
                                    Console.WriteLine("\nНет такого номера!");
                            }
                            /*Console.WriteLine("Введите № паспорта постояльца: ");
                            newGuest.passport_number = newGuest.InputPassportNumber();
                            reg.pass_num = newGuest.passport_number;
                            Console.WriteLine("Введите гостиничный номер для регистрации(в формате ANNN: где A – буква (Л – люкс, П – полулюкс, О – одноместный, М – многоместный), NNN – цифры): ");
                            reg.room_num = reg.InputRoomNumber();
                            list.Add(reg);*/
                            Console.ReadLine();
                            break;
                        }
                    case 12: // Регистрация выселения
                        {
                            AVL.Hotel_room hm = new AVL.Hotel_room();
                            DateTime checkOutDate = new DateTime();
                            checkOutDate = DateTime.Now;
                            Registration reg = new Registration();
                            Guest newGuest = new Guest();
                            string date;
                            Console.WriteLine("Введите № паспорта постояльца: ");
                            newGuest.passport_number = newGuest.InputPassportNumber();
                            if (list.IsEmpty)
                                Console.WriteLine("\nВ гостинице никто не проживает.");
                            else
                            {
                                reg = list.FindPassport(newGuest.passport_number);
                                if (reg != null)
                                {
                                    Console.WriteLine("\nВыселить постояльца? (y/n)");
                                    date = InputLetter();
                                    if (date.Equals("y"))
                                    {
                                        hm = avl.Find(reg.room_num);
                                        hm.places++;
                                        reg.chekOut = checkOutDate.ToString();
                                        Console.WriteLine("Дата: " + reg.chekOut);
                                        Console.WriteLine("Успешно!");
                                    }
                                    else
                                        Console.WriteLine("Отмена.");
                                }
                                else
                                    Console.WriteLine("Постоялец с таким номером паспорта не проживает в гостинице!");
                            }
                            Console.ReadLine();
                            break;
                        }
                }
            }
        }
    }
}
