using System.Diagnostics;

namespace Lab5Lib
{
    public class Lab1Lib
    {
        public static string Lab1Res(string input)
        {
            int[] numbers = InputValidation(input);
            return FindSequence(numbers[0], numbers[1], numbers[2]);
        }
        private static int[] InputValidation(string input)
        {
            string[] numbers = input.Split(" ");
            int.TryParse(numbers[0], out int n);
            int.TryParse(numbers[1], out int k);
            int.TryParse(numbers[2], out int m);
            if (n > 26 || k > n)
            {
                throw new ArgumentOutOfRangeException("Input numbers not in range");
            }
            else
            {
                return new int[3] { n, k, m };
            }
        }
        private static string FindSequence(int n, int k, int pos)
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
    }
}
