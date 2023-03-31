using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeSpace {
    public class Node {

        private float _val;
        private Node[] _nodes;
        private Move move;

        public Node(float value)
        {
            this.Val = value;
            this.Nodes = null;
        }
        //Make a Node filled with Nodes and a value
        public Node(float value, Node[] nodes)
        {
            this.Val = value;
            this.Nodes = nodes;
        }


        //Make a (presumebly) Leaf Node (Ending node)
        public static explicit operator Node(Node[] nodes)
        {
            Node node = new Node(0, nodes);
            return node;
        }
        public static explicit operator Node[](Node node)
        {
            Node[] nodes = { new Node(node.Val) };
            return nodes;
        }



        public Node[] Nodes
        {
            get
            {
                return _nodes;
            }
            set
            {
                _nodes = value;
            }
        }
        public float Val
        {
            get
            {
                return _val;
            }
            set
            {
                _val = value;
            }
        }

    }

    public class Tree{
        
    }
}
