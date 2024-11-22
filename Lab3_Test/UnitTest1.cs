using Lab3;
namespace Lab3_Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestShortestPath1()
        {
            string[] input = new string[]
            {
                "3 2 1",
                "0 1 1",
                "4 0 1",
                "2 1 0"
            };

            int result = Program.SetResult(input);
            Assert.Equal(3, result);
        }

        [Fact]
        public void TestShortestPath2()
        {
            string[] input = new string[]
            {
                "3 1 3",
                "0 4 1",
                "4 0 1",
                "2 1 0"
            };

            int result = Program.SetResult(input);
            Assert.Equal(1, result);
        }

        [Fact]
        public void TestShortestPath3()
        {
            string[] input = new string[]
            {
                "4 1 4",
                "0 3 1 6",
                "3 0 -1 1",
                "1 -1 0 1",
                "6 1 1 0"
            };

            int result = Program.SetResult(input);
            Assert.Equal(2, result);
        }

        [Fact]
        public void TestNoPath()
        {
            string[] input = new string[]
            {
                "3 1 3",
                "0 1 -1",
                "-1 0 -1",
                "-1 -1 0"
            };

            int result = Program.SetResult(input);
            Assert.Equal(-1, result);
        }

        [Fact]
        public void TestSingleNode()
        {
            string[] input = new string[]
            {
                "1 1 1",
                "0"
            };

            int result = Program.SetResult(input);
            Assert.Equal(0, result);
        }

        [Fact]
        public void TestTwoNodesConnected()
        {
            string[] input = new string[]
            {
                "2 1 2",
                "0 1",
                "1 0"
            };

            int result = Program.SetResult(input);
            Assert.Equal(1, result);
        }

        [Fact]
        public void TestTwoNodesNotConnected()
        {
            string[] input = new string[]
            {
                "2 1 2",
                "0 -1",
                "-1 0"
            };

            int result = Program.SetResult(input);
            Assert.Equal(-1, result);
        }
    }
}
