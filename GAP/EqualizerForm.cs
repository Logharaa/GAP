using GAP.SampleProviders;

namespace GAP
{
    public partial class EqualizerForm : Form
    {
        private Equalizer? _equalizer;

        public EqualizerForm(Equalizer? equalizer)
        {
            InitializeComponent();
            _equalizer = equalizer;

            if (_equalizer != null)
            {
                equalizerBand80.Value = (int)(_equalizer.bands[0].Gain * 10);
                equalizerBand250.Value = (int)(_equalizer.bands[1].Gain * 10);
                equalizerBand500.Value = (int)(_equalizer.bands[2].Gain * 10);
                equalizerBand1500.Value = (int)(_equalizer.bands[3].Gain * 10);
                equalizerBand3000.Value = (int)(_equalizer.bands[4].Gain * 10);
                equalizerBand5000.Value = (int)(_equalizer.bands[5].Gain * 10);
                equalizerBand10000.Value = (int)(_equalizer.bands[6].Gain * 10);
            }
        }

        public void UpdateEqualizer(Equalizer equalizer)
        {
            _equalizer = equalizer;
        }

        private void EqualizerBand90_ValueChanged(object sender, int e)
        {
            _equalizer?.UpdateGain(0, e * 0.1f);
        }

        private void EqualizerBand250_ValueChanged(object sender, int e)
        {
            _equalizer?.UpdateGain(1, e * 0.1f);
        }

        private void EqualizerBand500_ValueChanged(object sender, int e)
        {
            _equalizer?.UpdateGain(2, e * 0.1f);
        }

        private void EqualizerBand1500_ValueChanged(object sender, int e)
        {
            _equalizer?.UpdateGain(3, e * 0.1f);
        }

        private void EqualizerBand3000_ValueChanged(object sender, int e)
        {
            _equalizer?.UpdateGain(4, e * 0.1f);
        }

        private void EqualizerBand5000_ValueChanged(object sender, int e)
        {
            _equalizer?.UpdateGain(5, e * 0.1f);
        }

        private void EqualizerBand8000_ValueChanged(object sender, int e)
        {
            _equalizer?.UpdateGain(6, e * 0.1f);
        }
    }
}
