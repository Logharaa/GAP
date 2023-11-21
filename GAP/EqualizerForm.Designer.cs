namespace GAP
{
    partial class EqualizerForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EqualizerForm));
            equalizerBand80 = new CustomControls.EqualizerBand();
            equalizerLegend = new CustomControls.EqualizerLegend();
            freq80 = new Label();
            equalizerBand250 = new CustomControls.EqualizerBand();
            freq250 = new Label();
            equalizerBand500 = new CustomControls.EqualizerBand();
            freq500 = new Label();
            equalizerBand1500 = new CustomControls.EqualizerBand();
            freq1500 = new Label();
            equalizerBand3000 = new CustomControls.EqualizerBand();
            freq3000 = new Label();
            equalizerBand5000 = new CustomControls.EqualizerBand();
            freq5000 = new Label();
            equalizerBand10000 = new CustomControls.EqualizerBand();
            freq10000 = new Label();
            SuspendLayout();
            // 
            // equalizerBand80
            // 
            equalizerBand80.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            equalizerBand80.BackColor = Color.Transparent;
            equalizerBand80.Cursor = Cursors.Hand;
            equalizerBand80.Location = new Point(115, 40);
            equalizerBand80.Name = "equalizerBand80";
            equalizerBand80.Size = new Size(75, 380);
            equalizerBand80.TabIndex = 3;
            equalizerBand80.Text = "equalizerBand80";
            equalizerBand80.ValueChanged += EqualizerBand90_ValueChanged;
            // 
            // equalizerLegend
            // 
            equalizerLegend.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            equalizerLegend.Location = new Point(27, 40);
            equalizerLegend.Name = "equalizerLegend";
            equalizerLegend.Size = new Size(75, 380);
            equalizerLegend.TabIndex = 4;
            equalizerLegend.Text = "equalizerLegend1";
            // 
            // freq80
            // 
            freq80.Anchor = AnchorStyles.Bottom;
            freq80.AutoSize = true;
            freq80.BorderStyle = BorderStyle.FixedSingle;
            freq80.Font = new Font("Inter", 10F, FontStyle.Regular, GraphicsUnit.Point);
            freq80.ForeColor = Color.FromArgb(208, 208, 208);
            freq80.Location = new Point(136, 445);
            freq80.Name = "freq80";
            freq80.Size = new Size(28, 19);
            freq80.TabIndex = 5;
            freq80.Text = "80";
            // 
            // equalizerBand250
            // 
            equalizerBand250.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            equalizerBand250.BackColor = Color.Transparent;
            equalizerBand250.Cursor = Cursors.Hand;
            equalizerBand250.Location = new Point(196, 40);
            equalizerBand250.Name = "equalizerBand250";
            equalizerBand250.Size = new Size(75, 380);
            equalizerBand250.TabIndex = 6;
            equalizerBand250.Text = "equalizerBand250";
            equalizerBand250.ValueChanged += EqualizerBand250_ValueChanged;
            // 
            // freq250
            // 
            freq250.Anchor = AnchorStyles.Bottom;
            freq250.AutoSize = true;
            freq250.BorderStyle = BorderStyle.FixedSingle;
            freq250.Font = new Font("Inter", 10F, FontStyle.Regular, GraphicsUnit.Point);
            freq250.ForeColor = Color.FromArgb(208, 208, 208);
            freq250.Location = new Point(213, 445);
            freq250.Name = "freq250";
            freq250.Size = new Size(36, 19);
            freq250.TabIndex = 7;
            freq250.Text = "250";
            // 
            // equalizerBand500
            // 
            equalizerBand500.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            equalizerBand500.BackColor = Color.Transparent;
            equalizerBand500.Cursor = Cursors.Hand;
            equalizerBand500.Location = new Point(277, 40);
            equalizerBand500.Name = "equalizerBand500";
            equalizerBand500.Size = new Size(75, 380);
            equalizerBand500.TabIndex = 8;
            equalizerBand500.Text = "equalizerBand500";
            equalizerBand500.ValueChanged += EqualizerBand500_ValueChanged;
            // 
            // freq500
            // 
            freq500.Anchor = AnchorStyles.Bottom;
            freq500.AutoSize = true;
            freq500.BorderStyle = BorderStyle.FixedSingle;
            freq500.Font = new Font("Inter", 10F, FontStyle.Regular, GraphicsUnit.Point);
            freq500.ForeColor = Color.FromArgb(208, 208, 208);
            freq500.Location = new Point(293, 445);
            freq500.Name = "freq500";
            freq500.Size = new Size(37, 19);
            freq500.TabIndex = 9;
            freq500.Text = "500";
            // 
            // equalizerBand1500
            // 
            equalizerBand1500.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            equalizerBand1500.BackColor = Color.Transparent;
            equalizerBand1500.Cursor = Cursors.Hand;
            equalizerBand1500.Location = new Point(358, 40);
            equalizerBand1500.Name = "equalizerBand1500";
            equalizerBand1500.Size = new Size(75, 380);
            equalizerBand1500.TabIndex = 10;
            equalizerBand1500.Text = "equalizerBand1500";
            equalizerBand1500.ValueChanged += EqualizerBand1500_ValueChanged;
            // 
            // freq1500
            // 
            freq1500.Anchor = AnchorStyles.Bottom;
            freq1500.AutoSize = true;
            freq1500.BorderStyle = BorderStyle.FixedSingle;
            freq1500.Font = new Font("Inter", 10F, FontStyle.Regular, GraphicsUnit.Point);
            freq1500.ForeColor = Color.FromArgb(208, 208, 208);
            freq1500.Location = new Point(370, 445);
            freq1500.Name = "freq1500";
            freq1500.Size = new Size(44, 19);
            freq1500.TabIndex = 11;
            freq1500.Text = "1500";
            // 
            // equalizerBand3000
            // 
            equalizerBand3000.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            equalizerBand3000.BackColor = Color.Transparent;
            equalizerBand3000.Cursor = Cursors.Hand;
            equalizerBand3000.Location = new Point(439, 40);
            equalizerBand3000.Name = "equalizerBand3000";
            equalizerBand3000.Size = new Size(75, 380);
            equalizerBand3000.TabIndex = 12;
            equalizerBand3000.Text = "equalizerBand3000";
            equalizerBand3000.ValueChanged += EqualizerBand3000_ValueChanged;
            // 
            // freq3000
            // 
            freq3000.Anchor = AnchorStyles.Bottom;
            freq3000.AutoSize = true;
            freq3000.BorderStyle = BorderStyle.FixedSingle;
            freq3000.Font = new Font("Inter", 10F, FontStyle.Regular, GraphicsUnit.Point);
            freq3000.ForeColor = Color.FromArgb(208, 208, 208);
            freq3000.Location = new Point(450, 445);
            freq3000.Name = "freq3000";
            freq3000.Size = new Size(46, 19);
            freq3000.TabIndex = 13;
            freq3000.Text = "3000";
            // 
            // equalizerBand5000
            // 
            equalizerBand5000.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            equalizerBand5000.BackColor = Color.Transparent;
            equalizerBand5000.Cursor = Cursors.Hand;
            equalizerBand5000.Location = new Point(520, 40);
            equalizerBand5000.Name = "equalizerBand5000";
            equalizerBand5000.Size = new Size(75, 380);
            equalizerBand5000.TabIndex = 14;
            equalizerBand5000.Text = "equalizerBand5000";
            equalizerBand5000.ValueChanged += EqualizerBand5000_ValueChanged;
            // 
            // freq5000
            // 
            freq5000.Anchor = AnchorStyles.Bottom;
            freq5000.AutoSize = true;
            freq5000.BorderStyle = BorderStyle.FixedSingle;
            freq5000.Font = new Font("Inter", 10F, FontStyle.Regular, GraphicsUnit.Point);
            freq5000.ForeColor = Color.FromArgb(208, 208, 208);
            freq5000.Location = new Point(531, 445);
            freq5000.Name = "freq5000";
            freq5000.Size = new Size(46, 19);
            freq5000.TabIndex = 15;
            freq5000.Text = "5000";
            // 
            // equalizerBand10000
            // 
            equalizerBand10000.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            equalizerBand10000.BackColor = Color.Transparent;
            equalizerBand10000.Cursor = Cursors.Hand;
            equalizerBand10000.Location = new Point(601, 40);
            equalizerBand10000.Name = "equalizerBand10000";
            equalizerBand10000.Size = new Size(75, 380);
            equalizerBand10000.TabIndex = 16;
            equalizerBand10000.Text = "equalizerBand10000";
            equalizerBand10000.ValueChanged += EqualizerBand8000_ValueChanged;
            // 
            // freq10000
            // 
            freq10000.Anchor = AnchorStyles.Bottom;
            freq10000.AutoSize = true;
            freq10000.BorderStyle = BorderStyle.FixedSingle;
            freq10000.Font = new Font("Inter", 10F, FontStyle.Regular, GraphicsUnit.Point);
            freq10000.ForeColor = Color.FromArgb(208, 208, 208);
            freq10000.Location = new Point(612, 445);
            freq10000.Name = "freq10000";
            freq10000.Size = new Size(53, 19);
            freq10000.TabIndex = 17;
            freq10000.Text = "10000";
            // 
            // EqualizerForm
            // 
            AutoScaleDimensions = new SizeF(10F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 24, 24);
            ClientSize = new Size(734, 511);
            Controls.Add(freq10000);
            Controls.Add(equalizerBand10000);
            Controls.Add(freq5000);
            Controls.Add(equalizerBand5000);
            Controls.Add(freq3000);
            Controls.Add(equalizerBand3000);
            Controls.Add(freq1500);
            Controls.Add(equalizerBand1500);
            Controls.Add(freq500);
            Controls.Add(equalizerBand500);
            Controls.Add(freq250);
            Controls.Add(equalizerBand250);
            Controls.Add(freq80);
            Controls.Add(equalizerLegend);
            Controls.Add(equalizerBand80);
            Font = new Font("Inter", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.FromArgb(184, 184, 184);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximumSize = new Size(750, 1000);
            MinimumSize = new Size(750, 550);
            Name = "EqualizerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GAP Equalizer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private CustomControls.EqualizerBand equalizerBand80;
        private CustomControls.EqualizerLegend equalizerLegend;
        private Label freq80;
        private CustomControls.EqualizerBand equalizerBand250;
        private Label freq250;
        private CustomControls.EqualizerBand equalizerBand500;
        private Label freq500;
        private CustomControls.EqualizerBand equalizerBand1500;
        private Label freq1500;
        private CustomControls.EqualizerBand equalizerBand3000;
        private Label freq3000;
        private CustomControls.EqualizerBand equalizerBand5000;
        private Label freq5000;
        private CustomControls.EqualizerBand equalizerBand10000;
        private Label freq10000;
    }
}