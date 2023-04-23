using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GameSpace;
using NodeSpace;
using Objects;

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
            minPoint= -winPoint;
            maxPoint = winPoint;
        }
        public MoveMent alphaBeta(Board root, MoveMent movement, int depth, float alpha, float beta, bool maximizingPlayer)
        {
            MoveMent eva; // The value of the move (Need to implement method of calculating the value of the move)
            if (depth == 0 | (maximizingPlayer && movement.Val > maxPoint) | (!maximizingPlayer && movement.Val < minPoint)) //change
            {
                if((maximizingPlayer && movement.Val > maxPoint) | (!maximizingPlayer && movement.Val < minPoint))
                {
                    Console.WriteLine("AAAA");
                }
                checkMove++;
                return movement;
            }

            if (maximizingPlayer)  // for Player  
            {
                MoveMent maxEva = new MoveMent((0, 0), (0, 0), (-1, -1), negInf);
                //HAVE BOARD WITH A LIST OF PIECES AND IT'S POSITIONS for better efficiency
                for (int y = 0; y < root.getHeigth(); y++)
                {
                    for (int x = 0; x < root.getLength(); x++)
                    {
                        if (!root.isEmpty(x, y) && root.getPiece(x, y).isPlayerPiece() == maximizingPlayer)
                        {
                            Move[] moves = root.getPiece(x, y).getMoves();

                            foreach (Move move in moves)
                            {

                                bool valid = true;
                                int range = 1;

                                while (valid && range <= move.Range) //The move doesn't exceed it's range.
                                {
                                    (int, int) dest = root.calcDest(move.getHorizVert(), (x, y), range); //Destination.
                                    valid = root.validMove(move, (x, y), dest); //Is move valid.

                                    if (valid) //If valid, create Movement, else ignore and move on to next move.
                                    {
                                        if (move.Linkable)
                                        {

                                        }
                                        else //Movement
                                        {
                                            Board board = root;
                                            float val = board.calcMoveVal(maximizingPlayer, dest) + movement.Val;
                                            MoveMent moveMent = board.createMovement((x, y), dest, val);

                                            if (board.isEmpty(dest.Item1, dest.Item2))
                                            {
                                                board.movePiece(dest, (x, y));
                                                eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, false);

                                                moveMent.setVal(val + eva.Val);

                                                maxEva = mathEva(maxEva, moveMent, true); // Highest evaluated pointage
                                                alpha = Math.Max(alpha, maxEva.Val); // Highest valid evaluated pointage in whole tree

                                                board.movePiece((x, y), dest);
                                            }
                                            else
                                            {
                                                MoveAbles piece = board.getPiece(dest.Item1, dest.Item2);
                                                board.movePiece(dest, (x, y));
                                                eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, false);

                                                moveMent.setVal(val + eva.Val);

                                                maxEva = mathEva(maxEva, moveMent, true); // Highest evaluated pointage
                                                alpha = Math.Max(alpha, maxEva.Val); // Highest valid evaluated pointage in whole tree

                                                board.movePiece((x, y), dest);
                                                board.placePiece(piece, dest.Item1, dest.Item2);
                                            }


                                            if (beta <= alpha)
                                            {
                                                //Console.WriteLine(" loca:" + (x, y) + " dest:" + dest + " alpha: " + alpha + " beta :" + beta + " val:" + val);
                                                break;
                                            }
                                        }

                                        if (!root.isEmpty(dest.Item1, dest.Item2))
                                        {
                                            valid = false;
                                        }
                                    }

                                    range++;
                                }

                            }
                        }
                    }
                }


                return maxEva;

            }
            else
            {
                MoveMent minEva = new MoveMent((0, 0), (0, 0), (-1, -1), posInf);

                for (int y = 0; y < root.getHeigth(); y++)
                {
                    for (int x = 0; x < root.getLength(); x++)
                    {
                        if (!root.isEmpty(x, y) && root.getPiece(x, y).isPlayerPiece() == maximizingPlayer)
                        {
                            Move[] moves = root.getPiece(x, y).getMoves();

                            foreach (Move move in moves)
                            {

                                bool valid = true;
                                int range = 1;

                                while (valid && range <= move.Range) //The move doesn't exceed it's range.
                                {
                                    (int, int) dest = root.calcDest(move.getHorizVert(), (x, y), range); //Destination.
                                    valid = root.validMove(move, (x, y), dest); //Is move valid.

                                    if (valid) //If valid, create Movement, else ignore and move on to next move.
                                    {
                                        if (move.Linkable)
                                        {

                                        }
                                        else //Movement
                                        {
                                            Board board = root;
                                            float val = (-board.calcMoveVal(maximizingPlayer, dest)) + movement.Val;
                                            MoveMent moveMent = board.createMovement((x, y), dest, val);

                                            if (board.isEmpty(dest.Item1, dest.Item2))
                                            {
                                                board.movePiece(dest, (x, y));
                                                eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, true);

                                                moveMent.setVal(val + eva.Val);

                                                minEva = mathEva(minEva, moveMent, false); // Highest evaluated pointage
                                                beta = Math.Min(beta, minEva.Val); // Highest valid evaluated pointage in whole tree

                                                board.movePiece((x, y), dest);
                                            }
                                            else
                                            {
                                                MoveAbles piece = board.getPiece(dest.Item1, dest.Item2);
                                                board.movePiece(dest, (x, y));
                                                eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, true);

                                                moveMent.setVal(val + eva.Val);

                                                minEva = mathEva(minEva, moveMent, false); // Highest evaluated pointage
                                                beta = Math.Min(beta, minEva.Val); // Highest valid evaluated pointage in whole tree

                                                board.movePiece((x, y), dest);
                                                board.placePiece(piece, dest.Item1, dest.Item2);
                                            }
                                            if (beta <= alpha)
                                            {

                                                //Console.WriteLine(" loca:" + (x, y) + " dest:" + dest + " alpha: " + alpha + " beta :" + beta + " val:" + val);
                                                break;
                                            }
                                        }

                                        if (!root.isEmpty(dest.Item1, dest.Item2))
                                        {
                                            valid = false;
                                        }
                                    }

                                    range++;
                                }

                            }
                        }
                    }
                }
                return minEva;
            }
        }
        private MoveMent mathEva(MoveMent move1, MoveMent move2, bool max)
        {
            if (max) {
                if (move1.Val >= move2.Val) {
                    return move1;
                } else {
                    return move2;
                }
            } else {
                if (move1.Val >= move2.Val)
                {
                    return move2;
                }
                else
                {
                    return move1;
                }
            }

        }
        
        public int getCheckMove()
        {
            return checkMove;
        }
    }
}
