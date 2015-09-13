using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridMouseControler
{
    /// <summary>
    /// スクリーンごとに個別情報を保持するクラス
    /// </summary>
    public class GridScreen
    {
        /// <summary>
        /// スクリーンの画面領域
        /// </summary>
        protected Rectangle Bounds;
        /// <summary>
        /// 現在フォーカスの当たっているパネル
        /// </summary>
        protected AbstractPanel ActivePanel;
        /// <summary>
        /// スクリーン内で使うKeyMapは常に一つ
        /// </summary>
        protected KeyMap Map;

        /// <summary>
        /// スクリーンの左端座標
        /// </summary>
        public int Left { get { return Bounds.Left; } }
        /// <summary>
        /// スクリーンの上端座標
        /// </summary>
        public int Top { get { return Bounds.Top; } }
        /// <summary>
        /// スクリーンの内部領域を示す矩形
        /// </summary>
        public Rectangle InnerRect { get { return new Rectangle(new Point(0, 0), Bounds.Size); } }
        /// <summary>
        /// 画面全体を覆うルートパネル
        /// </summary>
        public RootPanel Root { get; private set; }

        public GridScreen(Rectangle bounds, KeyMap map)
        {
            Bounds = bounds;
            Root = new RootPanel(InnerRect);
            ActivePanel = Root;
            Map = map;
        }

        /// <summary>
        /// ルートパネルの子パネルを作成
        /// </summary>
        /// <param name="g"></param>
        public void CreateChildren(Graphics g)
        {
            Root.CreateChildren(g, Map);
        }

        /// <summary>
        /// パネル描画
        /// </summary>
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            Paint(g, ActivePanel, null, true);
            if (!ActivePanel.HasParent)
            {
                Root.Paint(g);
            }
        }

        /// <summary>
        /// 再帰的に描画
        /// </summary>
        /// <param name="g"></param>
        /// <param name="panel"></param>
        protected void Paint(Graphics g, AbstractPanel panel, AbstractPanel ignorePanel, bool isCharacterVisible)
        {
            panel.PaintChildren(g, ignorePanel, isCharacterVisible);
            if (panel.HasParent)
            {
                Paint(g, panel.Parent, panel, false);
            }
        }

        public Point Select(Graphics g, string key)
        {
            if (ActivePanel.DicChildren.ContainsKey(key))
            {
                ActivePanel = ActivePanel.DicChildren[key];
                ActivePanel.CreateChildren(g, Map);
            }
            var point = ActivePanel.CursorPoint;
            return new Point
            {
                X = point.X + Left,
                Y = point.Y + Top,
            };
        }

        public void Reset()
        {
            ActivePanel = Root;
        }

        public void AddHistory(Point position)
        {
            Root.AddHistory(position);
        }
    }
}
