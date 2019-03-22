using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ColorPicker
{
    public partial class Main : Form
    {
        KeyBoardHandler keyBoardHandler;
        bool isCapturingProbe;
        public Main()
        {
            InitializeComponent();

            keyBoardHandler = new KeyBoardHandler();
            keyBoardHandler.KeyDown += KeyBoardHandler_KeyDown;
            m_colorPicker.m_eyedropColorPicker.IsCaptureChanged += (object o, EventCapture e)=> isCapturingProbe = e.IsCapturing;
           
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Refresh();
            //this.Update();
        }

        private void KeyBoardHandler_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isCapturingProbe)
                return;
            switch (e.KeyData)
            {
                case Keys.Left:
                    MoveCursor(-1, 0);
                    e.Handled = true;
                    break;
                case Keys.Right:
                    MoveCursor(1, 0);
                    e.Handled = true;
                    break;
                case Keys.Up:
                    MoveCursor(0, -1);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    MoveCursor(0, 1);
                    e.Handled = true;
                    break;
            }
        }

        private void MoveCursor(int dx, int dy = 0)
        {
            SetCursorPos(Cursor.Position.X + dx, Cursor.Position.Y + dy);
        }

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);
    }
}