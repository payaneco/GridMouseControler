using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridMouseControler
{
    public class FullMap : KeyMap
    {
        const int DIV_X = 5;
        const int DIV_Y = 5;
        protected readonly Point DIVISION = new Point(DIV_X, DIV_Y);
        protected readonly string[,] CHAR_MAP = new string[DIV_Y, DIV_X]
        {
                { "q", "w", "e", "r", "t", },
                { "a", "s", "d", "f", "g", },
                { "z", "x", "c", "v", "b", },
                { "y", "u", "i", "o", "p", },
                { "h", "j", "k", "l", ";", },
        };

        public override Point Division { get { return DIVISION; } }

        protected override string[,] PanelKeyMap
        {
            get
            {
                return CHAR_MAP;
            }
        }

        public override int MaxDepth { get { return 4; } }
    }
}
