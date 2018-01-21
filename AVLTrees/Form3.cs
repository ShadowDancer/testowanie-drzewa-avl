using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AVLTrees
{
    public partial class Form3 : Form
    {
        public Form1 par;

        public Form3(Form1 par)
        {
            InitializeComponent();
            this.par = par;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            int lR;
            int rR;
            int steps;
            if (Int32.TryParse(textBox1.Text, out lR) &&
                Int32.TryParse(textBox2.Text, out rR) &&
                Int32.TryParse(textBox3.Text, out steps))
            {
                if ((lR <= rR) && (steps > 0))
                {
                    Random r = new Random();
                    int tmp;
                    TreeNode tn = null;
                    par.tree = new CAVLTree();
                    par.treeView1.Nodes[0].Nodes.Clear();
                    CAVLTreeOperationInfo avltoi = new CAVLTreeOperationInfo();
                    LinkedList<TreeNode> ll = new LinkedList<TreeNode>();
                    for (int i = 0; i < steps; ++i)
                    {
                        if (par.tree.Insert(tmp = lR + r.Next((rR - lR) + 1), avltoi))
                        {
                            tn = new TreeNode(tmp.ToString());
                            tn.Name = tmp.ToString();
                            ll.AddLast(tn);
                            par.pSelNode = tn;                            
                        }
                    }
                    par.treeView1.Nodes[0].Nodes.AddRange(ll.ToArray());
                    par.treeView1.TreeViewNodeSorter = new CNodeSorter();
                    par.treeView1.Sort();
                    par.treeView1.TreeViewNodeSorter = null;
                    par.treeView1.SelectedNode = tn;
                    par.label9.Text = par.treeView1.Nodes[0].Nodes.Count.ToString();
                    Form2 form2 = new Form2(this, avltoi.LLRC, avltoi.RRRC, avltoi.LRRC, avltoi.RLRC);
                    form2.ShowDialog();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
