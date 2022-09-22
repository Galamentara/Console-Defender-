using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_belive__i_complited_this {
    internal class User : Unit {
        private int score = 0;
        private int level = 0;
        internal int hp = 3;
        private char[] sprites = { '┴', '╨', '╩' };
        private char bigSprite = ' ';
        internal int Level {
            get { return level; }
        }
        internal int Score {
            get { return score; }
        }
        internal void AddScore () { score++; }
        internal void AddHp () { hp++; }
        internal void RemoveHp () { hp--; }
        internal void ReadyToDie () { hp = 0; }
        internal void LevelUp () {
            if (level < 2) {
                level++;
                sprite = sprites[level];
            } else if (level < 5) {
                level++;
                bigSprite = sprites[level - 3];
            }
        }
        internal void Default () {
            level = 0;
            sprite = sprites[level];
        }
        internal void Left () { if (X != 7) { Move(-1, 0); } }
        internal void Right () { if (X != 92) { Move(1, 0); } }
        internal void Fire(ref List<Bullet> bullets) {

            switch (level) {
                case 0: {
                        bullets.Add(new Bullet(X, '|'));
                    }
                    break;
                case 1: {
                        bullets.Add(new Bullet(X + 1, '|'));
                        bullets.Add(new Bullet(X - 1, '|'));
                    }
                    break;
                case 2: {
                        bullets.Add(new Bullet(X + 1, '|'));
                        bullets.Add(new Bullet(X, '|'));
                        bullets.Add(new Bullet(X - 1, '|'));
                    }
                    break;
                case 3: {
                        bullets.Add(new Bullet(X + 2, '|'));
                        bullets.Add(new Bullet(X, '|'));
                        bullets.Add(new Bullet(X - 2, '|'));
                    }
                    break;
                case 4: {
                        bullets.Add(new Bullet(X + 2, '/'));
                        bullets.Add(new Bullet(X + 1, '|'));
                        bullets.Add(new Bullet(X - 1, '|'));
                        bullets.Add(new Bullet(X - 2, '\\'));
                    }
                    break;
                case 5: {
                        bullets.Add(new Bullet(X + 2, '|'));
                        bullets.Add(new Bullet(X + 1, '/'));
                        bullets.Add(new Bullet(X, '|'));
                        bullets.Add(new Bullet(X - 1, '\\'));
                        bullets.Add(new Bullet(X - 2, '|'));
                    }
                    break;
            }
        }
        internal void FireBomb(ref List<Bomb> bombs) {
            if (bombs.Count < level) {
                bombs.Add(new Bomb(X, level, '▲'));
            }
        }

            internal bool FailCheck(List<EnemyBullet> enemB) {
            for (int i = 0; i < enemB.Count; i++) {
                if (level > 2) {
                    if (enemB[i].X <= (X + 1) && enemB[i].X >= (X - 1) && (enemB[i].Y == Y)) { return true; }
                } else {
                    if ((enemB[i].X == X) && (enemB[i].Y == Y)) { return true; }
                }
            }
            return false;
        }
        internal Bonus? FailCheck(List<Bonus> B) {
            for (int i = 0; i < B.Count; i++) {
                if (level > 2) {
                    if (B[i].X <= (X + 1) && B[i].X >= (X - 1) && (B[i].Y == Y)) { return B[i]; }
                } else {
                    if ((B[i].X == X) && (B[i].Y == Y)) { return B[i]; }
                }
            }
            return null;
        }
        internal void WriteUser() {
            if (level < 3) {
                Write();
            } else if (level < 6) {
                Write(bigSprite);
            }
        }
        internal User() {
            sprite = sprites[level]; ;
            X = 50;
            Y = 19;
        }
    }
}
