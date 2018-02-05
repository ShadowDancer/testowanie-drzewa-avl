using System;
using AVLTests.Harness;
using AVLTrees;
using Xunit;

namespace AVLTests
{
    public class TreeTests
    {


        /// <summary>
        /// Usuwanie elementów z drzewa
        /// </summary>
        [Fact]
        public void Delete()
        {
            // Arrange
            int[] numbers = { 1, 2, 19, 8, 4, -5, 17, 21 };
            Array.Sort(numbers);

            // Act
            var tree = TreeBuilder.Build(numbers);
            CAVLTreeOperationInfo operationInformation = new CAVLTreeOperationInfo();
            tree.Delete(tree.Search(19), operationInformation);
            tree.Delete(tree.Search(4), operationInformation);
            tree.Delete(tree.Search(-5), operationInformation);

            // Assert
            var result = TreePrinter.PrintInorder(tree);
            int[] expectedResult = { 1, 2, 8, 17, 21 };
            Assert.Equal(result, expectedResult);
        }

        /// <summary>
        /// Dodawanie elementów do drzewa
        /// </summary>
        [Fact]
        public void Insert()
        {
            // Arrange
            CAVLTree tree = new CAVLTree();
            int[] numbers = { 1, 2, 19, 8, 4, -5, 17, 21 };

            // Act
            CAVLTreeOperationInfo operationInfo = new CAVLTreeOperationInfo();
            foreach (var info in numbers)
            {
                tree.Insert(info, operationInfo);
            }

            // Assert
            Array.Sort(numbers);
            var result = TreePrinter.PrintInorder(tree);
            Assert.Equal(numbers, result);
        }

        [Fact]
        public void RrRotation()
        {
            // Arrange
            int[] numbers = {13, 10, 15, 5, 11, 16, 4, 8};
            var tree = TreeBuilder.Build(numbers);

            CAVLTreeOperationInfo info = new CAVLTreeOperationInfo();
            tree.Insert(3, info);

            Assert.Equal(1, info.RRRC);
            Assert.Equal(0, info.RLRC);
            Assert.Equal(0, info.LLRC);
            Assert.Equal(0, info.LRRC);
        }

        [Fact]
        public void LrRotation()
        {
            // Arrange
            int[] numbers = { 13, 10, 15, 5, 11, 16, 4, 6 };
            var tree = TreeBuilder.Build(numbers);

            CAVLTreeOperationInfo info = new CAVLTreeOperationInfo();
            tree.Insert(7, info);

            Assert.Equal(0, info.RRRC);
            Assert.Equal(0, info.RLRC);
            Assert.Equal(0, info.LLRC);
            Assert.Equal(1, info.LRRC);
        }

        [Fact]
        public void LlRotation()
        {
            // Arrange
            int[] numbers = { 30, 5, 35, 32, 40 };
            var tree = TreeBuilder.Build(numbers);

            CAVLTreeOperationInfo info = new CAVLTreeOperationInfo();
            tree.Insert(45, info);

            Assert.Equal(0, info.RRRC);
            Assert.Equal(0, info.RLRC);
            Assert.Equal(1, info.LLRC);
            Assert.Equal(0, info.LRRC);
        }

        [Fact]
        public void RlRotation()
        {
            // Arrange
            int[] numbers = { 5, 2, 7, 1, 4, 6, 9, 3, 16 };
            var tree = TreeBuilder.Build(numbers);

            // Act
            CAVLTreeOperationInfo info = new CAVLTreeOperationInfo();
            tree.Insert(15, info);

            // Assert
            Assert.Equal(0, info.RRRC);
            Assert.Equal(1, info.RLRC);
            Assert.Equal(0, info.LLRC);
            Assert.Equal(0, info.LRRC);
        }
    }
}
