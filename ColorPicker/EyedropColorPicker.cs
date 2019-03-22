using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ColorPicker
{
    public class EventCapture : EventArgs
    {
        public EventCapture(bool isCapturing) => IsCapturing = isCapturing;
        public bool IsCapturing { get; set; }
    }

	public class EyedropColorPicker : Control
	{
		public event EventHandler SelectedColorChanged;
        public event EventHandler<EventCapture> IsCaptureChanged;

		Bitmap	m_snapshot;
		Bitmap	m_icon;
		Color	m_selectedColor;
		float  m_zoom = 4;
		public int Zoom
		{
			get { return (int)m_zoom; }
			set 
			{ 
				m_zoom = value;
				RecalcSnapshotSize();
			}
		}
		public Color SelectedColor
		{
			get { return m_selectedColor; }
			set 
			{
				if (m_selectedColor == value)
					return;
			}
		}
		public EyedropColorPicker()
		{
			this.DoubleBuffered = true;
			m_icon = new Bitmap(typeof(EyedropColorPicker), "Resources.eyedropper.bmp");
			m_icon.MakeTransparent(Color.Magenta);
		}
		RectangleF ImageRect
		{
			get
			{
				return Util.Rect(ClientRectangle);
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			RecalcSnapshotSize();
		}
		void RecalcSnapshotSize()
		{
			if (m_snapshot != null)
				m_snapshot.Dispose();
			RectangleF r = ImageRect;
			int w = (int)(Math.Floor(r.Width / Zoom));
			int h = (int)(Math.Floor(r.Height / Zoom));
			m_snapshot = new Bitmap(w, h);
		}
		void GetSnapshot()
		{
			Point p = Control.MousePosition;
			p.X -= m_snapshot.Width / 2;
			p.Y -= m_snapshot.Height / 2;

			using (Graphics dc = Graphics.FromImage(m_snapshot))
			{
				dc.CopyFromScreen(p, new Point(0,0), m_snapshot.Size);
				Refresh(); //Invalidate();
				
				PointF center = Util.Center(new RectangleF(0, 0, m_snapshot.Size.Width, m_snapshot.Size.Height));
				Color c = m_snapshot.GetPixel((int)Math.Round(center.X), (int)Math.Round(center.Y));
				if (c != m_selectedColor)
				{
					m_selectedColor = c;
					if (SelectedColorChanged != null)
						SelectedColorChanged(this, null);
				}
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Rectangle rr = ClientRectangle;

			if (m_snapshot != null)
			{
				e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
				RectangleF r = RectangleF.Empty;
				r.Width = m_snapshot.Size.Width * Zoom;
				r.Height = m_snapshot.Size.Height * Zoom;
				r.X = 0;
				r.Y = 0;
				e.Graphics.DrawImage(m_snapshot, r);

				if (isCapturing)
				{
					PointF center = Util.Center(r);
					Rectangle centerRect = new Rectangle(Util.Point(center), new Size(0, 0));
					centerRect.X -= ((int)Zoom / 2 - 1);
					centerRect.Y -= ((int)Zoom / 2 - 1);
					centerRect.Width = (int)Zoom;
					centerRect.Height = (int)Zoom;
					e.Graphics.DrawRectangle(Pens.Black, centerRect);

                    --centerRect.X;
                    --centerRect.Y;
                    centerRect.Width+=2;
                    centerRect.Height+=2;
                    e.Graphics.DrawRectangle(Pens.Orange, centerRect);
				}
				else
				{
					int offset = 3;
					e.Graphics.FillRectangle(SystemBrushes.Control, new Rectangle(new Point(offset,offset), m_icon.Size));
					e.Graphics.DrawImage(m_icon, offset, offset);
				}
			}
			Pen pen = new Pen(BackColor, 3);
			rr.Inflate(-1,-1);
			e.Graphics.DrawRectangle(pen, rr);
			Util.DrawFrame(e.Graphics, rr, 6, Color.CadetBlue);
		}

		bool isCapturing = false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if ((e.Button & MouseButtons.Left)== MouseButtons.Left)
			{
				Cursor = Cursors.Cross;
				isCapturing = true;
				Invalidate();
                IsCaptureChanged?.Invoke(this, new EventCapture(true));
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if ((e.Button & MouseButtons.Left)== MouseButtons.Left)
				GetSnapshot();
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			{
				Cursor = Cursors.Arrow;
				isCapturing = false;
				Invalidate();
			}

            IsCaptureChanged?.Invoke(this, new EventCapture(false));
        }
    }
}
