using NAudio.Wave;

namespace Htlvb.Audio;

public class AudioPlayer
{
    public AudioPlayer(float[] samples, int samplesPerSecond, int channels)
    {
        Samples = samples ?? throw new ArgumentNullException(nameof(samples));
        SamplesPerSecond = samplesPerSecond > 0 ? samplesPerSecond : throw new ArgumentException("Sample rate must be positive.");;
        Channels = channels > 0 ? channels : throw new ArgumentException("Number of channels must be positive.");
    }

    public float[] Samples { get; }
    public int SamplesPerSecond { get; }
    public int Channels { get; }


    public async Task Play(CancellationToken ct = default)
    {
        using WaveOutEvent wo = new();
        var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(SamplesPerSecond, Channels);
        ISampleProvider sample = new InMemorySampleProvider(waveFormat, Samples, 0, Samples.Length);
        wo.Init(sample);
        wo.Play();
        TaskCompletionSource tcs = new();
        EventHandler<StoppedEventArgs> playbackStopped = (s, e) =>
        {
            tcs.SetResult();
        };
        ct.Register(() =>
        {
            wo.PlaybackStopped -= playbackStopped;
            tcs.SetCanceled();
        });
        wo.PlaybackStopped += playbackStopped;
        await tcs.Task;
    }
}
