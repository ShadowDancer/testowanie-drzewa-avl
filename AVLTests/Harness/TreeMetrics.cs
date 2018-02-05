using System;
using System.Collections.Generic;
using System.Linq;
using AVLTrees;

namespace AVLTests.Harness
{
    public static class TreeMetrics
    {
        /// <summary>
        /// Sprawdza, czy drzewo jest zbalansowane (wysokość lewego i prawego poddrzewa różnią się co najwyżej o 2)
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static bool TestIsBalanced(this CAVLTree tree)
        {
            Queue<CAVLTreeNode> nodes = new Queue<CAVLTreeNode>();
            nodes.Enqueue(tree.root);
            while (nodes.Any())
            {
                var node = nodes.Dequeue();

                var avlFactor = TestHeight(node.lNode) - TestHeight(node.rNode);
                if (avlFactor < -1 || avlFactor > 1)
                {
                    return false;
                }

                if (node.lNode != null)
                {
                    nodes.Enqueue(node.lNode);
                }
                if (node.rNode != null)
                {
                    nodes.Enqueue(node.rNode);
                }
            }
            return true;
        }
        
        /// <summary>
        /// Raportuje wysokość drzewa avl
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static int TestHeight(CAVLTreeNode node)
        {
            if (node == null)
            {
                return 0;
            }

            return Math.Max(TestHeight(node.lNode), TestHeight(node.rNode)) + 1;
        }
    }
}
