using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorPicker
{
    public class BaseControl : Control
    {
        internal Color m_frameColor = Color.CadetBlue;

        public BaseControl()
        {
            Margin = new Padding(0);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        bool _forceFocus;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Focused || _forceFocus)
            {
                var r = BaseRectangle;
                ++r.Width;
                ++r.Height;
                ControlPaint.DrawFocusRectangle(e.Graphics, r, Color.White, m_frameColor);
            }
            else
                Util.DrawFrame(e.Graphics, BaseRectangle, 6, m_frameColor);

        }


        internal void DrawFocus()
        {
            _forceFocus = true;
            Invalidate();
        }

        internal void DrawOffFocus()
        {
            _forceFocus = false;
            Invalidate();
        }

        protected Rectangle BaseRectangle
        {
            get
            {
                var r = ClientRectangle;
                --r.Width;
                --r.Height;
                //r.Location = new Point(r.Location.X -Margin.Left
                //r.Inflate(-Margin.Left - Margin.Right, -Margin.Top - Margin.Bottom);
                return r;
            }
        }
    }
}
