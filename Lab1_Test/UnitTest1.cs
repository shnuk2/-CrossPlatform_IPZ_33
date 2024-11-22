using Lab1;

namespace Tests
{
    public class Lab1_Test
    {
        [Fact]
        public void Test_Valid1()
        {

            int n = 50;
            int k = 3;
            int pos = 10;

            bool result = Program.isValuesValid(n, k, pos);
            Console.WriteLine(result.ToString());
            Console.WriteLine("Should be false");


            Assert.False(result);
        }

        [Fact]
        public void Test_Valid2()
        {

            int n = 5;
            int k = 33;
            int pos = 10;


            bool result = Program.isValuesValid(n, k, pos);
            Console.WriteLine(result.ToString());
            Console.WriteLine("Should be false");

            Assert.False(result);
        }

        [Fact]
        public void Test_Valid3()
        {

            int n = 5;
            int k = 3;
            int pos = 100;


            bool result = Program.isValuesValid(n, k, pos);
            Console.WriteLine(result.ToString());
            Console.WriteLine("Should be false");

            Assert.False(result);
        }

        [Fact]
        public void Test_EdgeCase1()
        {

            int n = 3;
            int k = 2;
            int pos = 3;


            string result = Program.FindSequence(n, k, pos);
            Console.WriteLine(result);
            Console.WriteLine("Should be bc");

            Assert.Equal("bc", result);
        }

        [Fact]
        public void Test_EdgeCase2()
        {

            int n = 4;
            int k = 3;
            int pos = 4;


            string result = Program.FindSequence(n, k, pos);
            Console.WriteLine(result);
            Console.WriteLine("Should be bcd");

            Assert.Equal("bcd", result);
        }
    }
}