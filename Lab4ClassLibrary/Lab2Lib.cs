using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4ClassLibrary
{
    public class Lab2Lib
    {
        public static void RunLab2(string inputPath,string outputPath)
        {
            string input = File.ReadAllText(inputPath).Trim();
            int pos = int.Parse(input);
            for (int step = 26; step >= 1; step--)
            {
                int half = (1 << (step - 1)) - 1;
                if (pos == 1)
                {
                    File.WriteAllText(outputPath, ((char)('a' + step - 1)).ToString());
                    return;
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
        }
    }
}
