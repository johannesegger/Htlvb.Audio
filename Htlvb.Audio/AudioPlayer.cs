using NAudio.Utils;
using NAudio.Wave;

namespace Htlvb.Audio;

public class AudioPlayer : IDisposable
{
    private WaveOutEvent? waveOut;

    public AudioPlayer(Audio audio)
    {
        Audio = audio ?? throw new ArgumentNullException(nameof(audio));
    }

    public Audio Audio { get; }

    public bool IsStopped => waveOut == null || waveOut.PlaybackState == PlaybackState.Stopped;
    public bool IsRunning => waveOut != null && waveOut.PlaybackState == PlaybackState.Playing;
    public bool IsPaused => waveOut != null && waveOut.PlaybackState == PlaybackState.Paused;
    public TimeSpan Position
    {
        get
        {
            if (waveOut == null)
            {
                return TimeSpan.Zero;
            }
            return waveOut.GetPositionTimeSpan();
        }
    }

    public void Play()
    {
        if (waveOut != null)
        {
            waveOut.Play();
        }
        else
        {
            waveOut = new();
            var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(Audio.SamplesPerSecond, Audio.Channels);
            ISampleProvider sample = new InMemorySampleProvider(waveFormat, Audio.Samples, 0, Audio.Samples.Length);
            waveOut.Init(sample);
            waveOut.Play();
        }
    }

    public void Stop()
    {
        waveOut?.Stop();
        waveOut = null;
    }

    public void Pause()
    {
        waveOut?.Pause();
    }

    public void Dispose()
    {
        waveOut?.Dispose();
        waveOut = null;
    }
}
