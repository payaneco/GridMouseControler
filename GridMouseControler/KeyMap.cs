using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridMouseControler
{
    public abstract class KeyMap
    {
        /// <summary> 再帰的な画面分割回数の最大値 </summary>
        public abstract int MaxDepth { get; }
        /// <summary> 画面分割数 </summary>
        public abstract Point Division { get; }
        /// <summary> 子パネルのキーマップ </summary>
        protected abstract string[,] PanelKeyMap { get; }
        public string GetKey(int x, int y)
        {
            return PanelKeyMap[y, x];
        }
    }
}
