using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridMouseControler
{
    public class GridPanel : AbstractPanel
    {
        const string FONT_FAMILY = "メイリオ";
        public static readonly SolidBrush DarkBrush = new SolidBrush(Color.Black);
        public static readonly SolidBrush BrightBrush = new SolidBrush(Color.Silver);
        /// <summary>
        /// 座標配列のインデックス
        /// </summary>
        public Point MapPosition;
        //計算上のフォントサイズ
        protected int _FontSize;

        /// <summary>
        /// 表示用フォントサイズ
        /// </summary>
        public int FontSize { get { return Math.Max(_FontSize, 4); } }
        /// <summary>
        /// 背景色
        /// </summary>
        public SolidBrush Background { get { return IsOdd ? DarkBrush : BrightBrush; } }
        /// <summary>
        /// テキストの色
        /// </summary>
        public SolidBrush Foreground { get { return !IsOdd ? DarkBrush : BrightBrush; } }
        /// <summary>
        /// テキストに表示する文字
        /// </summary>
        public string KeyCharacter { get; private set; }
        /// <summary>
        /// 市松模様の偶数パネルかどうかを返す
        /// </summary>
        public bool IsOdd
        {
            get
            {
                var d = (MapPosition.X + MapPosition.Y) % 2;
                return (d == 1);
            }
        }
        public Rectangle DrawBounds
        {
            get
            {
                return new Rectangle
                {
                    X = Bounds.X + MapPosition.X,
                    Y = Bounds.Y + MapPosition.Y,
                    Width = Bounds.Width + 1,
                    Height = Bounds.Height + 1,
                };
            }
        }

        /// <summary>
        /// カーソルの位置からフォントの大きさに応じて位置調整した座標
        /// </summary>
        public PointF CharacterPoint
        {
            get
            {
                return new Point
                {
                    X = CursorPoint.X - (FontSize / 2),
                    Y = CursorPoint.Y - FontSize,
                };
            }
        }
        /// <summary>
        /// 文字のフォント
        /// </summary>
        public Font CharacterFont { get { return new Font(FONT_FAMILY, FontSize); } }

        /// <summary>
        /// 子パネル用コンストラクタ
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="g"></param>
        /// <param name="division"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="key"></param>
        public GridPanel(AbstractPanel parent, Graphics g, Point division, int x, int y, string key)
        {
            Parent = parent;
            MapPosition = new Point(x, y);
            var width = parent.Bounds.Width / division.X;
            var height = parent.Bounds.Height / division.Y;
            Bounds = new Rectangle
            {
                X = parent.Bounds.X + width * x,
                Y = parent.Bounds.Y + height * y,
                Width = width,
                Height = height,
            };
            _FontSize = CalcFontSize(g);
            KeyCharacter = key;
        }

        /// <summary>
        /// 定型の様式でフォントサイズを計算して返す
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        protected int CalcFontSize(Graphics g)
        {
            return CalcFontSize("Z", Bounds.Size, FONT_FAMILY, g);
        }

        /// <summary>
        /// ざっくりフォントサイズを計算して返す
        /// </summary>
        /// <param name="str"></param>
        /// <param name="size"></param>
        /// <param name="fontName"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        /// <remarks>http://bbs.wankuma.com/index.cgi?mode=al2&namber=32673&KLOG=57</remarks>
        private int CalcFontSize(string str, Size size, string fontName, Graphics g)
        {
            using (var f = new Font(fontName, 1))
            {
                var s = g.MeasureString(str, f);
                int a = (int)(size.Width / s.Width);
                int b = (int)(size.Height / s.Height);
                return (a < b) ? a : b;
            }
        }

        /// <summary>
        /// パネル描画処理
        /// </summary>
        /// <param name="g"></param>
        /// <param name="isCharacterVisible"></param>
        public void Paint(Graphics g, bool isCharacterVisible)
        {
            g.FillRectangle(Background, DrawBounds);
            if (isCharacterVisible)
            {
                g.DrawString(KeyCharacter, CharacterFont, Foreground, CharacterPoint);
            }
        }
    }
}
