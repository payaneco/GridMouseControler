using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GridMouseControler
{
    public partial class MainForm : Form
    {

        protected readonly HotKey DisplayKey;
        
        protected int ScreenNo;
        protected GridScreen CurrentScreen;

        public Rectangle CurrentRect
        {
            get
            {
                var rect = Screen.AllScreens[ScreenNo].Bounds;
                rect.Location = new Point(0, 0);
                return rect;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            DisplayKey = new HotKey(MOD_KEY.ALT, Keys.Space);
            FormBorderStyle = FormBorderStyle.None;
            Opacity = 0.3D;
            TopMost = true;
            StartPosition = FormStartPosition.Manual;
            //BackColor = Color.Lime;
            //TransparencyKey = Color.LightGray;
            ShowInTaskbar = false;
            ScreenNo = 0;

            Load += MainScreen_Load;
            Disposed += MainScreen_Disposed;
            Paint += MainScreen_Paint;
            KeyUp += MainScreen_KeyUp;
            DisplayKey.HotKeyPush += DisplayKey_HotKeyPush;

            mniClose.Click += MniClose_Click;
        }

        private void DisplayKey_HotKeyPush(object sender, EventArgs e)
        {
            Visible = !Visible;
            CurrentScreen.Reset();
        }

        private void MniClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x00000020;
        //        return cp;
        //    }
        //}

        private void MainScreen_Load(object sender, EventArgs e)
        {
            SetScreenBounds();
            CurrentScreen = new GridScreen(CurrentRect, new FullMap());
        }

        private void SetScreenBounds()
        {
            var screen = Screen.AllScreens[ScreenNo];
            Bounds = screen.Bounds;
        }


        private void MainScreen_Disposed(object sender, EventArgs e)
        {
            DisplayKey.Dispose();
        }

        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            Repaint();
        }

        private void Repaint()
        {
            var rect = CurrentRect;
            var g = CreateGraphics();
            CurrentScreen.CreateChildren(g);
            CurrentScreen.Paint(g);
        }

        private Point[] Poses = new Point[5];

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x2;
        private const int MOUSEEVENTF_LEFTUP = 0x4;

        private void ClickScreen()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private void MainScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //capsloc Hide
            //a ホールド
            //q 前のスクリーン
            //t 次のスクリーン
            //g 右クリック
            //z 一つ上へ
            //b 中クリック
            // ダブルクリック


            if (e.KeyCode == Keys.Space && !e.Alt)
            {
                CurrentScreen.AddHistory(Cursor.Position);
                Hide();
                ClickScreen();
                CurrentScreen.Reset();
                return;
            }

            if (e.KeyCode == Keys.OemPeriod && e.Shift)
            {
                ScreenNo++;
                if(Screen.AllScreens.Length <= ScreenNo)
                {
                    ScreenNo = 0;
                }
                SetScreenBounds();
                return;
            }

            if (e.KeyCode == Keys.Oemcomma && e.Shift)
            {
                ScreenNo--;
                if (ScreenNo < 0)
                {
                    ScreenNo = Screen.AllScreens.Length - 1;
                }
                SetScreenBounds();
                return;
            }
            var rect = CurrentRect;
            var g = CreateGraphics();

            var s = new KeysConverter().ConvertToString(e.KeyCode).ToLower();
            int d;
            if(int.TryParse(s, out d))
            {
                Point p;
                if(d == 0)
                {
                    //カーソルを隠す
                    Cursor.Position = new Point( CurrentScreen.Left + CurrentScreen.InnerRect.Width, CurrentScreen.Top + CurrentScreen.InnerRect.Height);
                    Hide();
                    CurrentScreen.Reset();
                    return;
                }
                else if(CurrentScreen.Root.TryGetPosition(d, out p))
                {
                    Cursor.Position = p;
                }
                return;
            }

            s = (s == "oemplus") ? ";" : s;
            Cursor.Position = CurrentScreen.Select(g, s);
            Repaint();

            //for (var x = 0; x < DIV_X; x++)
            //{
            //    for (var y = 0; y < DIV_Y; y++)
            //    {
            //        if (s == CHAR_MAP[y, x])
            //        {
            //            Poses[Depth - 1] = new Point(x, y);
            //            for (var i = 0; i < Depth; i++)
            //            {
            //                var w = rect.Width / DIV_X;
            //                var h = rect.Height / DIV_Y;
            //                var l = rect.Left + w * Poses[i].X;
            //                var t = rect.Top + h * Poses[i].Y;
            //                rect = new Rectangle(l, t, w, h);
            //            }
            //            DrawMesh(rect, g, Color.Magenta);
            //            DrawChars(rect, g);
            //            Depth++;

            //            Cursor.Position = new Point(Location.X + rect.Left + rect.Width / 2, Location.Y + rect.Top + rect.Height / 2);
            //            return;
            //        }
            //    }
            //}
        }
    }
}
