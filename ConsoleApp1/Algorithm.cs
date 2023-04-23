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
                    Console.WriteLine("NOT DEPTH 0");
                }
                movement.setVal(movement.Val + root.moveAmount(!maximizingPlayer));
                checkMove++;
                return movement;
            }

            if (maximizingPlayer)  // for Player  
            {
                MoveMent maxEva = null;
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

                                                moveMent.setVal(eva.Val);

                                                Console.WriteLine(maxEva?.Val + " max " + eva.Val + " " + moveMent.Val);

                                                maxEva = mathEva(maxEva, moveMent, true); // Highest evaluated pointage
                                                alpha = Math.Max(alpha, maxEva.Val); // Highest valid evaluated pointage in whole tree

                                                board.movePiece((x, y), dest);
                                            }
                                            else
                                            {
                                                MoveAbles piece = board.getPiece(dest.Item1, dest.Item2);
                                                board.movePiece(dest, (x, y));
                                                eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, false);

                                                moveMent.setVal(eva.Val);

                                                Console.WriteLine(maxEva?.Val + " maxS " + eva.Val + " " + moveMent.Val);

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
                MoveMent minEva = null;

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

                                                moveMent.setVal(eva.Val);

                                                Console.WriteLine(minEva?.Val + " min " + eva.Val + " " + moveMent.Val);
                                                minEva = mathEva(minEva, moveMent, false); // Highest evaluated pointage
                                                beta = Math.Min(beta, minEva.Val); // Highest valid evaluated pointage in whole tree

                                                board.movePiece((x, y), dest);
                                            }
                                            else
                                            {
                                                MoveAbles piece = board.getPiece(dest.Item1, dest.Item2);

                                                board.movePiece(dest, (x, y));
                                                eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, true);

                                                moveMent.setVal(eva.Val);

                                                Console.WriteLine(minEva?.Val + " minS " + eva.Val + " " + moveMent.Val);
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
            if(move1== null)
            {
                return move2;
            }
            else if(move2== null)
            {
                return move1;
            }
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
