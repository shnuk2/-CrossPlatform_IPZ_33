using System.Diagnostics;

namespace Lab4ClassLibrary
{
    public class Lab1Lib
    {
        public static string FindSequence(int n, int k, int pos)
        {
            if (isValuesValid(n, k, pos))
            {
                int[,] c = new int[n + 1, n + 1];
                for (int i = 0; i <= n; i++)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        if (j == 0 || j == i)
                        {
                            c[i, j] = 1;
                        }
                        else
                        {
                            c[i, j] = c[i - 1, j - 1] + c[i - 1, j];
                        }
                    }
                }

                List<char> res = new List<char>();
                int start = 0;
                for (int index = 0; index < k; index++)
                {
                    for (int cur = start; cur + k - index <= n; cur++)
                    {
                        Debug.Assert(n - cur - 1 >= 0);
                        Debug.Assert(0 <= k - index - 1 && k - index - 1 <= n - cur - 1);
                        if (pos <= c[n - cur - 1, k - index - 1])
                        {
                            start = cur + 1;
                            res.Add((char)('a' + cur));
                            break;
                        }
                        else
                        {
                            pos -= c[n - cur - 1, k - index - 1];
                        }
                    }
                }

                Debug.Assert(pos == 1);
                res.Add('\0');
                return new string(res.ToArray(), 0, res.Count - 1);
            }
            return "Wrong values";
        }
        public static bool isValuesValid(int a, int b, int c)
        {
            if (a < 1 || a > 26 || b < 1 || b > 26 || c > 26)
            {
                return false;
            }
            return true;
        }
        public static void RunLab1(string inputpath, string outputFilePath)
        {
            string[] input = File.ReadAllText(inputpath).Trim().Split(' ');
            int n = int.Parse(input[0]);
            int k = int.Parse(input[1]);
            int pos = int.Parse(input[2]);

            string result = FindSequence(n, k, pos);

            File.WriteAllText(outputFilePath, result);
        }
    }
}
