using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GameSpace;
using Objects;

namespace MainStuff
{
    public class Algorithm
    {

        private float maxPoint = 100; //Extreme cutting
        private float minPoint = -100; //Extreme cutting
        private int checkMove = 0;
        private int cuts = 0;

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
            if (depth == 0 | (movement.Val > maxPoint) | (movement.Val < minPoint)) //change
            {
                movement.setVal(movement.Val + root.moveAmount(!maximizingPlayer));
                return movement;
            }
            if (maximizingPlayer)  // for Player  
            {
                MoveMent maxEva = new MoveMent((0, 0), (0, 0), (0, 0), float.NegativeInfinity);
                //HAVE BOARD WITH A LIST OF PIECES AND IT'S POSITIONS for better efficiency
                for (int y = 0; y < root.getHeigth(); y++)
                {
                    for (int x = 0; x < root.getLength(); x++)
                    {
                        if (!root.isEmpty(x, y) && root.getPiece((x, y)).isPlayerPiece() == maximizingPlayer)
                        {
                            Move[] moves = root.getPiece((x, y)).getMoves();

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
                                        checkMove++;
                                        if (move.Linkable)
                                        {
                                            Board board = root;
                                            (int, int) target = board.calcDest(move.getTarget()[0], (x,y), 1);
                                            float val = board.calcMoveVal(maximizingPlayer, target) + movement.Val;

                                            MoveMent moveMent = board.createTargMovement((x,y), dest, target, val);
                                            bool rush = board.getPiece((x, y)).canRush();

                                            MoveAbles piece = board.getPiece(target);
                                            board.movePiece(dest, (x, y));

                                            board.placePiece(null, target.Item1, target.Item2);

                                            eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, false);

                                            moveMent.setVal(eva.Val);

                                            //Console.WriteLine(maxEva?.Val + " maxS " + eva.Val + " " + moveMent.Val + " alpha:" + alpha + " beta:" + beta);

                                            maxEva = mathEva(maxEva, moveMent, true); // Highest evaluated pointage
                                            alpha = Math.Max(alpha, maxEva.Val); // Highest valid evaluated pointage in whole tree

                                            board.movePiece((x, y), dest);
                                            board.placePiece(piece, target.Item1, target.Item2);
                                            board.getPiece((x, y)).setRush(rush);
                                        }
                                        else //Movement
                                        {
                                            Board board = root;
                                            float val = board.calcMoveVal(maximizingPlayer, dest) + movement.Val;
                                            MoveMent moveMent = board.createMovement((x, y), dest, val);
                                            bool rush = board.getPiece((x, y)).canRush();

                                            board.getPiece((x, y)).setRush(rush);
                                            if (board.isEmpty(dest.Item1, dest.Item2))
                                            {
                                                if (move.getCompMove().HasValue)
                                                {
                                                    (Move, (int, int))? compMove = move.getCompMove();
                                                    (int, int) locComp = board.calcDest(compMove.Value.Item2, (x, y), 1);
                                                    (int, int) destComp = board.calcDest(compMove.Value.Item1.getHorizVert(), locComp, 1);
                                                    float compVal = board.calcMoveVal(maximizingPlayer, destComp);
                                                    moveMent.setVal(val+compVal);

                                                    board.movePiece(dest, (x, y));
                                                    board.movePiece(destComp, locComp);

                                                    eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, false);

                                                    moveMent.setVal(eva.Val);

                                                    //Console.WriteLine(maxEva?.Val + " max " + eva.Val + " " + moveMent.Val + " alpha:" + alpha + " beta:" + beta);

                                                    maxEva = mathEva(maxEva, moveMent, true); // Highest evaluated pointage
                                                    alpha = Math.Max(alpha, maxEva.Val); // Highest valid evaluated pointage in whole tree


                                                    board.movePiece((x, y), dest);
                                                    board.movePiece(locComp, destComp);
                                                }
                                                else
                                                {
                                                    board.getPiece((x, y)).setRush(rush);
                                                    board.movePiece(dest, (x, y));

                                                    eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, false);



                                                    moveMent.setVal(eva.Val);

                                                    //Console.WriteLine(maxEva?.Val + " max " + eva.Val + " " + moveMent.Val + " alpha:" + alpha + " beta:" + beta);

                                                    maxEva = mathEva(maxEva, moveMent, true); // Highest evaluated pointage
                                                    alpha = Math.Max(alpha, maxEva.Val); // Highest valid evaluated pointage in whole tree

                                                    board.movePiece((x, y), dest);

                                                }
                                            }
                                            else
                                            {
                                                MoveAbles piece = board.getPiece(dest);
                                                board.movePiece(dest, (x, y));

                                                eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, false);

                                                moveMent.setVal(eva.Val);

                                                //Console.WriteLine(maxEva?.Val + " maxS " + eva.Val + " " + moveMent.Val + " alpha:" + alpha + " beta:" + beta);

                                                maxEva = mathEva(maxEva, moveMent, true); // Highest evaluated pointage
                                                alpha = Math.Max(alpha, maxEva.Val); // Highest valid evaluated pointage in whole tree

                                                board.movePiece((x, y), dest);
                                                board.placePiece(piece, dest.Item1, dest.Item2);
                                            }

                                            board.getPiece((x, y)).setRush(rush);
                                        }

                                        if (beta < alpha)
                                        {
                                            cuts++;
                                            //Console.WriteLine(" loca:" + (x, y) + " dest:" + dest + " alpha: " + alpha + " beta :" + beta + " val:" + val);
                                            return maxEva;
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
                MoveMent minEva = new MoveMent((0, 0), (0, 0), (0, 0), float.PositiveInfinity); 

                for (int y = 0; y < root.getHeigth(); y++)
                {
                    for (int x = 0; x < root.getLength(); x++)
                    {
                        if (!root.isEmpty(x, y) && root.getPiece((x, y)).isPlayerPiece() == maximizingPlayer)
                        {
                            Move[] moves = root.getPiece((x, y)).getMoves();

                            foreach (Move move in moves)
                            {

                                bool valid = true;
                                int range = 1;

                                while (valid && range <= move.Range) //The move doesn't exceed it's range.
                                {
                                    (int, int) dest = root.calcDest(move.getHorizVert(), (x, y), range); //Destination.

                                    root.getPiece((x, y)).rushed();

                                    valid = root.validMove(move, (x, y), dest); //Is move valid.

                                    if (valid) //If valid, create Movement, else ignore and move on to next move.
                                    {
                                        if (move.Linkable)
                                        {
                                            Board board = root;
                                            (int, int) target = board.calcDest(move.getTarget()[0], (x, y), 1);
                                            float val = board.calcMoveVal(maximizingPlayer, target) + movement.Val;

                                            MoveMent moveMent = board.createTargMovement((x, y), dest, target, val);

                                            bool rush = board.getPiece((x, y)).canRush();

                                            MoveAbles piece = board.getPiece(target);
                                            board.movePiece(dest, (x, y));
                                            board.placePiece(null, target.Item1, target.Item2);
                                            
                                            eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, false);

                                            moveMent.setVal(eva.Val);

                                            //Console.WriteLine(maxEva?.Val + " maxS " + eva.Val + " " + moveMent.Val + " alpha:" + alpha + " beta:" + beta);

                                            minEva = mathEva(minEva, moveMent, false); // Highest evaluated pointage
                                            alpha = Math.Max(alpha, minEva.Val); // Highest valid evaluated pointage in whole tree

                                            board.movePiece((x, y), dest);
                                            board.placePiece(piece, target.Item1, target.Item2);

                                            board.getPiece((x, y)).setRush(rush);
                                        }
                                        else //Movement
                                        {
                                            Board board = root;
                                            float val = (board.calcMoveVal(maximizingPlayer, dest)) + movement.Val;
                                            MoveMent moveMent = board.createMovement((x, y), dest, val);
                                            bool rush = board.getPiece((x, y)).canRush();

                                            root.makeZone(dest, maximizingPlayer);

                                            if (board.isEmpty(dest.Item1, dest.Item2))
                                            {
                                                if (move.getCompMove().HasValue)
                                                {

                                                    (Move, (int, int))? compMove = move.getCompMove();
                                                    (int, int) locComp = board.calcDest(compMove.Value.Item2, (x, y), 1);
                                                    (int, int) destComp = board.calcDest(compMove.Value.Item1.getHorizVert(), locComp, 1);
                                                    float compVal = board.calcMoveVal(maximizingPlayer, destComp);
                                                    moveMent.setVal(val + compVal);

                                                    board.movePiece(dest, (x, y));
                                                    board.movePiece(destComp, locComp);

                                                    eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, true);

                                                    moveMent.setVal(eva.Val);
                                                    

                                                    //Console.WriteLine(minEva?.Val + " min " + eva.Val + " " + moveMent.Val + " beta:" + beta + " alpha:" + alpha);
                                                    minEva = mathEva(minEva, moveMent, false); // Highest evaluated pointage
                                                    beta = Math.Min(beta, minEva.Val); // Highest valid evaluated pointage in whole tree

                                                    board.movePiece((x, y), dest);
                                                    board.movePiece(locComp, destComp);
                                                }
                                                else
                                                {
                                                    board.movePiece(dest, (x, y));

                                                    eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, true);

                                                    moveMent.setVal(eva.Val);

                                                    //Console.WriteLine(minEva?.Val + " min " + eva.Val + " " + moveMent.Val + " beta:" + beta + " alpha:" + alpha);
                                                    minEva = mathEva(minEva, moveMent, false); // Highest evaluated pointage
                                                    beta = Math.Min(beta, minEva.Val); // Highest valid evaluated pointage in whole tree

                                                    board.movePiece((x, y), dest);
                                                }
                                            }
                                            else
                                            {
                                                MoveAbles piece = board.getPiece(dest);
                                                board.movePiece(dest, (x, y));

                                                eva = alphaBeta(board, moveMent, depth - 1, alpha, beta, true);

                                                moveMent.setVal(eva.Val);

                                                //Console.WriteLine(minEva?.Val + " minS " + eva.Val + " " + moveMent.Val + " beta:" + beta + " alpha:" + alpha);
                                                minEva = mathEva(minEva, moveMent, false); // Highest evaluated pointage
                                                beta = Math.Min(beta, minEva.Val); // Highest valid evaluated pointage in whole tree

                                                board.movePiece((x, y), dest);
                                                board.placePiece(piece, dest.Item1, dest.Item2);
                                            }

                                            board.getPiece((x, y)).setRush(rush);

                                            if (beta < alpha)
                                            {
                                                cuts++;
                                                //Console.WriteLine(" loca:" + (x, y) + " dest:" + dest + " beta :" + beta + " alpha: " + alpha  + " val:" + val);
                                                return minEva;
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
            if (move1.Val == 0)
            {
                return move2;
            }
            else if(move2.Val == 0)
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
        public int getCuts()
        {
            return cuts;
        }
    }
}
