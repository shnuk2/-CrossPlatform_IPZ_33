namespace Lab2
{
    public class Program
    {

        public static bool IsValidValue(int value)
        {
            if (value < 0)
            {
                return false;
            }
            return true;
        }

        public static void Main()
        {
            string inputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "INPUT.txt");
            string input = File.ReadAllText(inputPath).Trim();
            int pos = int.Parse(input);
            for (int step = 26; step >= 1; step--)
            {
                int half = (1 << (step - 1)) - 1;
                if (pos == 1)
                {
                    string outputPath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "OUTPUT.txt");
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
