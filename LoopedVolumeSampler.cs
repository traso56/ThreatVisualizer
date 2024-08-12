using NAudio.Vorbis;
using NAudio.Wave;

namespace ThreatVisualizer;

public class LoopedVolumeSampler : ISampleProvider, IDisposable
{
    public bool EnableLooping { get; set; } = true;
    public float Volume { get; set; }

    public VorbisWaveReader File { get; }
    public WaveFormat WaveFormat => File.WaveFormat;

    public LoopedVolumeSampler(string path)
    {
        File = new VorbisWaveReader(path);
        Volume = 1f;
    }

    public int Read(float[] buffer, int offset, int count)
    {
        int totalBytesRead = 0;

        while (totalBytesRead < count)
        {
            int bytesRead = File.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
            if (Volume != 1f)
            {
                for (int i = 0; i < count - totalBytesRead; i++)
                {
                    buffer[offset + totalBytesRead + i] *= Volume;
                }
            }
            if (bytesRead == 0)
            {
                if (File.Position == 0 || !EnableLooping)
                {
                    // something wrong with the source stream
                    break;
                }
                // loop
                File.Position = 0;
            }
            totalBytesRead += bytesRead;
        }
        return totalBytesRead;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            File.Dispose();
        }
    }
    ~LoopedVolumeSampler()
    {
        Dispose(false);
    }
}

