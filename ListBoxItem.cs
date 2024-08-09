using NAudio.Vorbis;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace ThreatVisualizer;

internal class ListBoxItem : IDisposable
{
    public LoopedVolumeSampler VolumeProvider { get; set; }
    public WaveOutEvent OutputDevice { get; }
    public VorbisWaveReader AudioFile { get; }

    public string FileName { get; }

    public ListBoxItem(string path, string name)
    {
        try
        {
            AudioFile = new VorbisWaveReader(path);
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show($"The file {name} could not be played\n{ex.Message}");
            throw;
        }
        VolumeProvider = new LoopedVolumeSampler(AudioFile);
        OutputDevice = new WaveOutEvent();
        OutputDevice.Init(VolumeProvider);
        FileName = name;
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
            AudioFile.Dispose();
        }
    }
    ~ListBoxItem()
    {
        Dispose(false);
    }
    public override string ToString()
    {
        return FileName;
    }
}
