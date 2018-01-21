using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AVLTrees
{
    public class CAVLTreeNode
    {
        public int info;
        public CAVLTreeNode lNode;
        public CAVLTreeNode rNode;
        public CAVLTreeNode par;
        public int height;
        public int bf;
        
        public CAVLTreeNode(int info)
        {
            this.info = info;
            this.lNode = null;
            this.rNode = null;
            this.par = null;
            this.height = 0;
            this.bf = 0;
        }
    }

    //   -*-   -*-   -*-

    public class CAVLTree
    {
        public CAVLTreeNode root;

        public CAVLTree()
        {
            this.root = null;
        }

        //   -*-   -*-   -*-

        public Boolean Insert(int info, CAVLTreeOperationInfo avltoi)
        {
            if (root == null)
            {
                root = new CAVLTreeNode(info);
                return true;
            }
            else
            {
                CAVLTreeNode tmp = root;
                Boolean done = false;
                while (true)
                {
                    if (info == tmp.info) return false;
                    else
                    {
                        if (info < tmp.info)
                        {
                            if (tmp.lNode != null) tmp = tmp.lNode;
                            else
                            {
                                tmp.lNode = new CAVLTreeNode(info);
                                tmp.lNode.par = tmp;
                                tmp = tmp.lNode;
                                break;
                            }
                        }
                        else
                        {
                            if (tmp.rNode != null) tmp = tmp.rNode;
                            else
                            {
                                tmp.rNode = new CAVLTreeNode(info);
                                tmp.rNode.par = tmp;
                                tmp = tmp.rNode;
                                break;
                            }
                        }
                    }
                }
                while ((!done) && (tmp.par != null))
                {
                    if (tmp.par.lNode == tmp)
                    {
                        if ((--tmp.par.bf) >= 0) done = true;
                        else
                        {
                            ++tmp.par.height;
                            if (tmp.par.bf == -2)
                            {
                                if (tmp.bf < 1)
                                {
                                    RR(tmp);
                                    ++avltoi.RRRC;
                                }
                                else
                                {
                                    LR(tmp.rNode);
                                    ++avltoi.LRRC;
                                }
                                done = true;
                            }
                        }
                    }
                    else
                    {
                        if ((++tmp.par.bf) <= 0) done = true;
                        else
                        {
                            ++tmp.par.height;
                            if (tmp.par.bf == 2)
                            {
                                if (tmp.bf > -1)
                                {
                                    LL(tmp);
                                    ++avltoi.LLRC;
                                }
                                else
                                {
                                    RL(tmp.lNode);
                                    ++avltoi.RLRC;
                                }
                                done = true;
                            }
                        }
                    }
                    tmp = tmp.par;
                }
                return true;
            }
        }

        //   -*-   -*-   -*-

        public CAVLTreeNode Search(int info)
        {
            CAVLTreeNode tmp = root;
            if (tmp == null) return tmp;
            else
            {
                while (tmp != null)
                {
                    if (info == tmp.info) return tmp;
                    else
                        tmp = (info < tmp.info) ? tmp.lNode : tmp.rNode;
                }
                return tmp;
            }
        }

        //   -*-   -*-   -*-

        public Boolean Delete(CAVLTreeNode node, CAVLTreeOperationInfo avltoi)
        {
            CAVLTreeNode pred;
            CAVLTreeNode tmp;
            Boolean left1 = false;
            Boolean left2;
            Boolean done = false;
            if (node == null) return false;
            if ((node.lNode == null) || (node.rNode == null))
            {
                pred = (node.lNode != null) ? node.lNode : node.rNode;
                if ((tmp = node.par) != null)
                {
                    if (tmp.lNode == node)
                    {
                        tmp.lNode = pred;
                        left1 = true;
                    }
                    else
                        tmp.rNode = pred;
                }
                else
                    root = pred;
                if (pred != null) pred.par = tmp;
            }
            else
            {
                pred = Prev(node);
                if (node.lNode != pred)
                {
                    (tmp = pred.par).rNode = pred.lNode;
                    if (pred.lNode != null) pred.lNode.par = pred.par;
                    pred.lNode = node.lNode;
                    pred.lNode.par = pred;
                }
                else
                {
                    tmp = pred;
                    left1 = true;
                }
                pred.rNode = node.rNode;
                pred.rNode.par = pred;
                if (node.par != null)
                {
                    if (node.par.lNode == node) node.par.lNode = pred;
                    else
                        node.par.rNode = pred;
                }
                else
                    root = pred;
                pred.par = node.par;
                pred.height = node.height;
                pred.bf = node.bf;
            }
            while ((tmp != null) && (!done))
            {
                pred = tmp;
                left2 = left1;
                tmp = tmp.par;
                if (tmp != null)
                {
                    if (tmp.lNode == pred) left1 = true;
                    else
                        left1 = false;
                }
                if (left2)
                {
                    if ((++pred.bf) > 0)
                    {
                        if (pred.bf == 2)
                        {
                            switch (pred.rNode.bf)
                            {
                                case -1: { RL(pred.rNode.lNode); ++avltoi.RLRC; break; }
                                case 0: { LL(pred.rNode); done = true; ++avltoi.LLRC; break; }
                                case 1: { LL(pred.rNode); ++avltoi.LLRC; break; }
                            }
                        }
                        else
                            done = true;
                    }
                    else
                        --pred.height;
                }
                else
                {
                    if ((--pred.bf) < 0)
                    {
                        if (pred.bf == -2)
                        {
                            switch (pred.lNode.bf)
                            {
                                case -1: { RR(pred.lNode); ++avltoi.RRRC; break; }
                                case 0: { RR(pred.lNode); done = true; ++avltoi.RRRC; break; }
                                case 1: { LR(pred.lNode.rNode); ++avltoi.LRRC; break; }
                            }
                        }
                        else
                            done = true;
                    }
                    else
                        --pred.height;
                }
            }
            return true;
        }
        
        //   -*-   -*-   -*-

        public CAVLTreeNode Prev(CAVLTreeNode node)
        {
            CAVLTreeNode tmp;
            if (node.lNode != null)
            {
                tmp = node.lNode;
                while (tmp.rNode != null) tmp = tmp.rNode;
                return tmp;
            }
            else
            {
                tmp = node;
                while ((tmp.par != null) && (tmp.par.rNode != tmp)) tmp = tmp.par;
                tmp = tmp.par;
                return tmp;
            }
        }

        //   -*-   -*-   -*-

        public void LL(CAVLTreeNode node)
        {
            CAVLTreeNode par = node.par;
            par.rNode = node.lNode;
            node.lNode = par;
            if (par != root)
            {
                if (par.par.lNode == par) par.par.lNode = node;
                else
                    par.par.rNode = node;
            }
            else
                root = node;
            if (par.rNode != null) par.rNode.par = par;
            node.par = par.par;
            par.par = node;
            if (node.bf == 0)
            {
                ++node.height;
                node.bf = -1;
                --par.height;
                par.bf = 1;
            }
            else
            {
                node.bf = 0;
                par.height -= 2;
                par.bf = 0;
            }
        }

        //   -*-   -*-   -*-
        
        public void RR(CAVLTreeNode node)
        {
            CAVLTreeNode par = node.par;
            par.lNode = node.rNode;
            node.rNode = par;
            if (par != root)
            {
                if (par.par.lNode == par) par.par.lNode = node;
                else
                    par.par.rNode = node;
            }
            else
                root = node;
            if (par.lNode != null) par.lNode.par = par;
            node.par = par.par;
            par.par = node;
            if (node.bf == 0)
            {
                ++node.height;
                node.bf = 1;
                --par.height;
                par.bf = -1;
            }
            else
            {
                node.bf = 0;
                par.height -= 2;
                par.bf = 0;
            }
        }

        //   -*-   -*-   -*-

        public void LR(CAVLTreeNode node)
        {
            CAVLTreeNode par = node.par;
            CAVLTreeNode gPar = par.par;
            par.rNode = node.lNode;
            gPar.lNode = node.rNode;
            node.lNode = par;
            node.rNode = gPar;
            if (gPar != root)
            {
                if (gPar.par.lNode == gPar) gPar.par.lNode = node;
                else
                    gPar.par.rNode = node;
            }
            else
                root = node;
            if (par.rNode != null) par.rNode.par = par;
            if (gPar.lNode != null) gPar.lNode.par = gPar;
            node.par = gPar.par;
            par.par = node;
            gPar.par = node;
            switch (node.bf)
            {
                case -1:
                    {
                        par.bf = 0;
                        gPar.bf = 1;
                        break;
                    }
                case 0:
                    {
                        par.bf = 0;
                        gPar.bf = 0;
                        break;
                    }
                case 1:
                    {
                        par.bf = -1;
                        gPar.bf = 0;
                        break;
                    }
            }
            ++node.height;
            node.bf = 0;
            --par.height;
            gPar.height -= 2;
        }

        //   -*-   -*-   -*-

        public void RL(CAVLTreeNode node)
        {
            CAVLTreeNode par = node.par;
            CAVLTreeNode gPar = par.par;
            par.lNode = node.rNode;
            gPar.rNode = node.lNode;
            node.lNode = gPar;
            node.rNode = par;
            if (gPar != root)
            {
                if (gPar.par.lNode == gPar) gPar.par.lNode = node;
                else
                    gPar.par.rNode = node;
            }
            else
                root = node;
            if (par.lNode != null) par.lNode.par = par;
            if (gPar.rNode != null) gPar.rNode.par = gPar;
            node.par = gPar.par;
            par.par = node;
            gPar.par = node;
            switch (node.bf)
            {
                case -1:
                    {
                        par.bf = 1;
                        gPar.bf = 0;
                        break;
                    }
                case 0:
                    {
                        par.bf = 0;
                        gPar.bf = 0;
                        break;
                    }
                case 1:
                    {
                        par.bf = 0;
                        gPar.bf = -1;
                        break;
                    }
            }
            ++node.height;
            node.bf = 0;
            --par.height;
            gPar.height -= 2;
        }
        
        //   -*-   -*-   -*-

        public void DrawNode(Graphics g, Point P, Pen outlnPn, Brush fillBr, CAVLTreeNode node)
        {
            g.FillEllipse(fillBr, P.X - 16, P.Y - 16, 32, 32);
            g.DrawEllipse(outlnPn, P.X - 16, P.Y - 16, 32, 32);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Far;
            g.DrawString(node.info.ToString(), new Font("Arial", 8.0f), Brushes.White, new PointF(P.X, P.Y), sf);
            sf.LineAlignment = StringAlignment.Near;
            g.DrawString(node.bf.ToString(),
                         new Font("Arial", 8.0f), Brushes.Black, new PointF(P.X, P.Y), sf);
        }

        //   -*-   -*-   -*-

        private enum CBrowsingDirection { FORWARD, BACKWARD }
        
        public Boolean Draw(Graphics g, Point O, int info, out Point P)
        {
            Boolean fnd = false;
            P = new Point(0, 0);
            if (root != null)
            {
                Point[] arr = new Point[root.height + 2];
                for (int i = 0; i < arr.Length; ++i) arr[i] = new Point(0, (i == 0) ? 0 : -(i + 2));
                CAVLTreeNode tmp1 = root;
                CAVLTreeNode tmp2;
                Brush outlnBr = new SolidBrush(Color.FromArgb(0, 192, 255));
                Brush nodeBr = new SolidBrush(Color.FromArgb(0, 64, 128));
                Brush selNodeBr = new SolidBrush(Color.FromArgb(255, 192, 0));
                CBrowsingDirection bd = CBrowsingDirection.FORWARD;
                Point pt;
                int depth = 0;
                while (tmp1 != null)
                {
                    switch (bd)
                    {
                        case CBrowsingDirection.FORWARD:
                            {
                                if (tmp1.lNode != null)
                                {
                                    tmp1 = tmp1.lNode;
                                    arr[depth + 1].Y = ((arr[depth + 1].Y < arr[depth].Y - 2) ? arr[depth].Y - 1 : arr[depth + 1].Y + 2);
                                    ++depth;
                                }
                                else
                                {
                                    arr[depth].Y = ((arr[depth + 1].Y < arr[depth].Y) ? arr[depth].Y : arr[depth + 1].Y + 1);
                                    if (tmp1.rNode != null)
                                    {
                                        tmp1 = tmp1.rNode;
                                        arr[depth + 1].Y = arr[depth].Y + 1;
                                        ++depth;
                                    }
                                    else
                                        bd = CBrowsingDirection.BACKWARD;
                                }
                                break;
                            }
                        case CBrowsingDirection.BACKWARD:
                            {
                                tmp2 = tmp1;
                                tmp1 = tmp1.par;
                                --depth;
                                if (tmp1 != null)
                                {
                                    if (tmp1.lNode == tmp2)
                                    {
                                        arr[depth].Y = arr[depth + 1].Y + 1;
                                        if (tmp1.rNode != null)
                                        {
                                            bd = CBrowsingDirection.FORWARD;
                                            tmp1 = tmp1.rNode;
                                            arr[depth + 1].X = arr[depth + 1].Y;
                                            arr[depth + 1].Y = arr[depth].Y + 1;
                                            ++depth;
                                        }
                                        else
                                        {
                                            g.DrawLine(new Pen(outlnBr),
                                                       O.X + (arr[depth + 1].Y * 24), O.Y + ((depth + 1) * 32),
                                                       O.X + (arr[depth].Y * 24), O.Y + (depth * 32));
                                            DrawNode(g,
                                                     pt = new Point(O.X + (arr[depth + 1].Y * 24), O.Y + ((depth + 1) * 32)),
                                                     new Pen(outlnBr),
                                                     (tmp2.info == info) ? selNodeBr : nodeBr,
                                                     tmp2);
                                            if (tmp2.info == info)
                                            {
                                                P = pt;
                                                fnd = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (tmp1.lNode != null)
                                        {
                                            arr[depth].Y = (arr[depth + 1].X + arr[depth + 1].Y) / 2;
                                            g.DrawLine(new Pen(outlnBr),
                                                       O.X + (arr[depth + 1].X * 24), O.Y + ((depth + 1) * 32),
                                                       O.X + (arr[depth].Y * 24), O.Y + (depth * 32));
                                            DrawNode(g,
                                                     pt = new Point(O.X + (arr[depth + 1].X * 24), O.Y + ((depth + 1) * 32)),
                                                     new Pen(outlnBr),
                                                     (tmp1.lNode.info == info) ? selNodeBr : nodeBr,
                                                     tmp1.lNode);
                                            if (tmp1.lNode.info == info)
                                            {
                                                P = pt;
                                                fnd = true;
                                            }
                                            g.DrawLine(new Pen(outlnBr),
                                                       O.X + (arr[depth + 1].Y * 24), O.Y + ((depth + 1) * 32),
                                                       O.X + (arr[depth].Y * 24), O.Y + (depth * 32));
                                            DrawNode(g,
                                                     pt = new Point(O.X + (arr[depth + 1].Y * 24), O.Y + ((depth + 1) * 32)),
                                                     new Pen(outlnBr),
                                                     (tmp2.info == info) ? selNodeBr : nodeBr,
                                                     tmp2);
                                            if (tmp2.info == info)
                                            {
                                                P = pt;
                                                fnd = true;
                                            }
                                        }
                                        else
                                        {
                                            g.DrawLine(new Pen(outlnBr),
                                                       O.X + (arr[depth + 1].Y * 24), O.Y + ((depth + 1) * 32),
                                                       O.X + (arr[depth].Y * 24), O.Y + (depth * 32));
                                            DrawNode(g,
                                                     pt = new Point(O.X + (arr[depth + 1].Y * 24), O.Y + ((depth + 1) * 32)),
                                                     new Pen(outlnBr),
                                                     (tmp2.info == info) ? selNodeBr : nodeBr,
                                                     tmp2);
                                            if (tmp2.info == info)
                                            {
                                                P = pt;
                                                fnd = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    DrawNode(g,
                                             pt = new Point(O.X + (arr[depth + 1].Y * 24), O.Y + ((depth + 1) * 32)),
                                             new Pen(outlnBr),
                                             (tmp2.info == info) ? selNodeBr : nodeBr,
                                             tmp2);
                                    if (tmp2.info == info)
                                    {
                                        P = pt;
                                        fnd = true;
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            return fnd;
        }
    }

    //   -*-   -*-   -*-

    public class CAVLTreeOperationInfo
    {
        public int LLRC;
        public int RRRC;
        public int LRRC;
        public int RLRC;

        public CAVLTreeOperationInfo()
        {
            LLRC = 0;
            RRRC = 0;
            LRRC = 0;
            RLRC = 0;
        }
    }
}
