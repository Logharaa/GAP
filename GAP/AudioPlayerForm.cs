using GAP.SampleProviders;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace GAP
{
    public partial class AudioPlayerForm : Form
    {
        private WasapiOut? _wasapiOut;
        private AudioFileReader? _audioFileReader;
        private Equalizer? _equalizer;
        private MeteringSampleProvider? _amplitudeProvider;
        private FourierTransformProvider? _fourierTransformProvider;
        private string? _audioFilePath;
        private bool _isPlaying = false;
        private bool _isDraggingAudioSlider = false;
        private int _volumeBeforeMute = 50;

        private EqualizerForm? _eqForm;
        private readonly EqualizerBand[] _eqBands = new EqualizerBand[]
        {
            new(80.0f, 0.4f, 0.0f),
            new(250.0f, 0.4f, 0.0f),
            new(500.0f, 0.4f, 0.0f),
            new(1500.0f, 0.4f, 0.0f),
            new(3000.0f, 0.4f, 0.0f),
            new(5000.0f, 0.4f, 0.0f),
            new(10000.0f, 0.4f, 0.0f),
        };

        public AudioPlayerForm()
        {
            InitializeComponent();
            menuStrip.Renderer = new GapMenuStripRenderer();
        }

        private void OpenFileMenuItem_Click(object sender, EventArgs e)
        {
            if (openAudioFileDialog.ShowDialog() == DialogResult.OK)
            {
                CleanUpAudio();
                ResetAudioSlider();
                _audioFilePath = openAudioFileDialog.FileName;
                audioFileName.Text = openAudioFileDialog.SafeFileName;
                audioFileName.ForeColor = Color.FromArgb(208, 208, 208);
                InitAudio();
                audioSlider.Maximum = GetSecondsFromTimeSpan(_audioFileReader!.TotalTime);
                totalTime.Text = _audioFileReader!.TotalTime.ToString(@"mm\:ss");
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "GAP (for \"Gaël Audio Player\") is a personal project I created to familiarize myself with audio application development in C#. Feel free to use it for any purpose.",
                "About GAP"
            );
        }

        private void PlayPauseButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (_audioFilePath == null)
                return;

            if (_isPlaying)
            {
                _wasapiOut?.Pause();
                audioSliderTimer.Stop();
                playPauseButton.ButtonImage = Properties.Resources.play_arrow;
                StopVisualizationsSmoothly();
                _isPlaying = false;
            }
            else
            {
                if (_audioFileReader == null || _wasapiOut == null)
                {
                    // When audio is replayed after it has been stopped.
                    InitAudio();
                }

                _wasapiOut?.Play();
                audioSliderTimer.Start();
                playPauseButton.ButtonImage = Properties.Resources.pause;
                EnsureVisualizationsReady();
                _isPlaying = true;
            }
        }

        private void RewindButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (_audioFilePath == null || _audioFileReader == null || _wasapiOut == null)
                return;

            TimeSpan newCurrentTime = _audioFileReader.CurrentTime - TimeSpan.FromSeconds(10);

            if (newCurrentTime < TimeSpan.Zero)
                _audioFileReader.CurrentTime = TimeSpan.Zero;
            else
                _audioFileReader.CurrentTime = newCurrentTime;

            UpdateAudioSlider();
        }

        private void ForwardButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (_audioFilePath == null || _audioFileReader == null || _wasapiOut == null)
                return;

            TimeSpan newCurrentTime = _audioFileReader.CurrentTime + TimeSpan.FromSeconds(10);

            if (newCurrentTime >= _audioFileReader.TotalTime)
            {
                _wasapiOut.Stop();
                // No need to update the audio slider here because
                // the Stop() call will invoke OnPlaybackStopped
                // which updates the slider itself.
            }
            else
            {
                _audioFileReader.CurrentTime = newCurrentTime;
                UpdateAudioSlider();
            }
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            audioSliderTimer.Stop();
            ResetAudioSlider();
            playPauseButton.ButtonImage = Properties.Resources.play_arrow;
            StopVisualizationsSmoothly();
            _isPlaying = false;
            CleanUpAudio();
        }

        private void StopVisualizationsSmoothly()
        {
            peakMeterLeftChannel.StopSmoothly();
            peakMeterRightChannel.StopSmoothly();
            spectrumAnalyzer.StopSmoothly();
        }

        private void EnsureVisualizationsReady()
        {
            // Handle corner case where the user replays the audio before
            // the visualizations has finished stopping smoothly.
            peakMeterLeftChannel.EndStoppingAnimation();
            peakMeterRightChannel.EndStoppingAnimation();
            spectrumAnalyzer.EndStoppingAnimation();
        }

        private void OnStreamVolume(object? sender, StreamVolumeEventArgs e)
        {
            peakMeterLeftChannel.Amplitude = e.MaxSampleValues[0];

            if (_audioFileReader!.WaveFormat.Channels == 2)
                peakMeterRightChannel.Amplitude = e.MaxSampleValues[1];
            else
                peakMeterRightChannel.Amplitude = e.MaxSampleValues[0];
        }

        private void OnFftCalculated(object? sender, FftEventArgs e)
        {
            spectrumAnalyzer.FftResults = e.Results;
        }

        private void InitAudio()
        {
            _wasapiOut = new();
            _wasapiOut.PlaybackStopped += OnPlaybackStopped;
            _audioFileReader = new(_audioFilePath) { Volume = GetNormalizedVolume(volumeSlider.Value) };
            _equalizer = new(_audioFileReader, _eqBands);
            _eqForm?.UpdateEqualizer(_equalizer);
            _amplitudeProvider = new(_equalizer, _audioFileReader.WaveFormat.SampleRate / 48);
            _amplitudeProvider.StreamVolume += OnStreamVolume;
            _fourierTransformProvider = new(_amplitudeProvider);
            _fourierTransformProvider.FftCalculated += OnFftCalculated;
            _wasapiOut.Init(_fourierTransformProvider);
        }

        private void CleanUpAudio()
        {
            if (_audioFileReader != null)
            {
                _audioFileReader.Dispose();
                _audioFileReader = null;
            }

            if (_wasapiOut != null)
            {
                _wasapiOut.Dispose();
                _wasapiOut = null;
            }
        }

        private void UpdateAudioSlider()
        {
            if (_audioFileReader != null)
            {
                audioSlider.Value = GetSecondsFromTimeSpan(_audioFileReader.CurrentTime);
                currentTime.Text = _audioFileReader.CurrentTime.ToString(@"mm\:ss");
            }
        }

        private void ResetAudioSlider()
        {
            audioSlider.Value = audioSlider.Minimum;
            currentTime.Text = "00:00";
        }

        private void AudioSliderTimer_Tick(object sender, EventArgs e)
        {
            if (!_isDraggingAudioSlider)
                UpdateAudioSlider();
        }

        private void AudioSlider_ValueChanged(int oldValue, int newValue)
        {
            currentTime.Text = TimeSpan.FromSeconds(newValue).ToString(@"mm\:ss");
        }

        private void AudioSlider_FinishedDragging(object sender, int newValue)
        {
            if (_audioFileReader != null)
            {
                _audioFileReader.CurrentTime = TimeSpan.FromSeconds(newValue);
                UpdateAudioSlider();
            }
        }

        private void AudioSlider_MouseDown(object sender, MouseEventArgs e)
        {
            _isDraggingAudioSlider = true;
        }

        private void AudioSlider_MouseUp(object sender, MouseEventArgs e)
        {
            _isDraggingAudioSlider = false;
        }

        private void EqualizerButton_Click(object sender, EventArgs e)
        {
            if (_eqForm == null || _eqForm.IsDisposed)
            {
                _eqForm = new(_equalizer);
                _eqForm.Show();
            }
            else
            {
                _eqForm.WindowState = FormWindowState.Normal;
                _eqForm.Activate();
            }
        }

        private void VolumeSlider_ValueChanged(int oldValue, int newValue)
        {
            if (newValue == volumeSlider.Minimum
                && oldValue > volumeSlider.Minimum)
            {
                volumeButton.ButtonImage = Properties.Resources.volume_off;
            }
            else if (newValue > volumeSlider.Minimum
                && oldValue == volumeSlider.Minimum)
            {
                volumeButton.ButtonImage = Properties.Resources.volume_on;
            }

            if (_audioFileReader != null)
                _audioFileReader.Volume = GetNormalizedVolume(newValue);
        }

        private void VolumeButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (volumeSlider.Value != volumeSlider.Minimum)
            {
                _volumeBeforeMute = volumeSlider.Value;
                volumeSlider.Value = 0;
                if (_audioFileReader != null)
                    _audioFileReader.Volume = 0.0f;
            }
            else
            {
                volumeSlider.Value = _volumeBeforeMute;
                if (_audioFileReader != null)
                    _audioFileReader.Volume = GetNormalizedVolume(_volumeBeforeMute);
            }
        }


        private static float GetNormalizedVolume(int value)
        {
            // I use power scale instead of decibel scale because volume transition
            // still feels smooth and it can represent complete silence,
            // unlike decibel scale where I would have to choose an arbitrary
            // minimum value like -60 dB (complete silence being -inf dB).
            return (float)Math.Pow(value * 0.01f, 2);
        }

        private static int GetSecondsFromTimeSpan(TimeSpan ts)
        {
            return (int)(ts.TotalSeconds + 0.5);
        }
    }


    // Custom renderer to change menuStrip colors (can't do it from designer).
    public class GapMenuStripRenderer : ToolStripProfessionalRenderer
    {

        public GapMenuStripRenderer() : base(new CustomColors()) { }

        private class CustomColors : ProfessionalColorTable
        {
            private readonly Color gray78 = Color.FromArgb(78, 78, 78);
            private readonly Color gray56 = Color.FromArgb(56, 56, 56);

            // Item background color on hover.
            public override Color MenuItemSelectedGradientBegin => gray78;
            public override Color MenuItemSelectedGradientEnd => gray78;

            public override Color MenuItemBorder => gray78;

            // Item background color on click.
            public override Color MenuItemPressedGradientBegin => gray56;
            public override Color MenuItemPressedGradientEnd => gray56;

            public override Color MenuBorder => Color.FromArgb(108, 108, 108);

            public override Color ToolStripDropDownBackground => gray56;
            public override Color SeparatorDark => Color.FromArgb(88, 88, 88);

            // Icon column background color.
            public override Color ImageMarginGradientBegin => gray56;
            public override Color ImageMarginGradientMiddle => gray56;
            public override Color ImageMarginGradientEnd => gray56;
        }
    }
}