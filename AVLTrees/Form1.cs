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
    public partial class Form1 : Form
    {
        public Bitmap bm;
        public Graphics g;
        public CAVLTree tree;
        public Point O;
        public Point v;
        public Point a;
        public Point P;
        public Timer tm;
        public int tTicks;
        public TreeNode pSelNode;

        public Form1()
        {
            InitializeComponent();
            bm = new Bitmap(pictureBox1.Width,
                            pictureBox1.Height,
                            System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            g = Graphics.FromImage(bm);
            tree = new CAVLTree();
            O = new Point(0, 0);
            v = new Point(0, 0);
            a = new Point(0, 0);
            P = new Point(0, 0);
            tm = new Timer();
            tm.Interval = 40;
            tm.Tick += new System.EventHandler(OnTimerEvent);
            tTicks = 0;
            pSelNode = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tmp;
            Boolean error = false;
            if (!Int32.TryParse(textBox2.Text, out tmp)) error = true;
            else
            {
                CAVLTreeOperationInfo avltoi = new CAVLTreeOperationInfo();
                if (tree.Insert(tmp, avltoi))
                {
                    String s = tmp.ToString();
                    TreeNode tn = treeView1.Nodes[0].Nodes.Add(s, s);
                    pSelNode = tn;
                    treeView1.TreeViewNodeSorter = new CNodeSorter();
                    treeView1.Sort();
                    treeView1.TreeViewNodeSorter = null;
                    treeView1.SelectedNode = tn;
                    treeView1.Select();
                    label9.Text = treeView1.Nodes[0].Nodes.Count.ToString();
                    Form2 form2 = new Form2(this, avltoi.LLRC, avltoi.RRRC, avltoi.LRRC, avltoi.RLRC);
                    form2.ShowDialog();
                }
                else
                    error = true;
            }
            if (error)
            {
                if (pSelNode != null)
                {
                    tmp = Int32.Parse(pSelNode.Text);
                    CAVLTreeNode node = tree.Search(tmp);
                    textBox2.Text = node.info.ToString();
                    textBox4.Text = node.height.ToString();
                    textBox1.Text = (node.lNode != null) ? node.lNode.height.ToString() : "-";
                    textBox5.Text = (node.rNode != null) ? node.rNode.height.ToString() : "-";
                    textBox3.Text = node.bf.ToString();
                }
                else
                    textBox2.Text = "";
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text != "Nodes:")
            {
                pSelNode = e.Node;
                textBox2.Text = e.Node.Text;
                int tmp = Int32.Parse(pSelNode.Text);
                CAVLTreeNode node = tree.Search(tmp);
                textBox4.Text = node.height.ToString();
                textBox1.Text = (node.lNode != null) ? node.lNode.height.ToString() : "-";
                textBox5.Text = (node.rNode != null) ? node.rNode.height.ToString() : "-";
                textBox3.Text = node.bf.ToString();
                O.X = pictureBox1.Width >> 1;
                O.Y = pictureBox1.Height >> 1;
                tree.Draw(g, O, tmp, out P);
                Point tmpPt;
                g.Clear(Color.Black);
                tree.Draw(g, new Point((O.X << 1) - P.X, (O.Y << 1) - P.Y), tmp, out tmpPt);
                pictureBox1.Image = bm;
            }
        }

        private void OnTimerEvent(object src, EventArgs e)
        {
            Point tmpPt;
            if (tTicks == 0)
            {
                O.X += v.X * 40;
                O.Y += v.Y * 40;
                if (pSelNode != null)
                {
                    g.Clear(Color.Black);
                    tree.Draw(g, new Point((O.X << 1) - P.X, (O.Y << 1) - P.Y), Int32.Parse(pSelNode.Text), out tmpPt);
                    pictureBox1.Image = bm;
                }
            }
            else
            {
                if (tTicks > 5)
                {
                    O.X += v.X;
                    if (v.X + a.X <= 40) v.X += a.X;
                    O.Y += v.Y;
                    if (v.Y + a.Y <= 40) v.Y += a.Y;
                    if (pSelNode != null)
                    {
                        g.Clear(Color.Black);
                        tree.Draw(g, new Point((O.X << 1) - P.X, (O.Y << 1) - P.Y), Int32.Parse(pSelNode.Text), out tmpPt);
                        pictureBox1.Image = bm;
                    }
                }
            }
            ++tTicks;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            O.X = pictureBox1.Width >> 1;
            O.Y = pictureBox1.Height >> 1;
            Point tmpPt;
            if (pSelNode != null)
            {
                g.Clear(Color.Black);
                tree.Draw(g, new Point((O.X << 1) - P.X, (O.Y << 1) - P.Y), Int32.Parse(pSelNode.Text), out tmpPt);
                pictureBox1.Image = bm;
            }
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            v.X = 1; v.Y = 0;
            a.X = 1; a.Y = 0;
            tTicks = 0;
            tm.Start();
        }

        private void button7_MouseUp(object sender, MouseEventArgs e)
        {
            tm.Stop();
        }

        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            v.X = -1; v.Y = 0;
            a.X = -1; a.Y = 0;
            tTicks = 0;
            tm.Start();
        }

        private void button5_MouseUp(object sender, MouseEventArgs e)
        {
            tm.Stop();
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            v.X = 0; v.Y = 1;
            a.X = 0; a.Y = 1;
            tTicks = 0;
            tm.Start();
        }

        private void button4_MouseUp(object sender, MouseEventArgs e)
        {
            tm.Stop();
        }

        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            v.X = 0; v.Y = -1;
            a.X = 0; a.Y = -1;
            tTicks = 0;
            tm.Start();
        }

        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
            tm.Stop();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox1 = new AboutBox1();
            aboutBox1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TreeNode[] tn;
            Boolean success = false;
            Boolean updateV = false;
            TreeNode tmp;
            CAVLTreeOperationInfo avltoi = new CAVLTreeOperationInfo();
            if ((tn = treeView1.Nodes[0].Nodes.Find(textBox2.Text.Trim(), true)).Length != 0)
            {
                tree.Delete(tree.Search(Int32.Parse(tn[0].Text)), avltoi);
                success = true;
                if ((tmp = tn[0].NextNode) != null)
                {
                    if (treeView1.SelectedNode != tmp)
                    {
                        pSelNode = tmp;
                        treeView1.SelectedNode = pSelNode;
                    }
                    else
                        updateV = true;
                    treeView1.Select();
                }
                else
                {
                    if ((tmp = tn[0].PrevNode) != null)
                    {
                        if (treeView1.SelectedNode != tmp)
                        {
                            pSelNode = tmp;
                            treeView1.SelectedNode = pSelNode;
                        }
                        else
                            updateV = true;
                        treeView1.Select();
                    }
                    else
                    {
                        pSelNode = null;
                        updateV = true;
                    }
                }
                tn[0].Remove();
                label9.Text = treeView1.Nodes[0].Nodes.Count.ToString();
            }
            else
                updateV = true;
            if (updateV)
            {
                if (pSelNode != null)
                {
                    int info = Int32.Parse(pSelNode.Text);
                    CAVLTreeNode node = tree.Search(info);
                    textBox2.Text = node.info.ToString();
                    textBox4.Text = node.height.ToString();
                    textBox1.Text = (node.lNode != null) ? node.lNode.height.ToString() : "-";
                    textBox5.Text = (node.rNode != null) ? node.rNode.height.ToString() : "-";
                    textBox3.Text = node.bf.ToString();
                    O.X = pictureBox1.Width >> 1;
                    O.Y = pictureBox1.Height >> 1;
                    tree.Draw(g, O, info, out P);
                    Point tmpPt;
                    g.Clear(Color.Black);
                    tree.Draw(g, new Point((O.X << 1) - P.X, (O.Y << 1) - P.Y), info, out tmpPt);
                    pictureBox1.Image = bm;
                }
                else
                {
                    textBox2.Text = "";
                    g.Clear(Color.Black);
                    pictureBox1.Image = bm;
                }
            }
            if (success)
            {
                Form2 form2 = new Form2(this, avltoi.LLRC, avltoi.RRRC, avltoi.LRRC, avltoi.RLRC);
                form2.ShowDialog();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox1.Text = "";
            textBox5.Text = "";
            textBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TreeNode[] tn;
            if (((tn = treeView1.Nodes[0].Nodes.Find(textBox2.Text.Trim(), true)).Length != 0) &&
                (pSelNode.Text != textBox2.Text.Trim()))
            {
                pSelNode = tn[0];
                treeView1.SelectedNode = pSelNode;
                treeView1.Select();
            }
            else
            {
                if (pSelNode != null)
                {
                    int tmp = Int32.Parse(pSelNode.Text);
                    CAVLTreeNode node = tree.Search(tmp);
                    textBox2.Text = node.info.ToString();
                    textBox4.Text = node.height.ToString();
                    textBox1.Text = (node.lNode != null) ? node.lNode.height.ToString() : "-";
                    textBox5.Text = (node.rNode != null) ? node.rNode.height.ToString() : "-";
                    textBox3.Text = node.bf.ToString();
                }
                else
                    textBox2.Text = "";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tree = new CAVLTree();
            treeView1.Nodes[0].Nodes.Clear();
            pSelNode = null;
            label9.Text = "0";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox1.Text = "";
            textBox5.Text = "";
            textBox3.Text = "";
            g.Clear(Color.Black);
            pictureBox1.Image = bm;
        }
    }
    
    public class CNodeSorter : System.Collections.IComparer
    {
        public int Compare(object n_1, object n_2)
        {
            int v_1 = Int32.Parse(((TreeNode)n_1).Text);
            int v_2 = Int32.Parse(((TreeNode)n_2).Text);
            return (v_1 == v_2) ? 0 : ((v_1 < v_2) ? -1 : 1);
        }
    }
}
