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
        private (int, int) target; //What the zone of strike is.
        private bool mover = false; //Can just move itslef
        private bool destroyer = false; //Can destroy other pieces and replace them
        private bool linkable = false;  //Can be followed by other moves. Not just itself. 
        private bool rush = false; //One time use move that can only be done from the starting position


        public Move(int range, (int, int) horizVert)
        {
            this.range = range;
            this.horizVert = horizVert;
        }
        public Move(int range, (int, int) horizVert, bool mover, bool destroyer, bool rush)
        {
            this.range = range;
            this.horizVert = horizVert;
            this.mover = mover;
            this.destroyer = destroyer;
            this.rush = rush;
        }
        public Move(int range, (int, int) horizVert, (int, int) target, bool mover, bool destroyer, bool linkable)
        {
            this.range = range;
            this.horizVert = horizVert;
            this.target = target;
            this.mover = mover;
            this.destroyer = destroyer;
            this.linkable = linkable;
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
        public (int, int) getTarget()
        {
            return target;
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

    }
    public class MoveMent
    {
        private (int, int) destination,location;
        private (int, int)[] targets;
        private float val;

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
