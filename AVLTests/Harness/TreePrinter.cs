using System.Collections.Generic;
using AVLTrees;

namespace AVLTests.Harness
{
    public static class TreePrinter
    {
        /// <summary>
        /// Przechodzi drzewo metodą inorder i dane przepisuje do kolekcji
        /// </summary>
        /// <param name="tree"></param>
        /// <returns>Dane z drzewa w porządku inorder</returns>
        public static ICollection<int> PrintInorder(CAVLTree tree)
        {
            List<int> result = new List<int>();

            PrintInorder(tree.root, result);

            return result;
        }

        private static void PrintInorder(CAVLTreeNode node, ICollection<int> result)
        {
            if (node == null)
            {
                return;
            }

            if (node.lNode != null)
            {
                PrintInorder(node.lNode, result);
            }

            result.Add(node.info);

            if (node.rNode != null)
            {
                PrintInorder(node.rNode, result);
            }
        }
    }
}
