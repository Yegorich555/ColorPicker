namespace ColorPicker
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.m_colorPicker = new ColorPicker.ColorPickerCtrl();
            this.SuspendLayout();
            // 
            // m_colorPicker
            // 
            this.m_colorPicker.BackColor = System.Drawing.Color.Transparent;
            this.m_colorPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_colorPicker.Location = new System.Drawing.Point(0, 0);
            this.m_colorPicker.Name = "m_colorPicker";
            this.m_colorPicker.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.m_colorPicker.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(235)))), ((int)(((byte)(215)))));
            this.m_colorPicker.Size = new System.Drawing.Size(663, 336);
            this.m_colorPicker.TabIndex = 0;
            // 
            // Main
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.ClientSize = new System.Drawing.Size(663, 336);
            this.Controls.Add(this.m_colorPicker);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(578, 224);
            this.Name = "Main";
            this.Text = "Color Picker by Yegorich_555";
            this.ResumeLayout(false);

        }

        #endregion

        private ColorPickerCtrl m_colorPicker;
    }
}

