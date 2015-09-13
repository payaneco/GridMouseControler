using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridMouseControler
{
    public class RootPanel: AbstractPanel
    {
        const int MAX_HISTORY = 9;

        protected Queue<Point> ClickHistory;

        /// <summary>
        /// 親パネル用コンストラクタ
        /// </summary>
        /// <param name="bounds"></param>
        public RootPanel(Rectangle bounds)
        {
            Bounds = bounds;
            ClickHistory = new Queue<Point>();
        }

        public void AddHistory(Point position)
        {
            ClickHistory.Enqueue(position);
            if(ClickHistory.Count > MAX_HISTORY)
            {
                ClickHistory.Dequeue();
            }
        }

        public bool TryGetPosition(int index, out Point position)
        {
            if(ClickHistory.Count < index)
            {
                position = new Point();
                return false;
            }
            position = ClickHistory.ToArray()[index - 1];
            return true;
        }

        /// <summary>
        /// パネルクリック履歴描画処理
        /// </summary>
        /// <param name="g"></param>
        public void Paint(Graphics g)
        {
            var pen = new Pen(Color.OrangeRed);
            var index = 1;
            foreach (var point in ClickHistory)
            {
                g.DrawLine(pen, new Point(point.X - 5, point.Y), new Point(point.X + 5, point.Y));
                g.DrawLine(pen, new Point(point.X, point.Y - 5), new Point(point.X, point.Y + 5));
                g.FillEllipse(new SolidBrush(Color.Aqua), point.X + 2, point.Y + 2, 25, 25);
                g.DrawString(index.ToString(), new Font("メイリオ", 14), new SolidBrush(Color.Navy), new PointF(point.X + 6, point.Y + 2));
                index++;
            }
        }
    }
}
