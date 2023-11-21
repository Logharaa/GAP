using GAP.CustomControls;

namespace GAP
{
    partial class AudioPlayerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioPlayerForm));
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openAudioFileMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripMenuItem2 = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            audioFileName = new Label();
            openAudioFileDialog = new OpenFileDialog();
            audioSliderTimer = new System.Windows.Forms.Timer(components);
            playerButtonsPanel = new RoundedPanel();
            volumeSlider = new HorizontalSlider();
            volumeButton = new CircularImageButton();
            equalizerButton = new Button();
            forwardButton = new CircularImageButton();
            playPauseButton = new CircularImageButton();
            rewindButton = new CircularImageButton();
            totalTime = new Label();
            currentTime = new Label();
            audioSlider = new HorizontalSlider();
            peakMeterLeftChannel = new PeakMeter();
            peakMeterRightChannel = new PeakMeter();
            spectrumAnalyzer = new SpectrumAnalyzer();
            menuStrip.SuspendLayout();
            playerButtonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.BackColor = Color.FromArgb(46, 46, 46);
            menuStrip.Font = new Font("Inter", 9F, FontStyle.Regular, GraphicsUnit.Point);
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip.Location = new Point(0, 1);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(6, 4, 0, 4);
            menuStrip.Size = new Size(984, 33);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.BackColor = Color.FromArgb(46, 46, 46);
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openAudioFileMenuItem, toolStripSeparator1, toolStripMenuItem2 });
            fileToolStripMenuItem.ForeColor = Color.White;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Padding = new Padding(8, 3, 8, 3);
            fileToolStripMenuItem.Size = new Size(47, 25);
            fileToolStripMenuItem.Text = "&File";
            // 
            // openAudioFileMenuItem
            // 
            openAudioFileMenuItem.ForeColor = Color.White;
            openAudioFileMenuItem.Image = Properties.Resources.audio_file;
            openAudioFileMenuItem.Name = "openAudioFileMenuItem";
            openAudioFileMenuItem.Size = new Size(117, 26);
            openAudioFileMenuItem.Text = "&Open...";
            openAudioFileMenuItem.Click += OpenFileMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(114, 6);
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.ForeColor = Color.White;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(117, 26);
            toolStripMenuItem2.Text = "E&xit";
            toolStripMenuItem2.Click += ExitMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.ForeColor = Color.White;
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Padding = new Padding(8, 3, 8, 3);
            aboutToolStripMenuItem.Size = new Size(60, 25);
            aboutToolStripMenuItem.Text = "&About";
            aboutToolStripMenuItem.Click += AboutMenuItem_Click;
            // 
            // audioFileName
            // 
            audioFileName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            audioFileName.AutoSize = true;
            audioFileName.Font = new Font("Inter SemiBold", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            audioFileName.ForeColor = Color.FromArgb(78, 78, 78);
            audioFileName.Location = new Point(42, 407);
            audioFileName.Name = "audioFileName";
            audioFileName.Size = new Size(258, 33);
            audioFileName.TabIndex = 3;
            audioFileName.Text = "No audio selected.";
            // 
            // openAudioFileDialog
            // 
            openAudioFileDialog.Filter = "Audio files|*.mp3;*.wav;*.aiff;*.wma";
            // 
            // audioSliderTimer
            // 
            audioSliderTimer.Interval = 1000;
            audioSliderTimer.Tick += AudioSliderTimer_Tick;
            // 
            // playerButtonsPanel
            // 
            playerButtonsPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            playerButtonsPanel.BackColor = Color.Transparent;
            playerButtonsPanel.Controls.Add(volumeSlider);
            playerButtonsPanel.Controls.Add(volumeButton);
            playerButtonsPanel.Controls.Add(equalizerButton);
            playerButtonsPanel.Controls.Add(forwardButton);
            playerButtonsPanel.Controls.Add(playPauseButton);
            playerButtonsPanel.Controls.Add(rewindButton);
            playerButtonsPanel.Controls.Add(totalTime);
            playerButtonsPanel.Controls.Add(currentTime);
            playerButtonsPanel.Controls.Add(audioSlider);
            playerButtonsPanel.Location = new Point(42, 473);
            playerButtonsPanel.Name = "playerButtonsPanel";
            playerButtonsPanel.Size = new Size(900, 196);
            playerButtonsPanel.TabIndex = 4;
            // 
            // volumeSlider
            // 
            volumeSlider.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            volumeSlider.BackColor = Color.Transparent;
            volumeSlider.Cursor = Cursors.Hand;
            volumeSlider.KnobRadius = 10;
            volumeSlider.Location = new Point(714, 115);
            volumeSlider.Maximum = 100;
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Size = new Size(151, 23);
            volumeSlider.SliderBarHeight = 6;
            volumeSlider.TabIndex = 8;
            volumeSlider.Text = "volumeSlider";
            volumeSlider.Value = 100;
            volumeSlider.ValueChanged += VolumeSlider_ValueChanged;
            // 
            // volumeButton
            // 
            volumeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            volumeButton.BackColor = Color.Transparent;
            volumeButton.ButtonDefaultColor = Color.FromArgb(48, 48, 48);
            volumeButton.ButtonHoveredColor = Color.FromArgb(48, 48, 48);
            volumeButton.ButtonImage = Properties.Resources.volume_on;
            volumeButton.ButtonPressedColor = Color.FromArgb(48, 48, 48);
            volumeButton.Cursor = Cursors.Hand;
            volumeButton.Location = new Point(674, 106);
            volumeButton.Name = "volumeButton";
            volumeButton.Size = new Size(40, 40);
            volumeButton.TabIndex = 7;
            volumeButton.Text = "volumeButton";
            volumeButton.MouseUp += VolumeButton_MouseUp;
            // 
            // equalizerButton
            // 
            equalizerButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            equalizerButton.Cursor = Cursors.Hand;
            equalizerButton.FlatAppearance.BorderSize = 2;
            equalizerButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(38, 38, 38);
            equalizerButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(68, 68, 68);
            equalizerButton.FlatStyle = FlatStyle.Flat;
            equalizerButton.Font = new Font("Inter SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            equalizerButton.ForeColor = Color.FromArgb(208, 208, 208);
            equalizerButton.Location = new Point(281, 106);
            equalizerButton.Name = "equalizerButton";
            equalizerButton.Size = new Size(60, 40);
            equalizerButton.TabIndex = 6;
            equalizerButton.Text = "EQ";
            equalizerButton.UseVisualStyleBackColor = true;
            equalizerButton.Click += EqualizerButton_Click;
            // 
            // forwardButton
            // 
            forwardButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            forwardButton.BackColor = Color.Transparent;
            forwardButton.ButtonHoveredColor = Color.FromArgb(255, 255, 255);
            forwardButton.ButtonImage = Properties.Resources.forward10sec;
            forwardButton.ButtonPressedColor = Color.FromArgb(128, 128, 128);
            forwardButton.Cursor = Cursors.Hand;
            forwardButton.Location = new Point(191, 101);
            forwardButton.Name = "forwardButton";
            forwardButton.Size = new Size(50, 50);
            forwardButton.TabIndex = 5;
            forwardButton.Text = "circularImageButton1";
            forwardButton.MouseUp += ForwardButton_MouseUp;
            // 
            // playPauseButton
            // 
            playPauseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            playPauseButton.BackColor = Color.Transparent;
            playPauseButton.ButtonHoveredColor = Color.FromArgb(255, 255, 255);
            playPauseButton.ButtonImage = Properties.Resources.play_arrow;
            playPauseButton.ButtonImageSize = new Size(50, 50);
            playPauseButton.ButtonPressedColor = Color.FromArgb(128, 128, 128);
            playPauseButton.Cursor = Cursors.Hand;
            playPauseButton.Location = new Point(91, 86);
            playPauseButton.Name = "playPauseButton";
            playPauseButton.Size = new Size(80, 80);
            playPauseButton.TabIndex = 4;
            playPauseButton.Text = "playPauseButton";
            playPauseButton.MouseUp += PlayPauseButton_MouseUp;
            // 
            // rewindButton
            // 
            rewindButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            rewindButton.BackColor = Color.Transparent;
            rewindButton.ButtonHoveredColor = Color.FromArgb(255, 255, 255);
            rewindButton.ButtonImage = Properties.Resources.rewind10sec;
            rewindButton.ButtonPressedColor = Color.FromArgb(128, 128, 128);
            rewindButton.Cursor = Cursors.Hand;
            rewindButton.Location = new Point(21, 101);
            rewindButton.Name = "rewindButton";
            rewindButton.Size = new Size(50, 50);
            rewindButton.TabIndex = 3;
            rewindButton.Text = "rewindButton";
            rewindButton.MouseUp += RewindButton_MouseUp;
            // 
            // totalTime
            // 
            totalTime.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            totalTime.AutoSize = true;
            totalTime.Font = new Font("Inter Medium", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            totalTime.Location = new Point(819, 34);
            totalTime.Name = "totalTime";
            totalTime.Size = new Size(43, 16);
            totalTime.TabIndex = 2;
            totalTime.Text = "00:00";
            // 
            // currentTime
            // 
            currentTime.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            currentTime.AutoSize = true;
            currentTime.Font = new Font("Inter Medium", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            currentTime.Location = new Point(24, 34);
            currentTime.Name = "currentTime";
            currentTime.Size = new Size(43, 16);
            currentTime.TabIndex = 1;
            currentTime.Text = "00:00";
            // 
            // audioSlider
            // 
            audioSlider.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            audioSlider.BackColor = Color.Transparent;
            audioSlider.Cursor = Cursors.Hand;
            audioSlider.KnobRadius = 13;
            audioSlider.Location = new Point(82, 30);
            audioSlider.Maximum = 0;
            audioSlider.Name = "audioSlider";
            audioSlider.Size = new Size(736, 27);
            audioSlider.TabIndex = 0;
            audioSlider.Text = "audioSlider";
            audioSlider.ValueChanged += AudioSlider_ValueChanged;
            audioSlider.FinishedDragging += AudioSlider_FinishedDragging;
            audioSlider.MouseDown += AudioSlider_MouseDown;
            audioSlider.MouseUp += AudioSlider_MouseUp;
            // 
            // peakMeterLeftChannel
            // 
            peakMeterLeftChannel.Amplitude = 0.0001F;
            peakMeterLeftChannel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            peakMeterLeftChannel.Location = new Point(128, 94);
            peakMeterLeftChannel.Name = "peakMeterLeftChannel";
            peakMeterLeftChannel.Size = new Size(35, 265);
            peakMeterLeftChannel.TabIndex = 5;
            peakMeterLeftChannel.Text = "peakMeterLeftChannel";
            // 
            // peakMeterRightChannel
            // 
            peakMeterRightChannel.Amplitude = 0.0001F;
            peakMeterRightChannel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            peakMeterRightChannel.Location = new Point(172, 94);
            peakMeterRightChannel.Name = "peakMeterRightChannel";
            peakMeterRightChannel.Size = new Size(35, 265);
            peakMeterRightChannel.TabIndex = 6;
            peakMeterRightChannel.Text = "peakMeterRightChannel";
            // 
            // spectrumAnalyzer
            // 
            spectrumAnalyzer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            spectrumAnalyzer.FftResults = null;
            spectrumAnalyzer.Location = new Point(271, 94);
            spectrumAnalyzer.Name = "spectrumAnalyzer";
            spectrumAnalyzer.Size = new Size(585, 265);
            spectrumAnalyzer.TabIndex = 7;
            spectrumAnalyzer.Text = "spectrumAnalyzer";
            // 
            // AudioPlayerForm
            // 
            AutoScaleDimensions = new SizeF(10F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 24, 24);
            ClientSize = new Size(984, 711);
            Controls.Add(spectrumAnalyzer);
            Controls.Add(peakMeterRightChannel);
            Controls.Add(peakMeterLeftChannel);
            Controls.Add(playerButtonsPanel);
            Controls.Add(audioFileName);
            Controls.Add(menuStrip);
            Font = new Font("Inter", 12F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.FromArgb(184, 184, 184);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Margin = new Padding(4);
            MinimumSize = new Size(700, 700);
            Name = "AudioPlayerForm";
            Padding = new Padding(0, 1, 0, 1);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GAP";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            playerButtonsPanel.ResumeLayout(false);
            playerButtonsPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openAudioFileMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuItem2;
        private Label audioFileName;
        private CircularImageButton playPauseButton;
        private CircularImageButton rewindButton;
        private OpenFileDialog openAudioFileDialog;
        private System.Windows.Forms.Timer audioSliderTimer;
        private RoundedPanel playerButtonsPanel;
        private HorizontalSlider audioSlider;
        private Label currentTime;
        private Label totalTime;
        private CircularImageButton forwardButton;
        private Button equalizerButton;
        private CircularImageButton volumeButton;
        private HorizontalSlider volumeSlider;
        private PeakMeter peakMeterLeftChannel;
        private PeakMeter peakMeterRightChannel;
        private SpectrumAnalyzer spectrumAnalyzer;
    }
}