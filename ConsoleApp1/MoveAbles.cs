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
        private char type; //Board representation

        public MoveAbles(Move[] moves, float value, char type) { 
            this.moves = moves;
            this.value = value;
            this.type = type;
            
        }

        public Move[] getMoves() 
        { 
            return moves;
        }
        public float getValue() 
        { 
            return value; 
        }
        public char getType() 
        { 
            return type; 
        }

    }
}
