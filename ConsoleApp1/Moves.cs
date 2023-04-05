using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects {

    public class Move {

        private int range; //How many possible linked moves. Pawn(Standard move), king and knight will have range of 1 while all other pieces will have infinite range
        private (int, int) horizVert; //Where it will move according to it's positionfdrt
        private bool teleport = false; //Can phase through pieces 

        public Move(int range, (int, int) horizVert)
        {
            this.range = range;
            this.horizVert = horizVert;

        }
        public Move(int range, (int, int) horizVert, bool tel)
        {
            this.range = range;
            this.horizVert = horizVert;
            this.teleport= tel;

        }

        public int Range 
        { 
            get 
            { 
                return range; 
            } 
        }
        public (int, int) HorizVert
        { 
            get 
            { 
                return horizVert; 
            } 
        }
        public bool Teleport
        { 
            get 
            { 
                return teleport; 
            } 
        }


    }
}
