using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeSpace {
    public class Node {

        private Node superNode = null;
        private MoveMent move = null;
        public Node(MoveMent move)
        {
            this.move = move;
        }
        public Node(MoveMent move, Node superNode)
        {
            this.superNode = superNode;
            this.move = move;
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
                return move.Val;
            }
        }

    }

}
