using NAudio.Vorbis;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace ThreatVisualizer;

public class SoundMixer : IDisposable
{
    public List<LoopedVolumeSampler> Readers { get; } = [];
    public List<string> FileNames { get; } = [];

    public MixingSampleProvider MixerProvider { get; }
    public WaveOutEvent OutputDevice { get; }

    public bool AllSameLength
    {
        get
        {
            long length = Readers[0].File.Length;
            for (int i = 1; i < Readers.Count; i++)
            {
                if (Readers[i].File.Length != length)
                    return false;
            }
            return true;
        }
    }

    public SoundMixer(string[] paths)
    {
        foreach (var path in paths)
        {
            try
            {
                Readers.Add(new LoopedVolumeSampler(path));
            }
            catch
            {
                foreach (var reader in Readers)
                    reader.Dispose();
                throw;
            }
            FileNames.Add(Path.GetFileNameWithoutExtension(path));
        }

        MixerProvider = new MixingSampleProvider(Readers);

        OutputDevice = new WaveOutEvent();
        OutputDevice.Init(MixerProvider);
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
            OutputDevice.Dispose();
            foreach (var reader in Readers)
                reader.Dispose();
        }
    }
    ~SoundMixer()
    {
        Dispose(false);
    }
}
