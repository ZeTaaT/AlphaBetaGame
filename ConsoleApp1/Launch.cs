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
            bool playerWhite = true;
            
            Board board = new Board(8, 8);

            Move diagWUpAttackR = new Move(1, (1, 1), false, true, false);
            Move diagWUpAttackL = new Move(1, (-1, 1), false, true, false);
            Move forwardW = new Move(1, (0, 1), true, false, false);
            Move jumpW = new Move(1, (0, 2), true, false, true);

            Move diagBUpAttackR = new Move(1, (-1, -1), false, true, false);
            Move diagBUpAttackL = new Move(1, (1, -1), false, true, false);
            Move forwardB = new Move(1, (0, -1), true, false, false);
            Move jumpB = new Move(1, (0, -2), true, false, true);



            Move[] pawnMovesW = { jumpW, forwardW, diagWUpAttackR, diagWUpAttackL };
            Move[] pawnMovesB = { jumpB, forwardB, diagBUpAttackR, diagBUpAttackL };



            Move bigDiagUpAtkR = new Move(board.getHeigth(), (1, 1), true, true, false);
            Move bigDiagUpAtkL = new Move(board.getHeigth(), (-1, 1), true, true, false);
            Move bigDiagDownAtkR = new Move(board.getHeigth(), (1, -1), true, true, false);
            Move bigDiagDownAtkL = new Move(board.getHeigth(), (-1, -1), true, true, false);

            Move[] bishopMoves = { bigDiagUpAtkR, bigDiagUpAtkL, bigDiagDownAtkR, bigDiagDownAtkL };


            Move bigForwardAttack = new Move(board.getHeigth(), (0, 1), true, true, false);
            Move bigRightAttack = new Move(board.getHeigth(), (1, 0), true, true, false);
            Move bigDownwardAttack = new Move(board.getHeigth(), (0, -1), true, true, false);
            Move bigLeftAttack = new Move(board.getHeigth(), (-1, 0), true, true, false);

            Move[] rookMoves = { bigForwardAttack, bigDownwardAttack, bigLeftAttack, bigRightAttack };


            Move[] queenMoves = rookMoves.Concat(bishopMoves).ToArray();


            Move attackL0 = new Move(1, (-1, 2), true, true, false);
            Move attackL1 = new Move(1, (1, 2), true, true, false);

            Move attackL2 = new Move(1, (2, 1), true, true, false);
            Move attackL3 = new Move(1, (2, -1), true, true, false);

            Move attackL4 = new Move(1, (1, -2), true, true, false);
            Move attackL5 = new Move(1, (-1, -2), true, true, false);

            Move attackL6 = new Move(1, (-2, -1), true, true, false);
            Move attackL7 = new Move(1, (-2, 1), true, true, false);

            Move[] knightMoves = { attackL0, attackL1, attackL2, attackL3, attackL4, attackL5, attackL6, attackL7 };


            Move attackUp = new Move(1, (0, 1), true, true, false);
            Move attackDown = new Move(1, (0, -1), true, true, false);
            Move attackLeft = new Move(1, (-1, 0), true, true, false);
            Move attackRight = new Move(1, (1, 0), true, true, false);
            Move attackTopR = new Move(1, (1, 1), true, true, false);
            Move attackTopL = new Move(1, (-1, 1), true, true, false);
            Move attackBottomR = new Move(1, (1, -1), true, true, false);
            Move attackBottomL = new Move(1, (-1, -1), true, true, false);


            Move[] kingMoves = { attackUp, attackDown, attackLeft, attackRight, attackTopR, attackTopL, attackBottomR, attackBottomL };













            board.placePiece(new MoveAbles(kingMoves, 100000, "KW", playerWhite), 4, 0);

            board.placePiece(new MoveAbles(kingMoves, 100000, "KB", !playerWhite), 4, 7);

            board.placePiece(new MoveAbles(queenMoves, 9, "qW", playerWhite), 3, 0);

            board.placePiece(new MoveAbles(queenMoves, 9, "qB", !playerWhite), 3, 7);

            board.placePiece(new MoveAbles(knightMoves, 3, "kW", playerWhite), 1, 0);
            board.placePiece(new MoveAbles(knightMoves, 3, "kW", playerWhite), 6, 0);

            board.placePiece(new MoveAbles(knightMoves, 3, "kB", !playerWhite), 1, 7);
            board.placePiece(new MoveAbles(knightMoves, 3, "kB", !playerWhite), 6, 7);

            board.placePiece(new MoveAbles(bishopMoves, 3, "bW", playerWhite), 2, 0);
            board.placePiece(new MoveAbles(bishopMoves, 3, "bW", playerWhite), 5, 0);

            board.placePiece(new MoveAbles(bishopMoves, 3, "bB", !playerWhite), 2, 7);
            board.placePiece(new MoveAbles(bishopMoves, 3, "bB", !playerWhite), 5, 7);

            board.placePiece(new MoveAbles(rookMoves, 5, "tW", playerWhite), 0, 0);
            board.placePiece(new MoveAbles(rookMoves, 5, "tW", playerWhite), 7, 0);

            board.placePiece(new MoveAbles(rookMoves, 5, "tB", !playerWhite), 0, 7);
            board.placePiece(new MoveAbles(rookMoves, 5, "tB", !playerWhite), 7, 7);



            for (int x = 0; x < 8; x++)
            {
                board.placePiece(new MoveAbles(pawnMovesB, 1, "pB", !playerWhite), x, 6);
                board.placePiece(new MoveAbles(pawnMovesW, 1, "pW", playerWhite), x, 1);
            }

            Game game = new Game(1000, board, playerWhite);



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
