namespace ColorPicker
{
	partial class ColorWheelCtrl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.m_colorWheel = new ColorPicker.ColorWheel();
            this.m_colorBar = new ColorPicker.HSLColorSlider();
            this.SuspendLayout();
            // 
            // m_colorWheel
            // 
            this.m_colorWheel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_colorWheel.Location = new System.Drawing.Point(0, 0);
            this.m_colorWheel.Name = "m_colorWheel";
            this.m_colorWheel.Size = new System.Drawing.Size(200, 175);
            this.m_colorWheel.TabIndex = 0;
            this.m_colorWheel.Text = "colorWheel1";
            // 
            // m_colorBar
            // 
            this.m_colorBar.BarPadding = new System.Windows.Forms.Padding(12, 5, 32, 10);
            this.m_colorBar.Color1 = System.Drawing.Color.Black;
            this.m_colorBar.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.m_colorBar.Color3 = System.Drawing.Color.White;
            this.m_colorBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_colorBar.Location = new System.Drawing.Point(200, 0);
            this.m_colorBar.Name = "m_colorBar";
            this.m_colorBar.NumberOfColors = ColorPicker.ColorSlider.eNumberOfColors.Use3Colors;
            this.m_colorBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.m_colorBar.Percent = 0F;
            this.m_colorBar.RotatePointAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.m_colorBar.Size = new System.Drawing.Size(46, 175);
            this.m_colorBar.TabIndex = 1;
            this.m_colorBar.Text = "Lightness";
            this.m_colorBar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_colorBar.TextAngle = 270F;
            this.m_colorBar.ValueOrientation = ColorPicker.ColorSlider.eValueOrientation.MinToMax;
            // 
            // ColorWheelCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.m_colorWheel);
            this.Controls.Add(this.m_colorBar);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ColorWheelCtrl";
            this.Size = new System.Drawing.Size(246, 175);
            this.ResumeLayout(false);

		}

		#endregion

		private HSLColorSlider m_colorBar;
		private ColorWheel m_colorWheel;
	}
}
