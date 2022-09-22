using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static i_belive__i_complited_this.Program;

namespace i_belive__i_complited_this {
    internal class Enemy : Unit {
        private Random rnd = new Random();
        private int counter = 0;
        private int level = 0;
        private int[] rndKey = { 100, 70, 50 };

        private char[] sprites = { '┬', '╤', '╦' };
        internal bool HitCheck(ref List<Bullet> bullets) {
            if (bullets.Count > 0) {
                for (int i = 0; i < bullets.Count; i++) {
                    if (bullets[i].X == X && bullets[i].Y == Y) {
                        if (level > 0) {
                            level--;
                            sprite = sprites[level];
                            bullets.RemoveAt(i);
                            return false;
                        } else {
                            bullets.RemoveAt(i);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        internal Bomb? HitCheck(ref List<Bomb> bombs) {
            if (bombs.Count > 0) {
                for (int i = 0; i < bombs.Count; i++) {
                    if (bombs[i].X == X && bombs[i].Y == Y) {
                        return bombs[i];
                    }
                }
            }
            return null;
        }
        internal bool WaveCheck (ref Bomb bomb) {
            bool posMaxX = bomb.X > X - bomb.Power;
            bool posMinX = bomb.X < X + bomb.Power;
            bool posMaxY = bomb.Y > Y - bomb.Power;
            bool posMinY = bomb.Y < Y + bomb.Power;

            if (posMaxX && posMinX && posMaxY && posMinY) {
                if (level > 0) {
                    level--;
                    sprite = sprites[level];
                    return false;
                } else {
                    return true;
                }
            }
            return false;
        }
        internal void NextPosition() {
            if (counter == 20) {
                Move(-1, 0);
            } else if (counter == 40) {
                Move(0, 1);
            } else if (counter == 60) {
                Move(1, 0);
            } else if (counter == 80) {
                Move(0, 1);
            }
            if (counter == 80) {
                counter = 0;
            } else {
                counter++;
            }
        }
        internal void Fire(ref List<EnemyBullet> enemB) {
            if (rnd.Next(0, rndKey[level]) == 1) {
                enemB.Add(new EnemyBullet(X, Y, '☼'));
            }
        }
        internal Enemy(int x, int y, int level) {
            X = x;
            Y = y;
            this.level = level;
            sprite = sprites[level];
        }
    }
}
