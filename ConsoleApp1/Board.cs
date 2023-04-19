using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Objects {
    public class Board {

        private int length, heigth;
        private Tile[,] area;


        public Board(int length, int heigth) {
            this.length = length;   
            this.heigth = heigth;
            area = new Tile[length,heigth];
            fillBoard();
        }

        private void fillBoard()
        {
            for(int y = 0; y < heigth; y++)
            {
                for(int x = 0; x < length; x++)
                {
                    area[x,y] = new Tile();
                }
            }
        }
        public int getLength()
        {
            return length;
        }
        public int getHeigth()
        {
            return heigth;
        }
        public Tile getTile(in int x, in int y)
        {
            return area[x, y];
        }
        public MoveAbles getPiece(in int x, in int y)
        {
            return area[x,y].getPiece();
        }
        public float getPlayerValue()
        {
            float piecesTotal = 0;
            for(int x = 0; x < length; x++)
            {
                for(int y = 0; y < heigth; y++)
                {

                    if (area[x, y].getPiece() != null && area[x, y].getPiece().isPlayerPiece())
                    {
                        Console.WriteLine("Piece total " + piecesTotal);
                        piecesTotal += area[x, y].getPiece().getValue();
                    }
                }
            }
            return piecesTotal;

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
        private bool validMove(in Move move, in (int, int) position, in (int, int) dest)
        {
            bool valid = true;

            if (inRange(dest))  //Not Outside the board. Within limits.
            {
                if (!(getTile(dest.Item1, dest.Item2).IsEmpty)) //When the destination is not empty
                {
                    if (!move.Destroyer) //A move that doesn't eat cannot replace a piece.
                    {
                        valid = false;
                    }
                    else if (getPiece(dest.Item1, dest.Item2).isPlayerPiece() ==
                             getPiece(position.Item1, position.Item2).isPlayerPiece()) //A piece cannot take it's own allies.
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

        public List<MoveMent> calcAllMoves(in bool player) //Board
        {
            List<MoveMent> moveMents = new List<MoveMent>();
            for (int y = 0; y < getHeigth(); y++)
            {
                for (int x = 0; x < getLength(); x++)
                {
                    if (!isEmpty(x, y) && getPiece(x, y).isPlayerPiece() == player)
                    {
                        moveMents = moveMents.Concat(calcPieceMoves(x, y)).ToList();
                    }
                }
            }
            return moveMents;
        }
        private List<MoveMent> calcPieceMoves(in int x, in int y)
        {
            List<MoveMent> moveMents = new List<MoveMent>();
            Move[] moves = getPiece(x, y).getMoves();

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

                        if (!isEmpty(dest.Item1, dest.Item2))
                        {
                            valid = false;
                        }
                    }



                    range++;
                }

            }

            return moveMents;
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
            val += getPiece(target.Item1, target.Item2).getValue();





            return new MoveMent(position, destination, targets.ToArray(), val);
        }
        private float calcMoveVal(in (int, int) destination) //Calculate the value of the Movement
        {
            int reach = 0;
            float val = 0;
            if (!(getTile(destination.Item1, destination.Item2).IsEmpty))
            {
                val = getPiece(destination.Item1, destination.Item2).getValue();
            }






            return val;
        }
        private (int, int) calcDest(in (int, int) horizVert, in (int, int) position, in int range)
        {
            (int, int) destination = (position.Item1 +
                horizVert.Item1 * range, position.Item2 + horizVert.Item2 * range);
            return destination;
        }
        public void placePiece(in MoveAbles piece, in int x, in int y) 
        {
            area[x,y].setPiece(piece);
        }
        public void movePiece(in (int, int) destination, in (int,int) location)
        {
            MoveAbles piece = getPiece(location.Item1, location.Item2);
            area[location.Item1, location.Item2].setPiece(null);
            placePiece(piece, destination.Item1, destination.Item2);    
        }
        private bool inRange(in (int, int) dest)
        {
            return dest.Item1 <= getLength() - 1 && dest.Item1 >= 0 && dest.Item2 <= getHeigth() - 1 && dest.Item2 >= 0;
        }

        public bool isEmpty(in int x, in int y) 
        {
            return area[x,y].IsEmpty;
        }
    }
    public class Tile
    {
        private MoveAbles piece;

        public Tile()
        {
            piece = null;
        }
        public bool IsEmpty
        {
            get { return piece == null; }
        }
        public MoveAbles getPiece()
        {
            return piece;
        }
        public void setPiece(in MoveAbles piece)
        {
            this.piece = piece;
        }
    }
}
