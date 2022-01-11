using NAudio.Wave;

namespace Htlvb.Audio;

internal class InMemorySampleProvider : ISampleProvider
{
    private readonly WaveFormat waveFormat;
    private readonly float[] buffer;
    private readonly int offset;
    private readonly int length;
    private int position;

    public WaveFormat WaveFormat => waveFormat;

    public InMemorySampleProvider(WaveFormat waveFormat, float[] buffer, int offset, int length)
    {
        this.waveFormat = waveFormat;
        this.buffer = buffer;
        this.offset = offset;
        this.length = length;
        this.position = offset;
    }

    public int Read(float[] buffer, int offset, int count)
    {
        var remainingLength = this.length - this.position;
        var minLength = Math.Min(count, remainingLength);
        for (int i = 0; i < minLength; i++)
        {
            buffer[offset + i] = this.buffer[position];
            position++;
        }
        return minLength;
    }
}
