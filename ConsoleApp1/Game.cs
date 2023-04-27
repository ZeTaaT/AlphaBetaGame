using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MainStuff;
using Objects;

namespace GameSpace {
    public class Game {
            
        private Board board;
        private bool playerTurn;
        private float winPoint = 10000; //Should be Total points for all pieces

        public Game((float, float) magic, Board board, bool playerTurn) {
            this.board = board;
            this.playerTurn = playerTurn;
            winPoint = calcWinPoint();
            this.board.setCoef(magic.Item1, magic.Item2);
        }
        public Game((float, float) magic, Board board, bool playerTurn, float winPoint)
        {
            this.board = board;
            this.playerTurn = playerTurn;
            this.winPoint = winPoint;
            this.board.setCoef(magic.Item1, magic.Item2);
        }
        public void Launcher (int diff)
        {
            bool gameEnd = false;
            Algorithm algorithm = new Algorithm(getWinPoint());
            
            while (!gameEnd)
            {
                board.clearZones();
                showBoard(board);
                Console.ReadLine();
                bool validMove = false;

                MoveMent movement = new MoveMent((0, 0), (0, 0), (0, 0), 0);
                List<MoveMent> moveMents = board.calcAllMoves(playerTurn);
                if (moveMents.Count == 0)
                {
                    gameEnd = true;
                    Console.WriteLine("It's a tie");
                    break;
                }
                if (playerTurn)
                {
                    
                    showAllMoves(moveMents);
                    while (!validMove) 
                    {

                        Console.Write("Which move do you want to use? : ");
                        int moveNum = Int32.Parse(Console.ReadLine());

                        if(!(moveNum > moveMents.Count | 0 > moveNum))
                        {
                            validMove= true;
                            movement = moveMents.ToArray()[moveNum];
                        }
                    }
                    
                }
                else
                {
                    var timer = new Stopwatch();
                    timer.Start();

                    movement = algorithm.alphaBeta(board, movement, diff, float.NegativeInfinity, float.PositiveInfinity, false);
                    Console.WriteLine(" dest: " + movement.getDestination() + " loca: " + movement.getLocation() + " " + movement.Val + " " + algorithm.getCheckMove() + " " + algorithm.getCuts());
                    
                    timer.Stop();
                    TimeSpan timeTaken = timer.Elapsed;
                    Console.WriteLine("Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));
                }

                if (movement.getTargets() != null)
                {
                    foreach ((int, int) target in movement.getTargets())
                    {
                        board.placePiece(null, target.Item1, target.Item2);
                    }
                }
                board.movePiece(movement.getDestination(), movement.getLocation());
                Console.WriteLine(movement.getCompMove());
                if (movement.hasCompMove())
                {
                    board.movePiece(movement.getCompMove().getDestination(), movement.getCompMove().getLocation());
                }


                board.noRush(movement.getDestination());

                playerTurn = !playerTurn;
            }

        }
        public void showAllMoves(in List<MoveMent> moveMents) //Shows all possible moves
        {
            int count = 0;
            foreach (MoveMent m in moveMents)
            {
                Console.WriteLine("move " + count + " " + m);
                count++;
            }
        }
        public static void showBoard(Board board) 
        {
            for (int y = board.getLength() - 1; y >= 0; y--)
            {
                for (int x = 0; x < board.getHeigth(); x++)
                {
                    if(board.getPiece((x, y)) != null)
                    {
                        Console.Write(board.getPiece((x, y)).getType() + " ");
                    }
                    else
                    {
                        Console.Write("-- ");
                    }
                }
                Console.WriteLine();
            }
        } //Game
        private float calcWinPoint() {
            float pointage = board.getPlayerValue();
            //Depending on the game changes
            return pointage/2;
        }
        private bool loseNextRound()
        {

            return false;
        }
        public Board getBoard() { 
            return board; 
        }
        public float getWinPoint() { 
            return winPoint; 
        }
    }
}
