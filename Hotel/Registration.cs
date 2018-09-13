using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    public class Registration
    {
        public string pass_num; // паспорт
        public string room_num; // номер отеля
        public string chekIn; // дата вселения
        public string chekOut; // дата выселения
        public Registration next; // ссылка на следующий объект двусвязного списка
        public Registration prev; // ссылка на предыдущий объект двусвязного списка

        public long Convert(string str)
        {
            string num = str.Remove(4, 1);
            return long.Parse(num);
        }

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
