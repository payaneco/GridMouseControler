using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridMouseControler
{
    public class HandyMap : KeyMap
    {
        const int DIV_X = 3;
        const int DIV_Y = 3;
        protected readonly Point DIVISION = new Point(DIV_X, DIV_Y);
        protected readonly string[,] CHAR_MAP = new string[DIV_Y, DIV_X]
        {
                { "w", "e", "r", },
                { "s", "d", "f", },
                { "x", "c", "v", },
        };

        public override Point Division { get { return DIVISION; } }

        protected override string[,] PanelKeyMap
        {
            get
            {
                return CHAR_MAP;
            }
        }

        public override int MaxDepth { get { return 5; } }
    }
}
