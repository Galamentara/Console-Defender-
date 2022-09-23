using Microsoft.VisualBasic;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static System.Reflection.Metadata.BlobBuilder;

namespace i_belive__i_complited_this
{
    public class Program {
        static void Main(){

            static void Frame(List<Enemy> enem, List<Bullet> bull, List<EnemyBullet> enemBull, User user, List<Bonus> bonuses, List<Bomb> bombs) {

                for (int i = 0; i < enem.Count; i++) { enem[i].Write(); }
                
                for (int i = 0; i < bull.Count; i++) { bull[i].Write(); }
                for (int i = 0; i < bombs.Count; i++) { bombs[i].Write(); }
                for (int i = 0; i < enemBull.Count; i++) { enemBull[i].Write(); }
                for (int i = 0; i < bonuses.Count; i++) { bonuses[i].Write(); }
                
                Console.SetCursorPosition((98 - (user.hp)), 1);
                for (int i = 0; i < user.hp; i++) { Console.Write('♥'); }

                Console.SetCursorPosition(1, 20);
                for (int i = 1; i < 98; i++) { Console.Write('─'); }

                Console.SetCursorPosition(0, 1);
                Console.Write(user.Score);

                user.WriteUser();
            }

            Console.SetWindowSize(100, 21);
            Console.SetBufferSize(100, 21);

            int level = 0;
            int[,] enemyLine = new int[10,2] {
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 1, 1 },
                { 2, 1 },
                { 3, 1 },
                { 4, 1 },
                { 1, 2 },
                { 2, 2 },
                { 3, 2 },
            };
            User user = new();
            while (true) {
                level++;
                if (level == 10) {
                    Console.Clear();

                    Console.SetCursorPosition(45, 10);
                    Console.Write("You Winner");
                    Console.SetCursorPosition(0, 0);
                    bool endGame = true;
                    while (endGame) {
                        if (Console.KeyAvailable) {
                            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                            string key = keyInfo.Key.ToString();
                            if (key == "N") {
                                Console.Clear();
                                endGame = false;
                                level = 1;
                                user = new User();
                            }
                        }
                    }
                }
                Console.SetCursorPosition(45, 10);
                Console.Write($"LEVEL - {level}");
                Thread.Sleep(1000);
                List<EnemyBullet> enemyBullets = new();
                List<Bullet> bullets = new();
                List<Bonus> bonuses = new();
                List<Bomb> bombs = new();
                List<Enemy> enemies = new();
                for (int i = 0; i < enemyLine[level - 1, 0]; i++) {
                    for (int j = 0; j < 29; j++) {
                        int x = ((j + 1) * 3) + 5 + (i % 2);
                        int y = ((i + 1) * (3 - (enemyLine[level - 1, 0] / 3))) - (3 - (enemyLine[level - 1, 0] / 3)) + 1;
                        enemies.Add(new Enemy(x, y, enemyLine[level - 1, 1]));
                    }
                }
                int counter = 0;
                bool gameOver = false;
                bool gameWin = false;
                bool enemyWinn = false;
                Random rnd = new();
                while (!gameOver && !gameWin) {
                    if (Console.KeyAvailable) {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        string key = keyInfo.Key.ToString();
                        switch (key) {
                            case "A": { user.Left(); } break;
                            case "D": { user.Right(); } break;
                            case "W": { user.Fire(ref bullets); } break;
                            case "R": { gameOver = true; } break;
                            case "Z": { enemies.Clear(); } break;
                            case "Spacebar": { user.FireBomb(ref bombs); } break;
                            case "Q": { user.LevelUp(); } break;
                            case "E": { user.Default(); } break;
                        }
                    }

                    if (counter == 10) {
                        for (int i = 0; i < enemies.Count; i++) {
                            if (enemies[i].HitCheck(ref bullets)) {
                                enemies.RemoveAt(i);
                                user.AddScore();
                                i--;
                            }
                        }
                        for (int i = 0; i < enemies.Count; i++) {
                            Bomb? hitBomb = enemies[i].HitCheck(ref bombs);
                            if (hitBomb != null) {
                                for (int j = 0; j < enemies.Count; j++) {
                                    if (enemies[j].WaveCheck(ref hitBomb)) {
                                        enemies.RemoveAt(j);
                                        user.AddScore();
                                        j--;
                                    }
                                }
                                bombs.Remove(hitBomb);
                            }
                        }
                        for (int i = 0; i < bullets.Count; i++) {
                            if (bullets[i].BulletMove()) { bullets.RemoveAt(i); i--; }
                        }
                        for (int i = 0; i < enemyBullets.Count; i++) {
                            if (enemyBullets[i].Fall()) { enemyBullets.RemoveAt(i); i--; }
                        }
                        for (int i = 0; i < bonuses.Count; i++) {
                            if (bonuses[i].Fall()) { bonuses.RemoveAt(i); i--; }
                        }
                        for (int i = 0; i < bombs.Count; i++) {
                            if (bombs[i].BombMove()) { bombs.RemoveAt(i); i--; }
                        }
                        for (int i = 0; i < enemies.Count; i++) {
                            if (enemies[i].Y == 19) { enemyWinn = true; user.hp = 1; }
                            enemies[i].Fire(ref enemyBullets);
                            enemies[i].NextPosition();
                        }
                        if (rnd.Next(0, 20) == 1) { bonuses.Add(new Bonus()); }

                        if (bonuses.Count > 0) {
                            Bonus? bns = user.FailCheck(bonuses);
                            if (bns != null) {
                                switch (bns.Type) {
                                    case 0: { user.LevelUp(); } break;
                                    case 1: { enemyBullets.Clear(); } break;
                                    case 2: { user.AddHp(); } break;
                                    case 3: { user.AddScore(10); } break;
                                }
                            }
                        }
                        
                        if (user.FailCheck(enemyBullets) || enemyWinn) {
                            user.RemoveHp();
                            if (user.hp == 0) {
                               gameOver = true; 
                            } else {
                                for (int i = 0; i < 10; i++) {
                                    Console.Clear();
                                    enemyBullets.Clear();
                                    Thread.Sleep(100);
                                    Console.SetCursorPosition(user.X, user.Y);
                                    Console.Write('+'); 
                                    Thread.Sleep(100);
                                    Console.SetCursorPosition(user.X, user.Y);
                                    Console.Write('x');

                                }
                            }
                        } else if (enemies.Count == 0 && !gameOver) { gameWin = true; }

                        Console.Clear();
                        Frame(enemies, bullets, enemyBullets, user, bonuses, bombs);
                        Console.SetCursorPosition(0, 0);
                        counter = 0;
                    }
                    if (gameWin && !gameOver) {
                        Menu menu = new();
                        menu.StartMenu(user);
                    }
                    counter++;
                    Thread.Sleep(10);
                }
                if (gameOver) {
                    Console.Clear();
                    for (int i = 0; i < 200; i++) { Console.Write("-+СМЕРТЬ-+"); }
                    Console.SetCursorPosition(0, 0);
                    while (gameOver) {
                        if (Console.KeyAvailable) {
                            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                            string key = keyInfo.Key.ToString();
                            if (key == "N") {
                                Console.Clear();
                                gameOver = false;
                            }
                        }
                    }
                }
            }
        }
    }
}