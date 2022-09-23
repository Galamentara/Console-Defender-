using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i_belive__i_complited_this {
    internal class Unit {
        private int x = 0;
        internal int X {
            get { return x; }
            set { OldX = x; x = value; }
        }
        private int y = 0;
        internal int Y {
            get { return y; }
            set { OldY = y; y = value; }
        }
        internal int OldX = 0;
        internal int OldY = 0;
        internal char sprite = '0';
        internal void Move(int x, int y) {
            X += x;
            Y += y;
        }
        internal void Write() {
            Console.SetCursorPosition(X, Y);
            Console.Write(sprite);
        }
        
    }
}
