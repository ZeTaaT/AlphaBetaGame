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
            Console.WriteLine("Chess or chekers? :");
            string game = Console.ReadLine();
            if (game == "C")
            {
                Chess();
            }
            else if (game == "c")
            {
                Chekeres();
            }


        }
        static void Chekeres()
        {
            bool playerWhite = true;

            Board board = new Board(8, 8);

            Move diagWUpR = new Move(1, (1, 1), true, false, false);
            Move diagWUpL = new Move(1, (-1, 1), true, false, false);
            Move diagWAttackUpR = new Move(1, (2, 2), new (int, int)[] { (1, 1) }, false, true, true, false);
            Move diagWAttackUpL = new Move(1, (-2, 2), new (int, int)[] { (-1, 1) }, false, true, true, false);

            Move diagBUpR = new Move(1, (-1, -1), true, false, false);
            Move diagBUpL = new Move(1, (1, -1), true, false, false);
            Move diagBAttackUpR = new Move(1, (-2, -2), new (int, int)[] {(-1, -1)}, false, true, true, false);
            Move diagBAttackUpL = new Move(1, (2, -2), new (int, int)[] {(1, -1)}, false, true, true, false);


            Move[] chekerB = { diagBUpR, diagBUpL, diagBAttackUpL, diagBAttackUpR };
            Move[] chekerW = { diagWUpR, diagWUpL, diagWAttackUpL, diagWAttackUpR };

            Move[] queenMoves = { diagBUpL, diagBUpL, diagWUpL, diagWUpR, diagBAttackUpL, diagBAttackUpR, diagWAttackUpL, diagWAttackUpR };
            MoveAbles queenW = new MoveAbles(queenMoves, 4, "qW", playerWhite);
            MoveAbles queenB = new MoveAbles(queenMoves, 4, "qB", !playerWhite);


            for (int y = 0; y < 3; y++)
            {

                for (int x = 0; x < 4; x++)
                {
                    if(y == 1)
                    {
                        board.placePiece(new MoveAbles(chekerB, 1, "cB", !playerWhite, new MoveAbles[] { queenB }), x * 2, board.getHeigth() - 1 - y);
                        board.placePiece(new MoveAbles(chekerW, 1, "cW", playerWhite, new MoveAbles[] { queenW }), 1 + x * 2, y);
                    }
                    else
                    {
                        board.placePiece(new MoveAbles(chekerB, 1, "cB", !playerWhite, new MoveAbles[] { queenB }), 1+x * 2, board.getHeigth() - 1 - y);
                        board.placePiece(new MoveAbles(chekerW, 1, "cW", playerWhite, new MoveAbles[] { queenW }), x * 2, y);
                    }
                }
            }
            int difficulty = 0;
            while (1 > difficulty | difficulty > 10)
            {
                Console.Write("Input difficulty from 1 to 10 : ");
                difficulty = Int32.Parse(Console.ReadLine());
            }
            Game game = new Game((1, 100), board, playerWhite, 1200);
            game.Launcher(difficulty);
        }
        static void Chess() 
        {
            bool playerWhite = true;
            
            Board board = new Board(8, 8);

            Move diagWUpAttackR = new Move(1, (1, 1), false, true, false);
            Move diagWUpAttackL = new Move(1, (-1, 1), false, true, false);
            Move forwardW = new Move(1, (0, 1), true, false, false);
            Move jumpW = new Move(1, (0, 2), new (int, int)[] { (0, 1) }, true, false, false, true);

            Move diagBUpAttackR = new Move(1, (-1, -1), false, true, false);
            Move diagBUpAttackL = new Move(1, (1, -1), false, true, false);
            Move forwardB = new Move(1, (0, -1), true, false, false);
            Move jumpB = new Move(1, (0, -2), new (int, int)[] { (0, 1) }, true, false, false, true);



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

            Move rookCastL = new Move(1, (3, 0), true, false, true);
            Move rookCastR = new Move(1, (-2, 0), true, false, true);

            Move castleR = new Move(1, (2, 0), new (int, int)[] { (1,0), (2,0) },true, false, false, true, (rookCastR, (3,0)));
            Move castleL = new Move(1, (-2, 0), new (int, int)[] { (-1, 0), (-2, 0), (-3, 0) }, true, false, false, true, (rookCastL, (-4,0)));

            Move[] kingMoves = { attackUp, attackDown, attackLeft, attackRight, attackTopR, attackTopL, attackBottomR, attackBottomL, castleR, castleL };










            MoveAbles kingW = new MoveAbles(kingMoves, 100000, "KW", playerWhite);
            board.placePiece(kingW, 4, 0);

            MoveAbles kingB = new MoveAbles(kingMoves, 100000, "KB", !playerWhite);
            board.placePiece(kingB, 4, 7);

            MoveAbles queenW = new MoveAbles(queenMoves, 9, "qW", playerWhite);
            board.placePiece(queenW, 3, 0);

            MoveAbles queenB = new MoveAbles(queenMoves, 9, "qB", !playerWhite);
            board.placePiece(queenB, 3, 7);

            MoveAbles knightWL = new MoveAbles(knightMoves, 3, "kW", playerWhite);
            board.placePiece(knightWL, 1, 0);
            MoveAbles knightWR = new MoveAbles(knightMoves, 3, "kW", playerWhite);
            board.placePiece(knightWR, 6, 0);

            MoveAbles knightBL = new MoveAbles(knightMoves, 3, "kB", !playerWhite);
            board.placePiece(knightBL, 1, 7);
            MoveAbles knightBR = new MoveAbles(knightMoves, 3, "kB", !playerWhite);
            board.placePiece(knightBR, 6, 7);

            MoveAbles bishopWL = new MoveAbles(bishopMoves, 3, "bW", playerWhite);
            board.placePiece(bishopWL, 2, 0);
            MoveAbles bishopWR = new MoveAbles(bishopMoves, 3, "bW", playerWhite);
            board.placePiece(bishopWR, 5, 0);

            MoveAbles bishopBL = new MoveAbles(bishopMoves, 3, "bB", !playerWhite);
            board.placePiece(bishopBL, 2, 7);
            MoveAbles bishopBR = new MoveAbles(bishopMoves, 3, "bB", !playerWhite);
            board.placePiece(bishopBR, 5, 7);

            MoveAbles rookWL = new MoveAbles(rookMoves, 5, "tW", playerWhite);
            board.placePiece(rookWL, 0, 0);
            MoveAbles rookWR = new MoveAbles(rookMoves, 5, "tW", playerWhite);
            board.placePiece(rookWR, 7, 0);

            MoveAbles rookBL = new MoveAbles(rookMoves, 5, "tB", !playerWhite);
            board.placePiece(rookBL, 0, 7);
            MoveAbles rookBR = new MoveAbles(rookMoves, 5, "tB", !playerWhite);
            board.placePiece(rookBR, 7, 7);



            for (int x = 0; x < 8; x++)
            {
                board.placePiece(new MoveAbles(pawnMovesB, 1, "pB", !playerWhite, new MoveAbles[] { queenB, rookBL, bishopBL, knightBL }), x, 6);
                board.placePiece(new MoveAbles(pawnMovesW, 1, "pW", playerWhite, new MoveAbles[] { queenW, rookWL, bishopWL, knightWL }), x, 1);
            }

            Game game = new Game((1, 15), board, playerWhite);
            game.Launcher(3);


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
