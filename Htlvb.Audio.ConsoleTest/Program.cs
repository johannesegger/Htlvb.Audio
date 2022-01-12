using Htlvb.Audio;
using NAudio.Wave;

using var sampleReader = new MediaFoundationReader("sample.wav");
float[] data = sampleReader.ToSampleProvider().ReadToEnd();
Console.WriteLine($"Sample rate: {sampleReader.WaveFormat.SampleRate}");
Audio audio = new(data, sampleReader.WaveFormat.SampleRate, sampleReader.WaveFormat.Channels);
Console.WriteLine($"Duration: {audio.Duration}");
AudioPlayer audioPlayer = new(audio);
WriteAudioPlayerState(audioPlayer);
audioPlayer.Play();
WriteAudioPlayerState(audioPlayer);
await Task.Delay(2000);
audioPlayer.Pause();
WriteAudioPlayerState(audioPlayer);
await Task.Delay(2000);
audioPlayer.Play();
WriteAudioPlayerState(audioPlayer);
await Task.Delay(2000);
audioPlayer.Stop();
WriteAudioPlayerState(audioPlayer);

// audio.SaveAs?Mp3("out.mp3");

void WriteAudioPlayerState(AudioPlayer audioPlayer)
{
    Console.WriteLine($"IsStopped: {audioPlayer.IsStopped}, IsPaused: {audioPlayer.IsPaused}, IsRunning: {audioPlayer.IsRunning}, Position: {audioPlayer.Position}");
}

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
