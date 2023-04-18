﻿using System;
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
        private float postionVal, pieceVal; //Coeficients that will determine the value of moves
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

            while (!gameEnd)
            {
                showBoard();
                List<MoveMent> moveMents = calcAllMoves(false);
                showAllMoves(moveMents);
                bool validMove = false;
                (int, int) location = (0, 0);
                (int, int) destination = (0, 0);

                if (playerTurn) 
                { 
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
                }
                else
                {

                }
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
        private List<MoveMent> calcAllMoves(in bool player)
        {
            List<MoveMent> moveMents = new List<MoveMent>();
            for (int y = 0; y < board.getHeigth(); y++)
            {
                for (int x = 0; x < board.getLength(); x++)
                {
                    if (!board.isEmpty(x, y) && board.getPiece(x, y).isPlayerPiece() == player)
                    {
                        moveMents = moveMents.Concat(calcPieceMoves(x, y)).ToList();
                    }
                }
            }
            return moveMents;
        }
        public void showAllMoves(in List<MoveMent> moveMents) //Shows all possible moves
        {
            foreach (MoveMent m in moveMents)
            {
                Console.WriteLine(m);
            }
        }
        private (int, int) calcDest(in (int, int) horizVert, in (int, int) position, in int range)
        {
            (int, int) destination = (position.Item1 +
                horizVert.Item1 * range, position.Item2 + horizVert.Item2 * range);
            return destination;
        }
        private float calcMoveVal(in (int, int) destination) //Calculate the value of the Movement
        {
            int reach = 0;
            float val = 0;
            if (!(board.getTile(destination.Item1, destination.Item2).IsEmpty))
            {
                val = board.getPiece(destination.Item1, destination.Item2).getValue();
            }






            return val;
        }
        private MoveMent createMovement(in (int, int) position, in (int, int) destination, in float val)
        {
            return new MoveMent(position, destination, new (int, int)[1] { destination }, val);
        }
        private MoveMent complexMovement(in Move move, in (int, int) position, in Move[] moves, in (int, int) destination)
        {
            float val = 0;
            (int, int) target = (position.Item1 + move.getTarget().Item1,
                                  position.Item2 + move.getTarget().Item2); 
            List<(int, int)> targets = new List<(int, int)> { target };
            val += board.getPiece(target.Item1, target.Item2).getValue();





            return new MoveMent(position, destination, targets.ToArray(), val);
        }
        private MoveMent getMoveMent(Move move, (int, int) position, int range)
        {
            (int, int) dest = calcDest(move.getHorizVert(), (position.Item1, position.Item2), range);
            if (move.Linkable)
            {
                return null;
            }
            else
            {
                return createMovement((position.Item1, position.Item2), dest, calcMoveVal(dest));
            }

        }
        private List<MoveMent> calcPieceMoves(in int x, in int y)
        {
            List<MoveMent> moveMents = new List<MoveMent>();
            Move[] moves = board.getPiece(x, y).getMoves();

            foreach (Move move in moves)
            {

                bool valid = true;
                int range = 1;

                while (valid && range <= move.Range) //The move doesn't exceed it's range.
                {
                    (int, int) dest = calcDest(move.getHorizVert(), (x, y), range); //Destination.
                    valid = validMove(move, (x, y), dest); //Is move valid.

                    if (valid) //If valid, create Movement, else ignore and move on to next move.
                    {
                        if (move.Linkable)
                        {
                            moveMents.Add(complexMovement(move, (x, y), moves, dest));
                        }
                        else
                        {
                            moveMents.Add(createMovement((x, y), dest, calcMoveVal(dest))); // Create Movement.
                        }

                        if (!board.isEmpty(dest.Item1, dest.Item2))
                        {
                            valid = false;
                        }
                    }

                    

                    range++;
                }
                
            }

            return moveMents;
        }
        private bool inRange(in (int,int) dest)
        {
            return dest.Item1 <= board.getLength() - 1 && dest.Item1 >= 0 && dest.Item2 <= board.getHeigth() - 1 && dest.Item2 >= 0;
        }
        private bool validMove(in Move move, in (int, int) position, in (int, int) dest)
        {
            bool valid = true;

            if (inRange(dest))  //Not Outside the board. Within limits.
            {
                if (!(board.getTile(dest.Item1, dest.Item2).IsEmpty)) //When the destination is not empty
                {
                    if (!move.Destroyer ) //A move that doesn't eat cannot replace a piece.
                    {
                        valid = false;
                    }
                    else if (board.getPiece(dest.Item1, dest.Item2).isPlayerPiece() == 
                             board.getPiece(position.Item1, position.Item2).isPlayerPiece()) //A piece cannot take it's own allies.
                    {
                        valid = false;
                    }
                }
                else // When empty
                {
                    if (!move.Mover) //A move that eat cannot replace a piece.
                    {
                        valid = false;
                    }
                }
            }
            else
            {
                valid = false; 
            }

            return valid;
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
        }
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
