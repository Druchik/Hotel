using System;
using System.Linq;

namespace Hotel
{
    /// <summary>
    /// Регистрация
    /// </summary>
    public class Registration
    {
        /// <summary>
        /// Паспорт
        /// </summary>
        public string pass_num;
        
        /// <summary>
        /// номер отеля
        /// </summary>
        public string room_num;

        /// <summary>
        /// Дата вселения постояльца
        /// </summary>
        public string chekIn;

        /// <summary>
        /// Дата выселения постояльца
        /// </summary>
        public string chekOut; // дата выселения

        /// <summary>
        /// Ссылка на следующий объект
        /// </summary>
        public Registration next;

        /// <summary>
        /// Ссылка на предыдущий объект
        /// </summary>
        public Registration prev;

        /// <summary>
        /// Изменение типа
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public long Convert(string str)
        {
            string num = str.Remove(4, 1);
            return long.Parse(num);
        }

        /// <summary>
        /// Ввод номера отеля
        /// </summary>
        /// <returns></returns>
        public string InputRoomNumber()
        {
            string result = "";
            int i = 0;
            char[] ch = { 'Л', 'л', 'П', 'п', 'О', 'о', 'М', 'м' };
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
    }
}
