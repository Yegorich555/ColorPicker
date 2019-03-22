using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorPicker
{
    public class ButtonCtrl : Button
    {
        Color defaultColor = DefaultBackColor;
        public ButtonCtrl()
        {
            Cursor = Cursors.Hand;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            defaultColor = BackColor;
            BackColor = ChangeBrightness(defaultColor, 15);
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = defaultColor;
            base.OnMouseLeave(e);
        }

        public static Color ChangeBrightness(Color c, sbyte dBrigth)
        {
            int red = c.R;
            int green = c.G;
            int blue = c.B;

            red += dBrigth;
            if (red > 255) red = 255;
            if (red < 0) red = 0;

            green += dBrigth;
            if (green > 255) green = 255;
            if (green < 0) green = 0;

            blue += dBrigth;
            if (blue > 255) blue = 255;
            if (blue < 0) blue = 0;

            return Color.FromArgb(c.A, red, green, blue);
        }


    }
}
