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
        static private float negInf = float.NegativeInfinity;
        static private float posInf = float.PositiveInfinity;
        static private float winPoint = 10000; //Should be Total points for all pieces
        static private float maxPoint = 100; //Extreme cutting
        static private float minPoint = -100; //Extreme cutting
        public static int checkMove = 0;

        //Need a list of magic numbers

        static public float alphaBeta(Node root, int depth, float alpha, float beta, bool maximizingPlayer)
        {
            float eva; // The value of the move (Need to implement method of calculating the value of the move)
            
            if (depth == 0 | root.Nodes == null)
            {
                checkMove++;
                Console.WriteLine(root.Val);
                return root.Val;
            }

            if (maximizingPlayer == true)  // for Maximizer Player  
            {

                float maxEva = negInf;

                foreach (Node node in root.Nodes)
                {
                    eva = alphaBeta(node, depth - 1, alpha, beta, false);
                    maxEva = Math.Max(maxEva, eva); // Highest evaluated pointage
                    alpha = Math.Max(alpha, maxEva); // Highest valid evaluated pointage in whole tree
                    if (beta <= alpha)
                    {
                        Console.WriteLine("eva " + eva + " max " + maxEva + " alpha " + alpha);
                        break;
                    }
                }
                Console.WriteLine("MaxEva" + maxEva);
                return maxEva;

            }
            else
            {

                float minEva = posInf;

                foreach (Node node in root.Nodes)
                {
                    eva = alphaBeta(node, depth - 1, alpha, beta, true);
                    minEva = Math.Min(minEva, eva);
                    beta = Math.Min(beta, minEva);
                    if (beta <= alpha)
                    {
                        Console.WriteLine("eva " + eva + " min " + minEva + " beta " + beta);
                        break;
                    }


                }
                Console.WriteLine("MinEva" + minEva);
                return minEva;
            }
        }
        static void Main(string[] args)
        {
            Node[] nodes = new Node[100];
            Random rnd = new Random();
            Node node3 = (Node)new Node[] { new Node(12), new Node(12), new Node(12), new Node(12) };
            Node node4 = (Node)new Node[] { new Node(12), new Node(12), new Node(12), new Node(12) };
            Node node5 = (Node)new Node[] { new Node(12), new Node(12), new Node(12), new Node(12) };

            for (int i = 0; i < 100; i++)
            {
                nodes[i] = new Node(i, null);
            }

            Node node1 = new Node(0, new Node[] {node3, node4, node5 });
            //Input 

            Console.WriteLine("Value " + alphaBeta(node1, node1.Nodes.Length, negInf, posInf, false));
            Console.WriteLine("Checkmove " + checkMove);
            Console.WriteLine("Total moves");
        }
    }
}
