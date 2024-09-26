using Xunit;
using BankSystem.App;


namespace SemplForUnitTest.Tests
{
    public class SimpleCalculatorTests
    {
        [Fact]
        public void GetSum_2_Plus_5_Eq_7()
        {
            //Arrange
            SimpleCalculator calculator = new SimpleCalculator();
            //Act
            int result = calculator.GetSum(2, 5);
            //Assert
            Assert.Equal(result, 7);
        }
        [Fact]
        public void GetSubstraction_5_Minus_4_Eq_1()
        {
            //Arrange
            SimpleCalculator calculator = new SimpleCalculator();
            //Act
            int result = calculator.GetSubstraction(5, 4);
            //Assert
            Assert.Equal(result, 1);
        }
    }
}
