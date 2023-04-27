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
        private MoveAbles[] transformables = null;

        public MoveAbles(Move[] moves, float value, string type, bool playerPiece) { 
            this.moves = moves;
            this.value = value;
            this.type = type;
            this.playerPiece = playerPiece;
        }
        public MoveAbles(Move[] moves, float value, string type, bool playerPiece, MoveAbles[] transformables)
        {
            this.moves = moves;
            this.value = value;
            this.type = type;
            this.playerPiece = playerPiece;
            this.transformables = transformables;
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
        public bool isTransformable() 
        {
            return transformables != null;
        }
        public void rushed()
        {
            rush = false;
        }
        public void setRush(bool rush)
        {
            this.rush = rush;
        }
        public bool canRush()
        {
            return rush;
        }
    }
}
