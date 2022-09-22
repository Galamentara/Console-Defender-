﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace i_belive__i_complited_this {
    internal class Bonus : Unit {
        private Random rnd = new();
        private int type;
        internal int Type {
            get { return type; }
        }
        internal bool Fall() {
            if (Y == 19) {
                return true;
            } else {
                Y++;
                return false;
            }
        }
        internal Bonus () {
            type = rnd.Next(0, 3);
            switch (type) {
                case 0: {
                        sprite = '+';
                    }
                    break;
                case 1: {
                        sprite = '®';
                    }
                    break;
                case 2: {
                        sprite = '♥';
                    }
                    break;
            }
            X = rnd.Next(8, 91);
            Y = 1;
        }
    }
}
