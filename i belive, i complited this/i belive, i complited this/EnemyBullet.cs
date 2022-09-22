using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_belive__i_complited_this {
    internal class EnemyBullet : Unit {
        internal bool Fall() {
            if (Y == 19) {
                return true;
            } else {
                Y++;
                return false;
            }
        }
        internal EnemyBullet(int x, int y, char sprite) {
            X = x;
            Y = y - 1;
            this.sprite = sprite;
        }
    }
}
