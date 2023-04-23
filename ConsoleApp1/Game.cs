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
using NodeSpace;
using Objects;

namespace GameSpace {
    public class Game {
            
        private Board board;
        private bool playerTurn;
        private float winPoint = 10000; //Should be Total points for all pieces

        public Game(int magic, Board board, bool playerTurn) {
            int magic_Num = magic;
            this.board = board;
            this.playerTurn = playerTurn;
            winPoint = calcWinPoint();
            Launcher();
        }
        public Game(int magic, Board board, bool playerTurn, float winPoint)
        {
            int magic_Num = magic;
            this.board = board;
            this.playerTurn = playerTurn;
            this.winPoint = winPoint;
            showBoard();

        }
        public void Launcher ()
        {
            bool gameEnd = false;
            Algorithm algorithm = new Algorithm(getWinPoint());
            Console.WriteLine(getWinPoint());
            while (!gameEnd)
            {
                showBoard();
                Console.ReadLine();
                bool validMove = false;
                (int, int) location = (0, 0);
                (int, int) destination = (0, 0);

                if (playerTurn) 
                {
                    List<MoveMent> moveMents = board.calcAllMoves(playerTurn);
                    showAllMoves(moveMents);
                    while (!validMove) 
                    {
                        Console.Write("Input location X axes: ");
                        int inputLocX = Int32.Parse(Console.ReadLine());
                        Console.Write("Input location Y axes: ");
                        int inputLocY = Int32.Parse(Console.ReadLine());
                        location = (inputLocX, inputLocY);
                        Console.WriteLine("Location: " + location.ToString());
                        Console.Write("Input location X axes: ");
                        int inputDestX = Int32.Parse(Console.ReadLine());
                        Console.Write("Input location Y axes: ");
                        int inputDestY = Int32.Parse(Console.ReadLine());
                        destination = (inputDestX, inputDestY);
                        Console.WriteLine("Destination: " + destination.ToString());

                        validMove = canMove(location, destination, moveMents);
                    }
                    board.movePiece(destination, location);
                    board.noRush(destination);
                }
                else
                {
                    List<MoveMent> moveMents = board.calcAllMoves(playerTurn);
                    showAllMoves(moveMents);
                    Console.ReadLine();
                    MoveMent movement = new MoveMent((0, 0), (0, 0), (0, 0), float.NegativeInfinity);
                    movement = algorithm.alphaBeta(board, movement, 3, float.NegativeInfinity, float.PositiveInfinity, false);
                    Console.WriteLine(movement.getDestination() + " " + movement.getLocation() + " " + movement.Val + " " + algorithm.getCheckMove());
                    board.movePiece(movement.getDestination(), movement.getLocation());

                    board.noRush(movement.getDestination());
                }

                playerTurn = !playerTurn;
            }

        }
        private bool canMove((int, int) location, (int, int) destination, List<MoveMent> moveMents)
        {
            foreach (MoveMent m in moveMents)
            {
                if (m.getLocation() == location && m.getDestination() == destination)
                {
                    return true;
                }
            }
            return false;
        }    
        public void showAllMoves(in List<MoveMent> moveMents) //Shows all possible moves
        {
            foreach (MoveMent m in moveMents)
            {
                Console.WriteLine(m);
            }
        }
        public void showBoard() 
        {
            for (int y = board.getLength() - 1; y >= 0; y--)
            {
                for (int x = 0; x < board.getHeigth(); x++)
                {
                    if(board.getPiece(x, y) != null)
                    {
                        Console.Write(board.getPiece(x, y).getType() + " ");
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
