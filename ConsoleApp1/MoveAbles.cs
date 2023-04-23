using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{

    //Example of a moveable....Chess piece
    public class MoveAbles
    {
        private Move[] moves; //What moves it has
        private float value; //How much a piece is worth
        private string type; //Board representation
        private bool playerPiece; //Is it a player or the bot
        private bool rush = true;

        public MoveAbles(Move[] moves, float value, string type, bool playerPiece) { 
            this.moves = moves;
            this.value = value;
            this.type = type;
            this.playerPiece = playerPiece;
        }

        private Move[] reverseMoves(Move[] moves)
        {
            Move[] allMoves = new Move[moves.Length];
            for (int i = 0; i < moves.Length; i++)
            {
                moves[i].reverse();
                allMoves[i] = moves[i];
            }
            return allMoves;
        }
        public Move[] getMoves() 
        { 
            return moves;
        }
        public float getValue() 
        { 
            return value; 
        }
        public string getType() 
        { 
            return type; 
        }
        public bool isPlayerPiece()
        {
            return playerPiece;
        }
        public void rushed()
        {
            rush = false;
        }
        public bool canRush()
        {
            return rush;
        }
    }

}
