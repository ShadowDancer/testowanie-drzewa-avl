using System;
using System.Collections.Generic;
using System.Linq;
using AVLTests.Harness;
using AVLTrees;
using Xunit;

namespace AVLTests
{
    public class RandomTests
    {
        /// <summary>
        /// Sprawdzneie czy po wstawieniu losowych liczb drzewo spełnia warunek bst
        /// </summary>
        [Fact]
        public void InserBstCheck()
        {
            // Arrange
            CAVLTree tree = new CAVLTree();
            Random rand = new Random();
            CAVLTreeOperationInfo info = new CAVLTreeOperationInfo();


            // Act
            for (int i = 0; i < 100000; i++)
            {
                tree.Insert(rand.Next(0, 1000000), info);
            }

            // Assert
            var numbers = TreePrinter.PrintInorder(tree).ToArray();
            int prev = numbers[0];
            for (int i = 0; i < numbers.Length; i++)
            {
                Assert.True(numbers[i] >= prev);
                prev = numbers[i];
            }
        }

        /// <summary>
        /// Sprawdzneie czy po wstawieniu i usunięciu losowych liczb drzewo spełnia warunek bst
        /// </summary>
        [Fact]
        public void InserDeleteBstCheck()
        {
            // Arrange
            CAVLTree tree = new CAVLTree();
            Random rand = new Random();
            CAVLTreeOperationInfo info = new CAVLTreeOperationInfo();
            var numbers = new List<int>();

            // Act
            for (int i = 0; i < 100000; i++)
            {
                int val = rand.Next(0, 1000000);
                tree.Insert(val, info);
                numbers.Add(val);
            }
            int delNum = rand.Next(999);
            for (int i = 0; i < delNum; i++)
            {
                tree.Delete(tree.Search(numbers[rand.Next(numbers.Count)]), info);
            }

            // Assert
            numbers = TreePrinter.PrintInorder(tree).ToList();
            int prev = numbers[0];
            for (int i = 0; i < numbers.Count; i++)
            {
                Assert.True(numbers[i] >= prev);
                prev = numbers[i];
            }
        }

        /// <summary>
        /// Sprawdzneie czy po wstawieniu losowych liczb drzewo spełnia warunek avl
        /// </summary>
        [Fact]
        public void InsertBalanceCheck()
        {
            // Arrange
            CAVLTree tree = new CAVLTree();
            Random rand = new Random();
            CAVLTreeOperationInfo info = new CAVLTreeOperationInfo();

            // Act
            for (int i = 0; i < 100000; i++)
            {
                tree.Insert(rand.Next(0, 1000000), info);
            }

            // Assert
            Assert.True(tree.TestIsBalanced());
        }


        /// <summary>
        /// Sprawdzneie czy po wstawieniu i usunięciu losowych liczb drzewo spełnia warunek avl
        /// </summary>
        [Fact]
        public void InsertDeleteAvlCheck()
        {
            // Arrange
            CAVLTree tree = new CAVLTree();
            Random rand = new Random();
            CAVLTreeOperationInfo info = new CAVLTreeOperationInfo();
            var numbers = new List<int>();

            // Act
            for (int i = 0; i < 100000; i++)
            {
                int val = rand.Next(0, 1000000);
                tree.Insert(val, info);
                numbers.Add(val);
            }
            int delNum = rand.Next(999);
            for (int i = 0; i < delNum; i++)
            {
                tree.Delete(tree.Search(numbers[rand.Next(numbers.Count)]), info);
            }

            // Assert
            Assert.True(tree.TestIsBalanced());
        }
    }
}
