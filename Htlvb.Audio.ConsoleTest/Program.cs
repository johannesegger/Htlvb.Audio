using Htlvb.Audio;
using NAudio.Wave;

using var sampleReader = new MediaFoundationReader("sample.wav");
float[] data = sampleReader.ToSampleProvider().ReadToEnd();
AudioPlayer soundPlayer = new(data, sampleReader.WaveFormat.SampleRate, sampleReader.WaveFormat.Channels);
using CancellationTokenSource cts = new(TimeSpan.FromSeconds(2));
var t = soundPlayer.Play(cts.Token);
await t;

public static class SampleProviderExtensions
{
    public static float[] ReadToEnd(this ISampleProvider sampleProvider)
    {
        List<float> result = new();
        float[] buffer = new float[4 * 1024];
        int bytesRead;
        while ((bytesRead = sampleProvider.Read(buffer, 0, buffer.Length)) > 0)
        {
            result.AddRange(buffer.Take(bytesRead));
        }
        return result.ToArray();
    }
}
