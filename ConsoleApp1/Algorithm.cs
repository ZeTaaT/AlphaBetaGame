using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GameSpace;
using NodeSpace;

namespace MainStuff
{
    public class Algorithm
    {
        private float negInf = float.NegativeInfinity;
        private float posInf = float.PositiveInfinity;

        private float maxPoint = 100; //Extreme cutting
        private float minPoint = -100; //Extreme cutting
        public int checkMove = 0;

        //Need a list of magic numbers

        
        public Algorithm(float maxPoint, float minPoint)
        {
            this.maxPoint = maxPoint;
            this.minPoint = minPoint;
        }
        public Algorithm(float winPoint)
        {
            maxPoint = winPoint;
        }
        public Node alphaBeta(Node root, int depth, float alpha, float beta, bool maximizingPlayer)
        {
            Node eva; // The value of the move (Need to implement method of calculating the value of the move)
            
            if (depth == 0 | root.Nodes == null)
            {
                checkMove++;
                Console.WriteLine(root.Val);
                return root;
            }

            if (maximizingPlayer == true)  // for Maximizer Player  
            {

                Node maxEva = new Node(negInf, null);

                foreach (Node node in root.Nodes)
                {
                    eva = alphaBeta(node, depth - 1, alpha, beta, false);
                    maxEva = mathEva(maxEva, eva, true); // Highest evaluated pointage
                    alpha = Math.Max(alpha, maxEva.Val); // Highest valid evaluated pointage in whole tree
                    if (beta <= alpha)
                    {
                        Console.WriteLine("eva " + eva.Val + " max " + maxEva.Val + " alpha " + alpha);
                        break;
                    }
                }
                Console.WriteLine("MaxEva" + maxEva.Val);
                return maxEva;

            }
            else
            {

                Node minEva = new Node(posInf, null);

                foreach (Node node in root.Nodes)
                {
                    eva = alphaBeta(node, depth - 1, alpha, beta, true);
                    minEva = mathEva(minEva, eva, false);
                    beta = Math.Min(beta, minEva.Val);
                    if (beta <= alpha)
                    {
                        Console.WriteLine("eva " + eva.Val + " min " + minEva.Val + " beta " + beta);
                        break;
                    }


                }
                Console.WriteLine("MinEva" + minEva.Val);
                return minEva;
            }
        }
        private Node mathEva(Node node1, Node node2, bool max)
        {
            if (max) {
                if (node1.Val >= node2.Val) {
                    return node1;
                } else {
                    return node2;
                }
            } else {
                if (node1.Val >= node2.Val)
                {
                    return node2;
                }
                else
                {
                    return node1;
                }
            }

        }
      
    }
}
