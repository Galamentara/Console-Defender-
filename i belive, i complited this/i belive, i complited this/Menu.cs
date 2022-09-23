using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_belive__i_complited_this {
    internal class Menu {
        private int menuLevel = 0;
        private int menuElement = 0;
        private int subMenu = 0;
        private bool nextLevel = false;
        private string[][] menuElements = {
            new string[] { "BONUSES", "SKILS", "STATS" },
            new string[] { "HEALTH", "AIM" },
            new string[] { "FIRE LINE", "FRIZ", "PROTECTED" },
            new string[] { "MAX HEALTH", "MAX BOMB", "POWER" }
        };
        private void BuyThis (int subM, int elemM, User user) {
            switch (subM) {
                case 1: {
                        if (elemM == 0) { user.AddHp(); }
                        if (elemM == 1) { user.AimOn(); }
                    }
                    break;
                case 2: {
                        if (elemM == 0) { /*fire line*/ }
                        if (elemM == 1) { /*friz*/ }
                        if (elemM == 2) { /*protected*/ }
                    }
                    break;
                case 3: {
                        if (elemM == 0) { user.AddMaxHp(); }
                        if (elemM == 1) { user.AddMaxBomb(); }
                        if (elemM == 2) { /*power*/ }
                    }
                    break;
            }
        }
        internal void StartMenu (User user) {

            while (!nextLevel) {
                if (Console.KeyAvailable) {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    string key = keyInfo.Key.ToString();
                    switch (key) {
                        case "W": { 
                                if (menuElement != 0) {
                                    menuElement--;
                                } else {
                                    menuElement = menuElements[subMenu].Length - 1;
                                }
                            } break;
                        case "S": {
                                if (menuElement < menuElements[subMenu].Length - 1) {
                                    menuElement++;
                                } else {
                                    menuElement = 0;
                                }
                            }
                            break;
                        case "Enter": {
                                if (menuLevel != 1) {
                                    menuLevel = 1;
                                    subMenu = menuElement + 1;
                                    menuElement = 0;
                                } else {
                                    BuyThis(subMenu, menuElement, user);
                                }
                            }
                            break;
                        case "Escape": {
                                if (menuLevel != 0) {
                                    menuLevel = 0;
                                    subMenu = 0;
                                }
                            }
                            break;
                        case "Tab": {
                                nextLevel = true;
                            }
                            break;
                    }
                    Write();
                }
            }
        }
        internal void Write() {
            Console.Clear();
            for (int i = 0; i < menuElements[subMenu].Length; i++) {
                Console.SetCursorPosition(4, 2 + i);
                Console.Write(menuElements[subMenu][i]);
            }
            Console.SetCursorPosition(3, 2 + menuElement);
            Console.Write('-');
        }

    }
}
