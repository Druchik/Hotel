using System;

namespace Hotel
{
    //static public List<Guest> guests = new List<Guest>();
    /// <summary>
    /// Постоялец гостиницы
    /// </summary>
    public class Guest
    {
        /// <summary>
        /// Паспорт постояльца
        /// </summary>
        public string passport_number;

        /// <summary>
        /// Фамилия Имя Отчество
        /// </summary>
        public string FIO;

        /// <summary>
        /// Год рождения
        /// </summary>
        public int birth_year;

        /// <summary>
        /// Адрес проживания
        /// </summary>
        public string adress;

        /// <summary>
        /// Цель визита
        /// </summary>
        public string purpose_of_visit;

        public Guest next;

        /// <summary>
        /// Получение ключа
        /// </summary>
        /// <returns></returns>
        public long GetKey()
        {
            string num = passport_number.Remove(4, 1);
            return long.Parse(num);
        }

        public void SetNextNode(Guest obj)
        {
            next = obj;
        }

        public Guest GetNextNode()
        {
            return next;
        }

        /// <summary>
        /// Ввод только символов
        /// </summary>
        /// <returns></returns>
        public string InputParseNoDigital()
        {
            int num;
            string str;
            while (true)
            {
                if (Int32.TryParse(str = Console.ReadLine(), out num))
                    Console.WriteLine("Некорректный ввод");
                else
                    break;
            }
            return str;
        }

        /// <summary>
        /// Ввод паспорта
        /// </summary>
        /// <returns></returns>
        public string InputPassportNumber()
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
                    default:
                        if (char.IsDigit(c: k.KeyChar) && i != 4)
                        {
                            Console.Write(value: k.KeyChar);
                            result += k.KeyChar;
                            i++;
                        }
                        else if (i == 4 && k.KeyChar == '-')
                        {
                            Console.Write(k.KeyChar);
                            result += k.KeyChar;
                            i++;
                        }
                        break;
                }
            } while (i != 11);
            return result;
        }

        /// <summary>
        /// Проверка ввода даты рождения
        /// </summary>
        /// <returns></returns>
        public int InputParseBirthYear()
        {
            int i = 0;
            string str = "";
            do
            {
                var k = Console.ReadKey(true);
                switch (k.Key)
                {
                    case ConsoleKey.Backspace:
                        if (str.Length > 0)
                        {
                            str = str.Remove(startIndex: str.Length - 1, count: 1);
                            Console.Write(value: $"{k.KeyChar} {k.KeyChar}");
                            i--;
                        }
                        break;
                    default:
                        if (char.IsDigit(c: k.KeyChar) && i == 0 && (k.KeyChar == '1' || k.KeyChar == '2'))
                        {
                            Console.Write(value: k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }
                        else if (i == 1 && str.Contains("1") && k.KeyChar == '9')
                        {
                            Console.Write(k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }
                        else if (i == 1 && str.Contains("2") && k.KeyChar == '0')
                        {
                            Console.Write(k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }
                        else if (i == 2 && str.Contains("20") && k.KeyChar == '0')
                        {
                            Console.Write(k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }
                        else if (i == 3 && str.Contains("200") && k.KeyChar == '0')
                        {
                            Console.Write(k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }
                        else if (i > 1 && str.Contains("1") && char.IsDigit(c: k.KeyChar))
                        {
                            Console.Write(k.KeyChar);
                            str += k.KeyChar;
                            i++;
                        }
                        break;
                }
            } while (i != 4);
            return int.Parse(str);
        }

        /// <summary>
        /// Ввод нового постояльца
        /// </summary>
        /// <param name="newGuest"> Постоялец </param>
        /// <param name="hashtable"> Хэш-таблица </param>
        /// <returns></returns>
        public Guest Input(Guest newGuest, Hashtable hashtable)
        {
            bool isPresent;
            Hashtable ht = hashtable;
            do
            {
                Console.WriteLine("Введите номер паспорта постояльца: ");
                newGuest.passport_number = InputPassportNumber();
                isPresent = ht.Find(newGuest.GetKey());
                if (isPresent)
                    Console.WriteLine("\nПостоялец с таким номером паспорта уже внесен в базу!");
            } while (isPresent);
            
            Console.WriteLine("\nВведите ФИО постояльца: ");
            newGuest.FIO = InputParseNoDigital();
            Console.WriteLine("Введите год рождения постояльца: ");
            newGuest.birth_year = InputParseBirthYear();
            Console.WriteLine("\nВведите адрес постояльца: ");
            newGuest.adress = InputParseNoDigital();
            Console.WriteLine("Введите цель визита: ");
            newGuest.purpose_of_visit = InputParseNoDigital();
            return newGuest;
        }
    }
}
