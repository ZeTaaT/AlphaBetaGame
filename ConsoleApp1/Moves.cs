using NodeSpace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects {

    public class Move {

        private int range; //How many possible linked moves. Pawn(Standard move), king and knight will have range of 1(including chekers) while all other pieces will have infinite range.
        private (int, int) horizVert; //Where it will move according to it's position. Vectors
        private (int, int)[] targets = null; //What the zone of strike is. If destroyer
        private bool mover = false; //Can just move itslef
        private bool destroyer = false; //Can destroy other pieces and replace them
        private bool linkable = false;  //Can be followed by other moves. Not just itself. 
        private bool rush = false; //One time use move that can only be done from the starting position
        private (Move, (int, int))? compoundMove = null;

        public Move(int range, (int, int) horizVert, bool mover, bool destroyer, bool rush)
        {
            this.range = range;
            this.horizVert = horizVert;
            this.mover = mover;
            this.destroyer = destroyer;
            this.rush = rush;
        }
        public Move(int range, (int, int) horizVert, (int, int)[] targets, bool mover, bool destroyer, bool linkable, bool rush)
        {
            this.range = range;
            this.horizVert = horizVert;
            this.targets = targets;
            this.mover = mover;
            this.destroyer = destroyer;
            this.linkable = linkable;
            this.rush = rush;
        }
        public Move(int range, (int, int) horizVert, (int, int)[] targets, bool mover, bool destroyer, bool linkable, bool rush, (Move, (int, int)) compoundMove)
        {
            this.range = range;
            this.horizVert = horizVert;
            this.targets = targets;
            this.mover = mover;
            this.destroyer = destroyer;
            this.linkable = linkable;
            this.rush = rush;
            this.compoundMove = compoundMove;
        }
        public void reverse()
        {
            horizVert = (-horizVert.Item1, -horizVert.Item2);
            Console.WriteLine(horizVert);
        }
        public int Range
        {
            get
            {
                return range;
            }
        }
        public int getHoriz()
        {
            return horizVert.Item1;
        }
        public int getVert()
        {
            return horizVert.Item2;
        }
        public (int, int) getHorizVert()
        {
            return horizVert;
        }
        public (int, int)[] getTarget()
        {
            return targets;
        }
        public bool Mover
        {
            get
            {
                return mover;
            }
        }
        public bool Destroyer
        {
            get
            {

                return destroyer;

            }
        }
        public bool Linkable
        {
            get
            {

                return linkable;

            }
        }
        public bool Rush
        {
            get
            {
                return rush;
            }
        }
        public (Move, (int, int))? getCompMove()
        {
            return compoundMove;
        }
}
    

    public class MoveMent
    {
        private (int, int) destination,location;
        private (int, int)[] targets; //To destroy
        private float val;
        private MoveMent compoundMove = null;

        public MoveMent((int, int) location, (int, int) destination, (int, int) targets, float val)
        {
            this.destination = destination;
            this.location = location;
            this.targets = new (int, int)[] { targets };
        }
        public MoveMent((int, int) location, (int, int) destination, (int, int)[] targets, float val)
        {
            this.destination = destination;
            this.location = location;
            this.targets = targets;
            this.val = val;
        }
        public MoveMent((int, int) location, (int, int) destination, (int, int) targets, float val, MoveMent compoundMove)
        {
            this.destination = destination;
            this.location = location;
            this.targets = new (int, int)[] { targets };
            this.val = val;
            this.compoundMove = compoundMove;
        }

        public (int, int) getDestination()
        {
            return destination;
        }
        public (int, int) getLocation() 
        {
            return location;
        }
        public (int, int)[] getTargets()
        {
            return targets;
        }
        public MoveMent getCompMove()
        {
            return compoundMove;
        }
        public bool hasCompMove()
        {
            return compoundMove != null;
        }
        public float Val
        {
            get 
            {
                return val;
            }
        }
        public void setVal(float val) 
        {
            this.val = val;
        }
        
        public override string ToString()
        {
            string outTarget = "";
            foreach((int, int) target in targets)
            {
                outTarget += target.ToString();
            }
            return base.ToString() + ": " + " location :" + location.ToString() + " destination :" + destination.ToString() + " targets: " + outTarget + " value :" + val;
        }
    }
}
