using System;
using AVLTests.Harness;
using Xunit;

namespace AVLTests
{
    public class BasicTests
    {
        /// <summary>
        /// Test do sprawdzenia czy uprząż działa poprawnie, tworzy i odczytuje predefiniowane drzewo
        /// </summary>
        [Fact]
        public void HarnessTest()
        {
            // Arrange
            int[] numbers = {1, 2, 19, 8, 4, -5, 17, 21};
            Array.Sort(numbers);

            // Act
            var tree = TreeBuilder.Build(numbers);
            var result = TreePrinter.PrintInorder(tree);
            
            // Assert
            Assert.Equal(result, numbers);
        }
    }
}
