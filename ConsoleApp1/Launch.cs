using MainStuff;
using NodeSpace;
using Objects;
using GameSpace;
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

            
            Board board = new Board(8, 8);
            
            Move forward = new Move(1, (0, 1), true, false);
            //Move jump = new Move(1, (0, 2));
            Move diagAttackR = new Move(1, (1,1), false, true);
            Move diagAttackL = new Move(1, (-1,1), false, true); 
            Move[] pawnMoves = { forward, diagAttackR, diagAttackL };
            for (int x = 0; x < 8; x++)
            {
                board.placePiece(new MoveAbles(pawnMoves, 1, '↑', true), x, 6);
                board.placePiece(new MoveAbles(pawnMoves, 1, '↓', false), x, 1);
            }

            Game game = new Game(1000, board, true);
            Algorithm algorithm = new Algorithm(game.getWinPoint());


            MoveMent m = new MoveMent((10, 10), (20, 20), new (int, int)[] { (19, 19), (11, 11)}, 10);
            Console.WriteLine(m);
            game.showAllMoves();

            //Node[] nodes = new Node[100];
            //Random rnd = new Random();
            //Node node3 = (Node)new Node[] { new Node(12), new Node(12), new Node(12), new Node(12) };
            //Node node4 = (Node)new Node[] { new Node(12), new Node(12), new Node(12), new Node(12) };
            //Node node5 = (Node)new Node[] { new Node(12), new Node(12), new Node(12), new Node(12) };

            //for (int i = 0; i < 100; i++)
            //{
            //    nodes[i] = new Node(i, null);
            //}

            //Node node1 = new Node(0, new Node[] { node3, node4, node5 });
            //Input 

            //Console.WriteLine("Value " + (algorithm.alphaBeta(node1, node1.Nodes.Length, float.NegativeInfinity, float.PositiveInfinity, false)).Val);
            //Console.WriteLine("Checkmove " + algorithm.checkMove);
            //Console.WriteLine("Total moves");


        }
    }
}
