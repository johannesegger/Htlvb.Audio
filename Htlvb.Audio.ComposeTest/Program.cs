using NAudio.Wave;
using NAudio.Wave.SampleProviders;

// Frequenzen für beliebige Noten können berechnet werden
// siehe https://pages.mtu.edu/~suits/NoteFreqCalcs.html
double f0 = 440;
double a = Math.Pow(2, 1.0 / 12.0);
double F(int halfSteps) => f0 * Math.Pow(a, halfSteps);

// Frequenzen für einige Töne berechnen (erleichtert das spätere "Komponieren")
double c5 = F(3); // c5 = a4 + 3 Halbtöne
double e5 = F(7); // e5 = a4 + 7 Halbtöne
double g5 = F(10); // g5 = a4 + 10 Halbtöne
double c6 = F(15); // c6 = a4 + 15 Halbtöne

// Funktion zum Abspielen mehrerer Frequenzen für eine beliebige Dauer
void Play(TimeSpan duration, params double[] frequencies)
{
    ISampleProvider N(double frequency, TimeSpan duration)
    {
        return new SignalGenerator { Gain = 0.2, Frequency = frequency, Type = SignalGeneratorType.Sin }.Take(duration);
    }
    ISampleProvider M(params ISampleProvider[] providers) => new MixingSampleProvider(providers);

    var sample = M(frequencies.Select(v => N(v, duration)).ToArray());
    using var waveOut = new WaveOutEvent();
    waveOut.Init(sample);
    waveOut.Play();
    Thread.Sleep(duration);
}

// C-Dur-Dreiklang für eine Sekunde lang spielen
Play(TimeSpan.FromSeconds(1), c5, e5, g5, c6);
