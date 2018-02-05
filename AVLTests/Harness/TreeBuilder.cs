using System.Collections.Generic;
using AVLTrees;

namespace AVLTests.Harness
{
    public static class TreeBuilder
    {
        /// <summary>
        /// Tworzy drzewo po kolei wkładając dane
        /// </summary>
        /// <param name="build"></param>
        /// <returns></returns>
        public static CAVLTree Build(IEnumerable<int> build)
        {
            CAVLTree tree = new CAVLTree();
            var operationInvo = new CAVLTreeOperationInfo();
            foreach (var i in build)
            {
                tree.Insert(i, operationInvo);
            }
            return tree;
        }
    }
}
