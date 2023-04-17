using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeSpace {
    public class Node {

        private float val;
        private Node[] nodes = null;
        private Node superNode = null;
        private MoveMent move = null;

        public Node(float value)
        {
            this.val = value;
            this.nodes = null;
        }
        //Make a Node filled with Nodes and a value
        public Node(float value, Node[] nodes)
        {
            this.val = value;
            this.nodes = nodes;
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
                return nodes;
            }
        }
        public Node SuperNode 
        { 
            get 
            { 
                return superNode; 
            } 
        }
        public MoveMent Move 
        { 
            get 
            { 
                return move; 
            } 
        }
        public float Val
        {
            get
            {
                return val;
            }
        }

    }

}
