using MainStuff;
using NodeSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Launcher
{
    class Launch
    {
        static void Main(string[] args)
        {
            Algorithm algorithm = new Algorithm(100, -100);
            Node[] nodes = new Node[100];
            Random rnd = new Random();
            Node node3 = (Node)new Node[] { new Node(12), new Node(12), new Node(12), new Node(12) };
            Node node4 = (Node)new Node[] { new Node(12), new Node(12), new Node(12), new Node(12) };
            Node node5 = (Node)new Node[] { new Node(12), new Node(12), new Node(12), new Node(12) };

            for (int i = 0; i < 100; i++)
            {
                nodes[i] = new Node(i, null);
            }

            Node node1 = new Node(0, new Node[] { node3, node4, node5 });
            //Input 

            Console.WriteLine("Value " + (algorithm.alphaBeta(node1, node1.Nodes.Length, float.NegativeInfinity, float.PositiveInfinity, false)).Val);
            Console.WriteLine("Checkmove " + algorithm.checkMove);
            Console.WriteLine("Total moves");
        }
    }
}
