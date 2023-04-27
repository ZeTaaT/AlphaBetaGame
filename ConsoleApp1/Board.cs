using System;
using System.Collections.Generic;
using System.Linq;

namespace Objects
{
    public class Board
    {

        private int length, heigth;
        private Tile[,] area;
        private float postionCoef, pieceCoef; //Coeficients that will determine the value of moves

        public Board(int length, int heigth)
        {
            this.length = length;
            this.heigth = heigth;
            area = new Tile[length, heigth];
            fillBoard();
        }

        public void setCoef(float postionCoef, float pieceCoef)
        {
            this.postionCoef = postionCoef;
            this.pieceCoef = pieceCoef;
        }
        private void fillBoard()
        {
            for (int y = 0; y < heigth; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    area[x, y] = new Tile();
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
        public Tile getTile((int, int) destination)
        {
            return area[destination.Item1, destination.Item2];
        }
        public MoveAbles getPiece((int, int) pos)
        {
            return area[pos.Item1, pos.Item2].getPiece();
        }
        public void clearZones()
        {
            foreach (Tile tile in area)
            {
                tile.setZone(null);
            }
        }
        public void makeZone((int, int) dest, bool maximizingPlayer)
        {
            getTile(dest).setZone(maximizingPlayer);
        }
        public float getPlayerValue()
        {
            float piecesTotal = 0;
            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < heigth; y++)
                {
                    if (area[x, y].getPiece() != null && area[x, y].getPiece().isPlayerPiece())
                    {
                        piecesTotal += area[x, y].getPiece().getValue();
                    }
                }
            }
            return piecesTotal;

        }
        public bool allTargEmpty((int, int)[] targets, (int, int) pos)
        {
            foreach ((int, int) target in targets)
            {
                if (!getTile(calcDest(target, pos, 1)).IsEmpty)
                {
                    return false;
                }
            }

            return true;
        }
        public bool allTargFriendly((int, int)[] targets, (int, int) pos, bool player)
        {
            foreach ((int, int) target in targets)
            {
                if (getTile(calcDest(target, pos, 1)).getZone() != null && getTile(calcDest(target, pos, 1)).getZone() != player)
                {
                    return false;
                }
            }
            return true;
        }
        public bool validMove(Move move, (int, int) position, (int, int) dest)
        {
            (Move, (int, int))? compoundMove = move.getCompMove();
            if ((getPiece(position).canRush() == false && move.Rush == true))
            {
                return false;
            }

            if (compoundMove.HasValue)
            {
                (int, int) compLoc = calcDest(compoundMove.Value.Item2, position, 1);
                if (inRange(compLoc))  //Not Outside the board. Within limits.
                {
                    if (!getTile(compLoc).IsEmpty) //When the destination is not empty
                    {
                        if (getPiece(compLoc).canRush() == false && compoundMove.Value.Item1.Rush == true)
                        {
                            return false;
                        }
                        if (!allTargEmpty(move.getTarget(), position) | !allTargFriendly(move.getTarget(), position, getPiece(position).isPlayerPiece()))
                        {
                            return false;
                        }

                    }
                }
            }


            if (inRange(dest))  //Not Outside the board. Within limits.
            {
                if (move.Destroyer && !move.Mover && move.Linkable)
                {
                    if (getTile(calcDest(move.getTarget()[0], position, 1)).IsEmpty)
                    {
                        return false;
                    }
                    else if (!(getTile(dest).IsEmpty)) //When the destination is not empty
                    {
                        return false;
                    }
                    else if(!(getTile(calcDest(move.getTarget()[0], position, 1)).IsEmpty))
                    {
                        if (getPiece(calcDest(move.getTarget()[0], position, 1)).isPlayerPiece() ==
                             getPiece(position).isPlayerPiece()) //A piece cannot take it's own allies.
                        {
                            return false;
                        }
                    }

                }
                else if (!(getTile(dest).IsEmpty)) //When the destination is not empty
                {
                    if (!move.Destroyer) //A move that doesn't eat cannot replace a piece.
                    {
                        return false;
                    }
                    else if (getPiece(dest).isPlayerPiece() ==
                             getPiece(position).isPlayerPiece()) //A piece cannot take it's own allies.
                    {
                        return false;
                    }
                }
                else // When empty
                {
                    if (!move.Mover) //A move that eat cannot replace a piece.
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;

        }
        public List<MoveMent> calcAllMoves(bool player) //Board
        {
            List<MoveMent> moveMents = new List<MoveMent>();
            for (int y = 0; y < getHeigth(); y++)
            {
                for (int x = 0; x < getLength(); x++)
                {
                    if (!isEmpty(x, y) && getPiece((x, y)).isPlayerPiece() == player)
                    {
                        moveMents = moveMents.Concat(calcPieceMoves(x, y)).ToList();
                    }
                }
            }
            return moveMents;
        }
        private List<MoveMent> calcPieceMoves(int x, int y)
        {
            List<MoveMent> moveMents = new List<MoveMent>();
            Move[] moves = getPiece((x, y)).getMoves();

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
                            (int, int) target = calcDest(move.getTarget()[0], (x, y), 1);
                            float val = calcMoveVal((getPiece((x, y)).isPlayerPiece()), target);

                            moveMents.Add(createTargMovement((x, y), dest, target, val));
                        }
                        else if (move.getCompMove() != null)
                        {
                            (Move, (int, int))? compMove = move.getCompMove();
                            (int, int) compLoca = calcDest(compMove.Value.Item2, (x, y), 1);
                            (int, int) compDest = calcDest(compMove.Value.Item1.getHorizVert(), compLoca, range);
                            MoveMent compMoveMent = new MoveMent(compLoca, compDest, (0, 0), 0);
                            moveMents.Add(new MoveMent((x, y), dest, dest, calcMoveVal(getPiece((x, y)).isPlayerPiece(), dest), compMoveMent));
                        }
                        else
                        {
                            moveMents.Add(createMovement((x, y), dest, calcMoveVal(getPiece((x, y)).isPlayerPiece(), dest))); // Create Movement.
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
        public int moveAmount(bool player)
        {
            int amount = 0;
            for (int y = 0; y < getHeigth(); y++)
            {
                for (int x = 0; x < getLength(); x++)
                {
                    if (!isEmpty(x, y) && getPiece((x, y)).isPlayerPiece() == player)
                    {
                        Move[] moves = getPiece((x, y)).getMoves();

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
                                    if (player)
                                    {
                                        amount++;
                                    }
                                    else
                                    {
                                        amount--;
                                    }

                                    if (!isEmpty(dest.Item1, dest.Item2))
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

            return amount;
        }
        public MoveMent createMovement((int, int) position, (int, int) destination, float val)
        {
            return new MoveMent(position, destination, new (int, int)[1] { destination }, val);
        }
        public MoveMent createTargMovement((int, int) position, (int, int) destination, (int, int) target, float val)
        {
            return new MoveMent(position, destination, new (int, int)[1] { target }, val);
        }
        //private MoveMent complexMovement(Move move, (int, int) position, Move[] moves, (int, int) destination)
        //{
        //    float val = 0;
        //    (int, int) target = (position.Item1 + move.getTarget()[0].Item1,
        //                          position.Item2 + move.getTarget()[0].Item2);
        //    List<(int, int)> targets = new List<(int, int)> { target };
        //    val += getPiece(target).getValue();


        //    return new MoveMent(position, destination, targets.ToArray(), val);
        //}
        public float calcMoveVal(bool player, (int, int) destination) //Calculate the value of the Movement
        {
            float reach;
            float val = 0;

            if (!(getTile(destination).IsEmpty))
            {
                if (player)
                {
                    val = getPiece(destination).getValue() * pieceCoef;
                }
                else
                {
                    val = -getPiece(destination).getValue() * pieceCoef;
                }
            }

            reach = moveAmount(player) * postionCoef;

            return val + reach;
        }
        public (int, int) calcDest((int, int) horizVert, (int, int) position, int range)
        {
            (int, int) destination = (position.Item1 +
                horizVert.Item1 * range, position.Item2 + horizVert.Item2 * range);
            return destination;
        }
        public void placePiece(MoveAbles piece, int x, int y)
        {
            area[x, y].setPiece(piece);
        }
        public void movePiece((int, int) destination, (int, int) location)
        {
            MoveAbles piece = getPiece(location);
            area[location.Item1, location.Item2].setPiece(null);
            placePiece(piece, destination.Item1, destination.Item2);
        }
        public void noRush((int, int) destination)
        {
            getPiece(destination).rushed();
        }
        private bool inRange((int, int) dest)
        {
            return dest.Item1 <= getLength() - 1 && dest.Item1 >= 0 && dest.Item2 <= getHeigth() - 1 && dest.Item2 >= 0;
        }

        public bool isEmpty(int x, int y)
        {
            return area[x, y].IsEmpty;
        }
    }
    public class Tile
    {
        private MoveAbles piece = null;
        private bool? playerZone = null;

        public Tile() { }
        public bool IsEmpty
        {
            get { return piece == null; }
        }
        public void setZone(bool? playerZone)
        {
            this.playerZone = playerZone;
        }
        public bool? getZone()
        {
            return playerZone;
        }
        public MoveAbles getPiece()
        {
            return piece;
        }
        public void setPiece(MoveAbles piece)
        {
            this.piece = piece;
        }
    }
}
