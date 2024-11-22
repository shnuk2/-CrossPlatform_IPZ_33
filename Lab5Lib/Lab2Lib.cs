using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5Lib
{
    public class Lab2Lib
    {
        public static string Lab2Res(string inputText)
        {
            string input = inputText.Trim();
            int pos = int.Parse(input);
            string result = ""; // Змінна для збереження результату

            for (int step = 26; step >= 1; step--)
            {
                int half = (1 << (step - 1)) - 1;

                if (pos == 1)
                {
                    result = ((char)('a' + step - 1)).ToString();
                    return result; // Повертаємо результат
                }
                else if (pos <= 1 + half)
                {
                    pos--;
                }
                else
                {
                    pos -= 1 + half;
                }
            }

            return result; // Повертаємо результат (на випадок, якщо цикл завершиться без умов)
        }
    }
}
