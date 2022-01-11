namespace Htlvb.Audio;

public class Audio
{
    public Audio(float[] samples, int samplesPerSecond, int channels)
    {
        Samples = samples ?? throw new ArgumentNullException(nameof(samples));
        SamplesPerSecond = samplesPerSecond > 0 ? samplesPerSecond : throw new ArgumentException("Sample rate must be positive.");;
        Channels = channels > 0 ? channels : throw new ArgumentException("Number of channels must be positive.");
    }

    public float[] Samples { get; }
    public int SamplesPerSecond { get; }
    public int Channels { get; }
}
