using System;
using System.IO;
using Xunit;
using Lab2;

namespace Lab2_Test
{
    public class ProgramTests
    {
        [Fact]
        public void IsValidValue_NegativeValue_ReturnsFalse()
        {
            int value = -1;

            
            bool result = Program.IsValidValue(value);

          
            Assert.False(result);
        }

        [Fact]
        public void IsValidValue_ZeroValue_ReturnsTrue()
        {
           
            int value = 0;

          
            bool result = Program.IsValidValue(value);

           
            Assert.True(result);
        }

        [Fact]
        public void IsValidValue_PositiveValue_ReturnsTrue()
        {
            int value = 10;

         
            bool result = Program.IsValidValue(value);

        
            Assert.True(result);
        }

        [Fact]
        public void Main_ValidInput_ProducesCorrectOutput()
        {

            string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "INPUT.txt");
            string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "OUTPUT.txt");
            File.WriteAllText(inputFilePath, "5");

            Program.Main();

            string output = File.ReadAllText(outputFilePath).Trim();
            Assert.Equal("v", output);
        }

        [Fact]
        public void Main_ValidInput_CorrectOutput1()
        {

            string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "INPUT.txt");
            string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "OUTPUT.txt");
            File.WriteAllText(inputFilePath, "4");

            Program.Main();

            string output = File.ReadAllText(outputFilePath).Trim();
            Assert.Equal("w", output);
        }

        [Fact]
        public void Main_ValidInput_CorrectOutput2()
        {
            string inputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "INPUT.txt");
            string outputFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Lab2", "OUTPUT.txt");
            File.WriteAllText(inputFilePath, "26");

            Program.Main();
            string output = File.ReadAllText(outputFilePath).Trim();
            Assert.Equal("a", output);
        }
    }
}
