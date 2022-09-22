using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_belive__i_complited_this {
    internal class Bomb : Unit {
        private int power = 4;
        internal int Power {
            get { return power; }
            set { power = value; }
        }

        internal bool BombMove() {
            if (Y > 0) {
                Move(0, -1);
                return false;
            } else {
                return true;
            }
        }
        internal Bomb(int x, int power, char sprite) {
            X = x;
            Y = 19;
            this.power = power + 4;
            this.sprite = sprite;
        }
    }
}
