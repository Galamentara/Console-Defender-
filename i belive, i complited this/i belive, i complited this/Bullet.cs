using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_belive__i_complited_this {
    internal class Bullet : Unit {
        internal bool BulletMove() {
            if (Y > 0) {
                if (sprite == '|') { Move(0, -1); }
                if (sprite == '/') {
                    if (X < 97) {
                        Move(+1, -1);
                    } else {
                        return true;
                    }
                }
                if (sprite == '\\') { 
                    if (X > 3) {
                        Move(-1, -1);
                    } else {
                        return true;
                    }
                }

                return false;
            } else {
                return true;
            }
        }
        internal Bullet(int x, char sprite) {
            X = x;
            Y = 19;
            this.sprite = sprite;
        }
    }
}
