using NAudio.Vorbis;
using NAudio.Wave;

namespace ThreatVisualizer;

public class LoopedVolumeSampler : ISampleProvider
{
    public bool EnableLooping { get; set; } = true;
    public WaveFormat WaveFormat => _source.WaveFormat;
    public float Volume { get; set; }

    private readonly VorbisWaveReader _source;

    public LoopedVolumeSampler(VorbisWaveReader source)
    {
        _source = source;
        Volume = 1f;
    }

    public int Read(float[] buffer, int offset, int count)
    {
        int totalBytesRead = 0;

        while (totalBytesRead < count)
        {
            int bytesRead = _source.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
            if (Volume != 1f)
            {
                for (int i = 0; i < count - totalBytesRead; i++)
                {
                    buffer[offset + totalBytesRead + i] *= Volume;
                }
            }
            if (bytesRead == 0)
            {
                if (_source.Position == 0 || !EnableLooping)
                {
                    // something wrong with the source stream
                    break;
                }
                // loop
                _source.Position = 0;
            }
            totalBytesRead += bytesRead;
        }
        return totalBytesRead;
    }
}
