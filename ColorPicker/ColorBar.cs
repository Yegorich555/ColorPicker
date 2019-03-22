using System;
using System.Drawing;
using System.Windows.Forms;

namespace ColorPicker
{
    class ColorSlider : LabelRotate
	{
		public event EventHandler SelectedValueChanged;
        public Orientation Orientation { get; set; } = Orientation.Vertical;

        public enum ENumberOfColors
		{
			Use2Colors, 
			Use3Colors, 
		}
        public ENumberOfColors NumberOfColors { get; set; } = ENumberOfColors.Use3Colors;
        public enum EValueOrientation
		{
			MinToMax,
			MaxToMin,
		}
        public EValueOrientation ValueOrientation { get; set; } = EValueOrientation.MinToMax;
        float m_percent = 0;
		public float Percent
		{
			get { return m_percent; }
			set
			{
				// ok so it is not really percent, but a value between 0 - 1.
				if (value < 0) value = 0;
				if (value > 1) value = 1;
				if (value != m_percent)
				{
					m_percent = value;
                    SelectedValueChanged?.Invoke(this, null);
                    Invalidate();
				}

			}
		}

        public Color Color1 { get; set; } = Color.Black;
        public Color Color2 { get; set; } = Color.FromArgb(255, 127, 127, 127);
        public Color Color3 { get; set; } = Color.White;

        Padding m_barPadding = new Padding(12,5, 24, 10);
		public Padding BarPadding
		{
			get { return m_barPadding; }
			set 
			{ 
				m_barPadding = value;
				Invalidate();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			DrawColorBar(e.Graphics);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			PointF mousepoint = new PointF(e.X, e.Y);
			if (e.Button == MouseButtons.Left)
				SetPercent(mousepoint);
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			Focus();
			PointF mousepoint = new PointF(e.X, e.Y);
			if (e.Button == MouseButtons.Left)
				SetPercent(mousepoint);
		}
		protected override bool ProcessDialogKey(Keys keyData)
		{
			float percent = Percent * 100;
			int step = 0;
			if ((keyData & Keys.Up) == Keys.Up)
				step = 1;
			if ((keyData & Keys.Down) == Keys.Down)
				step = -1;
			if ((keyData & Keys.Control) == Keys.Control)
				step *= 5;
			if (step != 0)
			{
				SetPercent((float)Math.Round(percent + step));
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}
		protected virtual void SetPercent(float percent)
		{
			Percent = percent / 100;
		}
		protected virtual void SetPercent(PointF mousepoint)
		{			
			RectangleF br = BarRectangle;
			mousepoint.X += BaseRectangle.X - br.X;
			mousepoint.Y += BaseRectangle.Y - br.Y;
			Percent = GetPercentSet(BarRectangle, Orientation, mousepoint);
			Refresh();
		}

		protected Rectangle BarRectangle
		{
			get
			{
				Rectangle r = ClientRectangle;
				r.X			+= BarPadding.Left;
				r.Width		-= BarPadding.Right;
				r.Y			+= BarPadding.Top;
				r.Height	-= BarPadding.Bottom;
				return r;
			}
		}
		protected float GetPercentSet(RectangleF r, Orientation orientation, PointF mousepoint)
		{
			float percentSet = 0;
			if (orientation == Orientation.Vertical)
			{
				if (ValueOrientation == EValueOrientation.MaxToMin)
					percentSet = 1 - ((mousepoint.Y - r.Y / r.Height) / r.Height);
				else
					percentSet = mousepoint.Y / r.Height;
			}
			if (orientation == Orientation.Horizontal)
				if (ValueOrientation == EValueOrientation.MaxToMin)
					percentSet = 1 - ((mousepoint.X - r.X / r.Width) / r.Width);
				else
					percentSet = (mousepoint.X / r.Width);
			if (percentSet < 0)
				percentSet = 0;
			if (percentSet > 100)
				percentSet = 100;
			return percentSet;
		}
		protected void DrawSelector(Graphics dc, RectangleF r, Orientation orientation, float percentSet)
		{
			Pen pen = new Pen(Color.CadetBlue);
			percentSet = Math.Max(0, percentSet);
			percentSet = Math.Min(1, percentSet);
			if (orientation == Orientation.Vertical)
			{
				float selectorY = (float)Math.Floor(r.Top + (r.Height - (r.Height * percentSet)));
				if (ValueOrientation == EValueOrientation.MaxToMin)
					selectorY = (float)Math.Floor(r.Top + (r.Height - (r.Height * percentSet)));
				else
					selectorY = (float)Math.Floor(r.Top + (r.Height * percentSet));

				dc.DrawLine(pen, r.X, selectorY, r.Right, selectorY);

				Image image = SelectorImages.Image(SelectorImages.eIndexes.Right);
				float xpos = r.Right;
				float ypos = selectorY - image.Height/2;
				dc.DrawImageUnscaled(image, (int)xpos, (int)ypos);
				
				image = SelectorImages.Image(SelectorImages.eIndexes.Left);
				xpos = r.Left - image.Width;
				dc.DrawImageUnscaled(image, (int)xpos, (int)ypos);
			}
			if (orientation == Orientation.Horizontal)
			{
				float selectorX = 0;
				if (ValueOrientation == EValueOrientation.MaxToMin)
					selectorX = (float)Math.Floor(r.Left + (r.Width - (r.Width * percentSet)));
				else
					selectorX = (float)Math.Floor(r.Left + (r.Width * percentSet));

				dc.DrawLine(pen, selectorX, r.Top, selectorX, r.Bottom);

				Image image = SelectorImages.Image(SelectorImages.eIndexes.Up);
				float xpos = selectorX - image.Width/2;
				float ypos = r.Bottom;
				dc.DrawImageUnscaled(image, (int)xpos, (int)ypos);
				
				image = SelectorImages.Image(SelectorImages.eIndexes.Down);
				ypos = r.Top - image.Height;
				dc.DrawImageUnscaled(image, (int)xpos, (int)ypos);
			}
		}
		protected void DrawColorBar(Graphics dc)
		{
			RectangleF lr = BarRectangle;
			if (NumberOfColors == ENumberOfColors.Use2Colors)
				Util.Draw2ColorBar(dc, lr, Orientation, Color1, Color2);
			else
				Util.Draw3ColorBar(dc, lr, Orientation, Color1, Color2, Color3);
			DrawSelector(dc, lr, Orientation, (float)Percent);
		}
	}

	class HSLColorSlider : ColorSlider
	{
		HSLColor m_selectedColor = new HSLColor();
		public HSLColor SelectedHSLColor
		{
			get { return m_selectedColor; }
			set
			{
				if (m_selectedColor == value)
					return;
				m_selectedColor = value;
				value.Lightness = 0.5;
				Color2 = Color.FromArgb(255, value.Color);
				Percent = (float)m_selectedColor.Lightness;
				Refresh();//Invalidate(Util.Rect(BarRectangle));
			}
		}

		protected override void SetPercent(PointF mousepoint)
		{
			base.SetPercent(mousepoint);
			m_selectedColor.Lightness = Percent;
			Refresh();
		}
		protected override void SetPercent(float percent)
		{
			base.SetPercent(percent);
			m_selectedColor.Lightness = percent / 100;
			SelectedHSLColor = m_selectedColor;
		}
	}
}
