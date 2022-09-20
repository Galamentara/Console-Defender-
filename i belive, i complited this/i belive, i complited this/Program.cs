using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace i_belive__i_complited_this
{
    public class Program {
        public class Unit{
            public int X = 0;
            public int Y = 0;
            public char sprite = '0';
            public void Move(int x, int y) {
                X += x;
                Y += y;
            }
            public void Write() {
                Console.SetCursorPosition(X, Y);
                Console.Write(sprite);
            }
        }
        public class Enemy : Unit {
            Random rnd = new Random();
            private int counter = 0;
            public Enemy(int x, int y, char sprite) {
                X = x;
                Y = y;
                this.sprite = sprite;
            }
            public bool HitCheck(ref List<Bullet> bullets) {
                if (bullets.Count > 0) {
                    for (int i = 0; i < bullets.Count; i++) {
                        if (bullets[i].X == X && (bullets[i].Y == Y)) {
                            bullets.RemoveAt(i);
                            return true;
                        }
                    }
                }
                return false;
            }
            public void NextPosition() {
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
            public void Fire(ref List<EnemyBullet> enemB) {
                if (rnd.Next(0, 50) == 1) {
                    enemB.Add(new EnemyBullet(X, Y, '*'));
                }
            }
        }
        public class EnemyBullet : Unit {
            public bool Fall() {
                if (Y == 19) {
                    return true;
                } else {
                    Y++;
                    return false;
                }
            }
            /*public bool Shot(List<Bullet> bullets) {
                for (int i = 0; i < bullets.Count; i++) {
                    if ((bullets[i].X == X) && ((bullets[i].Y == Y) || (bullets[i].Y == Y - 1))) {
                        bullets.RemoveAt(i);
                        return true;
                    }
                }
                return false;
            }*/
            public EnemyBullet(int x, int y, char sprite) {
                X = x;
                Y = y - 1;
                this.sprite = sprite;
            }
        }
        public class Bullet : Unit {
            public bool bulletMove() {
                if (Y > 0) {
                    Move(0, -1);
                    return false;
                } else {
                    return true;
                }
            }
            public Bullet(int x, char sprite) {
                X = x;
                Y = 19;
                this.sprite = sprite;
            }
        }
        public class User : Unit {
            public void Left() {
                if (X != 7) {
                    Move(-1, 0);
                }
            }
            public void Right() {
                if (X != 92) {
                    Move(1, 0);
                }
            }
            public void Fire(ref List<Bullet> bullets) {
                bullets.Add(new Bullet(X, '|'));
            }
            public bool FailCheck(List<EnemyBullet> enemB) {
                for (int i = 0; i < enemB.Count; i++) {
                    if ((enemB[i].X == X) && (enemB[i].Y == Y)) {
                        return true;
                    }
                }
                return false;
            }
            public User(char sprite) {
                this.sprite = sprite;
                X = 50;
                Y = 19;
            }
        }
        static void Main(string[] args){

            void Frame(List<Enemy> enem, List<Bullet> bull, List<EnemyBullet> enemBull, User user, int hp, int score) {
                foreach (Enemy e in enem) {
                    e.NextPosition();
                    e.Write();
                }
                foreach (Bullet b in bull) {
                    b.Write();
                }
                foreach (EnemyBullet eB in enemBull) {
                    eB.Write();
                }
                for (int i = 0; i < hp; i++) {
                    Console.SetCursorPosition((98 - (i + 1)), 1);
                    Console.Write('♥');
                }
                Console.SetCursorPosition(0, 1);
                Console.Write(score);
                user.Write();
            }

            Console.SetWindowSize(100, 20);
            Console.SetBufferSize(100, 20);

            while (true) {
                User user = new User('X');
                List<EnemyBullet> enemyBullets = new List<EnemyBullet>();
                List<Bullet> bullets = new List<Bullet>();
                List<Enemy> enemies = new List<Enemy>();
                for (int i = 0; i < 29; i++) {
                    for (int j = 0; j < 3; j++) {
                        enemies.Add(new Enemy((((i + 1) * 3) + 5), (((j + 1) * 3) - 2), 'W'));
                    }
                }
                int counter = 0;
                int hp = 3;
                int score = 0;
                bool gameOver = false;
                bool enemyWinn = false;
                while (!gameOver) {
                    if (Console.KeyAvailable) {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        string key = keyInfo.Key.ToString();
                        if (key == "A") {
                            user.Left();
                        }
                        if (key == "D") {
                            user.Right();
                        }
                        if (key == "W") {
                            user.Fire(ref bullets);
                        }
                    }

                    if (counter == 10) {
                        Console.Clear();
                        for (int i = 0; i < enemies.Count; i++) {
                            if (enemies[i].HitCheck(ref bullets)) {
                                enemies.RemoveAt(i);
                                score++;
                                i--;
                            } else {
                                enemies[i].Fire(ref enemyBullets);
                            }
                        }
                        for (int i = 0; i < bullets.Count; i++) {
                            if (bullets[i].bulletMove()) {
                                bullets.RemoveAt(i);
                                i--;
                            }
                        }
                        for (int i = 0; i < enemyBullets.Count; i++) {
                            /*if (enemyBullets[i].Shot(bullets)) {
                                enemyBullets.RemoveAt(i);
                                i--;
                            } else*/ if (enemyBullets[i].Fall()) {
                                enemyBullets.RemoveAt(i);
                                i--;
                            }
                        }
                        
                        foreach (Enemy e in enemies) {
                            if (e.Y == 19) {
                                enemyWinn = true;
                                hp = 1;
                            }
                        }
                        if (user.FailCheck(enemyBullets) || enemyWinn) {
                            hp--;
                            if (hp == 0) {
                               gameOver = true; 
                            } else {
                                for (int i = 0; i < 10; i++) {
                                    enemyBullets.Clear();
                                    Thread.Sleep(100);
                                    Console.SetCursorPosition(user.X, user.Y);
                                    Console.Write('+'); 
                                    Thread.Sleep(100);
                                    Console.SetCursorPosition(user.X, user.Y);
                                    Console.Write('x');

                                }
                            }
                        }

                        Console.Clear();
                        Frame(enemies, bullets, enemyBullets, user, hp, score);
                        Console.SetCursorPosition(0, 0);
                        counter = 0;
                    }
                    counter++;
                    Thread.Sleep(10);
                }
                Console.Clear();
                for (int i = 0; i < 200; i++) {
                    Console.Write("-+СМЕРТЬ-+");
                }
                while (gameOver) {
                    if (Console.KeyAvailable) {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        string key = keyInfo.Key.ToString();
                        if (key == "N") {
                            gameOver = false;
                        }
                    }
                }
                
            }
        }
    }
}