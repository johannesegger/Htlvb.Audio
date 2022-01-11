using NAudio.Wave;

namespace Htlvb.Audio;

public static class AudioFileWriterExtensions
{
    public static void SaveAsMp3(this Audio audio, string path)
    {
        var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(audio.SamplesPerSecond, audio.Channels);
        ISampleProvider sample = new InMemorySampleProvider(waveFormat, audio.Samples, 0, audio.Samples.Length);
        MediaFoundationEncoder.EncodeToMp3(sample.ToWaveProvider(), path);
    }
}
