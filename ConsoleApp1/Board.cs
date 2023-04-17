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
        private float postionVal, pieceVal; //Coeficients that will determine the value of moves
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
        public Tile getTile(int x, int y)
        {
            return area[x, y];
        }
        public MoveAbles getPiece(int x, int y)
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
        public void placePiece(MoveAbles piece, int x, int y) 
        {
            area[x,y].setPiece(piece);
        }
        public void movePiece((int, int) destination, (int,int) location)
        {
            MoveAbles piece = getPiece(location.Item1, location.Item2);
            area[location.Item1, location.Item2].setPiece(null);
            placePiece(piece, destination.Item1, destination.Item2);    
        }
        public bool isEmpty(int x, int y) 
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
        public void setPiece(MoveAbles piece)
        {
            this.piece = piece;
        }
    }
}
