using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridMouseControler
{
    public abstract class AbstractPanel
    {
        /// <summary>
        /// 親パネル(存在しない場合はnull)
        /// </summary>
        public AbstractPanel Parent { get; protected set; }
        /// <summary>
        /// パネル自体の座標・サイズ
        /// </summary>
        public Rectangle Bounds { get; set; }
        /// <summary>
        /// 子パネル
        /// </summary>
        public Dictionary<string, GridPanel> DicChildren { get; protected set; }
        /// <summary>
        /// 親パネルの有無
        /// </summary>
        public bool HasParent { get { return (Parent != null); } }
        /// <summary>
        /// パネルの中央の座標
        /// </summary>
        public Point CursorPoint
        {
            get
            {
                return new Point
                {
                    X = Bounds.Left + (Bounds.Width / 2),
                    Y = Bounds.Top + (Bounds.Height / 2),
                };
            }
        }

        /// <summary>
        /// ignorePanelを除いた子パネルをすべて描画
        /// </summary>
        /// <param name="g"></param>
        /// <param name="ignorePanel"></param>
        /// <param name="isCharacterVisible"></param>
        /// <remarks>
        /// 最下部のパネルから描画するため、ignorePanelを除かないと描画済みのパネルも上書きしてしまう。
        /// なお、再帰的に描画すると指数関数的にオブジェクトが増えるので、必要なもののみ描画する。
        /// </remarks>
        public void PaintChildren(Graphics g, AbstractPanel ignorePanel, bool isCharacterVisible)
        {
            foreach (var child in DicChildren.Values)
            {
                if (ignorePanel != null && child.Bounds.Equals(ignorePanel.Bounds))
                {
                    continue;
                }
                child.Paint(g, isCharacterVisible);
            }
        }

        /// <summary>
        /// パネルを分割して子パネルをディクショナリに追加
        /// </summary>
        /// <param name="g"></param>
        /// <param name="map"></param>
        public void CreateChildren(Graphics g, KeyMap map)
        {
            DicChildren = new Dictionary<string, GridPanel>();
            for (var x = 0; x < map.Division.X; x++)
            {
                for (var y = 0; y < map.Division.Y; y++)
                {
                    var key = map.GetKey(x, y);
                    var panel = new GridPanel(this, g, map.Division, x, y, key);
                    DicChildren.Add(key, panel);
                }
            }
        }
    }
}
