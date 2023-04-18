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
            Move diagUpAttackR = new Move(1, (1,1), false, true);
            Move diagUpAttackL = new Move(1, (-1,1), false, true); 

            Move[] pawnMoves = { forward, diagUpAttackR, diagUpAttackL };


            Move bigDiagUpAtkR = new Move(board.getHeigth(), (1, 1), true, true);
            Move bigDiagUpAtkL = new Move(board.getHeigth(), (-1, 1), true, true);
            Move bigDiagDownAtkR = new Move(board.getHeigth(), (1, -1), true, true);
            Move bigDiagDownAtkL = new Move(board.getHeigth(), (-1, -1), true, true);

            Move[] bishopMoves = { bigDiagUpAtkR, bigDiagUpAtkL, bigDiagDownAtkR, bigDiagDownAtkL };


            Move bigForwardAttack = new Move(board.getHeigth(), (0, 1), true, true);
            Move bigRightAttack = new Move(board.getHeigth(), (1, 0), true, true);
            Move bigDownwardAttack = new Move(board.getHeigth(), (0, -1), true, true);
            Move bigLeftAttack = new Move(board.getHeigth(), (-1, 0), true, true);

            Move[] rookMoves = { bigForwardAttack, bigDownwardAttack, bigLeftAttack, bigRightAttack };


            Move[] queenMoves = rookMoves.Concat(bishopMoves).ToArray();


            Move attackL0 = new Move(1, (-1, 2), true, true);
            Move attackL1 = new Move(1, (1, 2), true, true);

            Move attackL2 = new Move(1, (2, 1), true, true);
            Move attackL3 = new Move(1, (2, -1), true, true);

            Move attackL4 = new Move(1, (1, -2), true, true);
            Move attackL5 = new Move(1, (-1, -2), true, true);

            Move attackL6 = new Move(1, (-2, -1), true, true);
            Move attackL7 = new Move(1, (-2, 1), true, true);

            Move[] knightMoves = { attackL0, attackL1, attackL2, attackL3, attackL4, attackL5, attackL6, attackL7 };


            Move attackUp = new Move(1, (0, 1), true, true);
            Move attackDown = new Move(1, (0, -1), true, true);
            Move attackLeft = new Move(1, (-1, 0), true, true);
            Move attackRight = new Move(1, (1, 0), true, true);
            Move attackTopR = new Move(1, (1, 1), true, true);
            Move attackTopL = new Move(1, (-1, 1), true, true);
            Move attackBottomR = new Move(1, (1, -1), true, true);
            Move attackBottomL = new Move(1, (-1, -1), true, true);


            Move[] kingMoves = { attackUp, attackDown, attackLeft, attackRight, attackTopR, attackTopL, attackBottomR, attackBottomL };

            board.placePiece(new MoveAbles(kingMoves, 100000, "kW", true), 4, 7);

            board.placePiece(new MoveAbles(kingMoves, 100000, "kB", false), 4, 0);

            board.placePiece(new MoveAbles(queenMoves, 9, "qW", true), 3, 7);

            board.placePiece(new MoveAbles(queenMoves, 9, "qB", false), 3, 0);

            board.placePiece(new MoveAbles(knightMoves, 3, "kW", false), 1, 0);
            board.placePiece(new MoveAbles(knightMoves, 3, "kW", false), 6, 0);

            board.placePiece(new MoveAbles(knightMoves, 3, "kW", true), 1, 7);
            board.placePiece(new MoveAbles(knightMoves, 3, "kW", true), 6, 7);

            board.placePiece(new MoveAbles(bishopMoves, 3, "bW", false), 2, 0);
            board.placePiece(new MoveAbles(bishopMoves, 3, "bW", false), 5, 0);

            board.placePiece(new MoveAbles(bishopMoves, 3, "bB", true), 2, 7);
            board.placePiece(new MoveAbles(bishopMoves, 3, "bB", true), 5, 7);

            board.placePiece(new MoveAbles(rookMoves, 5, "tW", false), 0, 0);
            board.placePiece(new MoveAbles(rookMoves, 5, "tW", false), 7, 0);

            board.placePiece(new MoveAbles(rookMoves, 5, "tB", true), 0, 7);
            board.placePiece(new MoveAbles(rookMoves, 5, "tB", true), 7, 7);



            for (int x = 0; x < 8; x++)
            {
                board.placePiece(new MoveAbles(pawnMoves, 1, "pB", true), x, 6);
                board.placePiece(new MoveAbles(pawnMoves, 1, "pW", false), x, 1);
            }

            Game game = new Game(1000, board, false);



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
