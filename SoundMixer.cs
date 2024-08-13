using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace ThreatVisualizer;

public class SoundMixer : IDisposable
{
    public enum CellState
    {
        Empty, Available, Selected
    }

    public List<LoopedVolumeSampler> Readers { get; } = [];
    public List<string> FileNames { get; } = [];

    public MixingSampleProvider MixerProvider { get; }
    public WaveOutEvent OutputDevice { get; }

    private CellState[,] _cells = null!;
    private List<int> _layerIndexes = [];
    private List<List<int>> _possibleIndexes = [];

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

    public int Layers => _cells.GetLength(0);
    public int SongAmount => _cells.GetLength(1);

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

    public void ReadConfig(string path)
    {
        string[] lines = File.ReadAllLines(path).Where(l => l.StartsWith("Layer :")).ToArray();

        _cells = new CellState[lines.Length, FileNames.Count];

        for (int i = 0; i < lines.Length; i++)
        {
            _possibleIndexes.Add([]);
            for (int j = 0; j < FileNames.Count; j++)
            {
                if (lines[i].Contains(FileNames[j]))
                {
                    _cells[i, j] = CellState.Available;
                    _possibleIndexes[i].Add(j);
                }
                else
                {
                    _cells[i, j] = CellState.Empty;
                }
            }
        }

        Randomize();
    }
    public void Randomize()
    {
        _layerIndexes.Clear();

        for (int i = 0; i < Layers; i++)
        {
            List<int> possible = [];
            possible.AddRange(_possibleIndexes[i]);
            possible = possible.Distinct().ToList();
            possible = possible.Except(_layerIndexes).ToList();

            if (possible.Count > 0)
            {
                Random rng = new Random();
                _layerIndexes.Add(possible[rng.Next(possible.Count)]);
            }
            else
            {
                _layerIndexes.Add(-1);
            }
        }

        for (int i = 0; i < Layers; i++)
        {
            foreach (int j in _possibleIndexes[i])
                _cells[i, j] = CellState.Available;
            if (_layerIndexes[i] >= 0)
            {
                _cells[i, _layerIndexes[i]] = CellState.Selected;
            }
        }
    }
    public void Draw(DataGridView grid)
    {
        grid.Rows.Clear();
        grid.Columns.Clear();

        grid.ColumnCount = Layers;
        for (int i = 0; i < Layers; i++)
            grid.Columns[i].Name = (i + 1).ToString();

        grid.RowCount = SongAmount;
        for (int i = 0; i < SongAmount; i++)
            grid.Rows[i].HeaderCell.Value = FileNames[i];

        for (int i = 0; i < Layers; i++)
        {
            for (int j = 0; j < SongAmount; j++)
            {
                switch (_cells[i, j])
                {
                    case CellState.Empty:
                        grid.Rows[j].Cells[i].Style.BackColor = Color.White;
                        break;
                    case CellState.Available:
                        grid.Rows[j].Cells[i].Style.BackColor = Color.Blue;
                        break;
                    case CellState.Selected:
                        grid.Rows[j].Cells[i].Style.BackColor = Color.Green;
                        break;
                }
            }
        }
    }
    public void AdjustThreat(int percentage)
    {
        foreach (var reader in Readers)
            reader.Volume = 0f;

        float level = percentage / 100f;

        for (int i = 0; i < Layers; i++)
        {
            float value = (float)(i + 1) / Layers;
            if (level > value && _layerIndexes[i] >= 0)
            {
                Readers[_layerIndexes[i]].Volume = 1f;
            }
            else if (_layerIndexes[i] >= 0)
            {
                float fraction = level - (float)i / Layers;
                float volume = fraction / (1f / Layers);
                Readers[_layerIndexes[i]].Volume = volume;
                break;
            }
        }
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
