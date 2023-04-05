using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainStuff;
using NodeSpace;
using Objects;

namespace GameSpace {
    public class Game {

        private Board board;
        private float winPoint = 10000; //Should be Total points for all pieces

        public Game(int magic, Board board) {
            int magic_Num = magic;
            winPoint = calcWinPoint(board);

        }
        private float calcWinPoint(Board board) {
            float pointage = 0;

            return pointage;
        }







        private float valueCalc(Move move) {





            return 0;
        }
    }
}
