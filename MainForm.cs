using NAudio.Wave;
using System.IO;

namespace ThreatVisualizer;

public partial class MainForm : Form
{
    private readonly OpenFileDialog _openFileDialog = new OpenFileDialog();

    private bool _polling = false;
    private bool _dragging = false;

    private SoundMixer _mixer = null!;

    public MainForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        GridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllHeaders;
        GridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        GridView.CellBorderStyle = DataGridViewCellBorderStyle.Single;
        GridView.GridColor = Color.Black;
    }
    private void PopulateDataGridView()
    {
        GridView.Columns[0].Name = "1";
        GridView.Columns[1].Name = "2";
        GridView.Columns[2].Name = "3";
        GridView.Columns[3].Name = "4";
        GridView.Columns[4].Name = "5";
        GridView.Columns[4].Name = "6";

        string[] row0 = ["", "", "", "", "", ""];
        string[] row1 = ["", "", "", "", "", ""];
        string[] row2 = ["", "", "", "", "", ""];
        string[] row3 = ["", "", "", "", "", ""];
        string[] row4 = ["", "", "", "", "", ""];
        string[] row5 = ["", "", "", "", "", ""];

        GridView.Rows.Add(row0);
        GridView.Rows.Add(row1);
        GridView.Rows.Add(row2);
        GridView.Rows.Add(row3);
        GridView.Rows.Add(row4);
        GridView.Rows.Add(row5);

        GridView.Rows[0].HeaderCell.Value = "Song1";
        GridView.Rows[1].HeaderCell.Value = "Song2";
        GridView.Rows[2].HeaderCell.Value = "Song3";
        GridView.Rows[3].HeaderCell.Value = "Song4";
        GridView.Rows[4].HeaderCell.Value = "Song5";
        GridView.Rows[5].HeaderCell.Value = "Song6";

    }
    /********************************************
        FORM EVENTS
    ********************************************/
    private void PlayButton_Click(object sender, EventArgs e)
    {
        _mixer.OutputDevice.Play();
        _polling = true;

        _ = Task.Run(async () =>
        {
            while (_polling)
            {
                if (!_dragging)
                    Invoke(() => ProgressTrackBar.Value = (int)_mixer.Readers[0].File.Position);

                await Task.Delay(100);
            }
        });
        PlayButton.Enabled = false;
        StopButton.Enabled = true;
        OpenFileButton.Enabled = false;
        ConfigFileButton.Enabled = false;
    }
    private void StopButton_Click(object sender, EventArgs e)
    {
        _mixer.OutputDevice.Stop();

        _polling = false;

        PlayButton.Enabled = true;
        StopButton.Enabled = false;
        OpenFileButton.Enabled = true;
        ConfigFileButton.Enabled = true;
    }
    private void OpenFileButton_Click(object sender, EventArgs e)
    {
        _openFileDialog.Multiselect = true;
        _openFileDialog.Filter = "Sound files|*.ogg";

        if (_openFileDialog.ShowDialog() != DialogResult.OK)
            return;

        try
        {
            _mixer?.Dispose();

            _mixer = new SoundMixer(_openFileDialog.FileNames);

            MessageBox.Show($"Loaded {_openFileDialog.FileNames.Length} files");

            if (!_mixer.AllSameLength)
                MessageBox.Show("Some files have different lengths. Playback might not work correctly.");

            PlayButton.Enabled = true;
            ConfigFileButton.Enabled = true;

            VolumeSlider.Enabled = true;
            ProgressTrackBar.Maximum = (int)_mixer.Readers[0].File.Length;
            ProgressTrackBar.Value = 0;
            ProgressTrackBar.Enabled = true;

            // REMOVE
            GridView.Rows.Clear();

            GridView.ColumnCount = 1;

            GridView.Columns[0].Name = "1";

            for (int i = 0; i < _mixer.FileNames.Count; i++)
                GridView.Rows.Add("");

            for (int i = 0; i < _mixer.FileNames.Count; i++)
                GridView.Rows[i].HeaderCell.Value = _mixer.FileNames[i];
        }
        catch
        {
            MessageBox.Show("One or more of the files could not be read!");
        }
    }
    private void VolumeSlider_VolumeChanged(object sender, EventArgs e)
    {
        _mixer.OutputDevice.Volume = VolumeSlider.Volume;
    }
    private void ProgressTrackBar_MouseUp(object sender, MouseEventArgs e)
    {
        foreach (var reader in _mixer.Readers)
            reader.File.Position = ProgressTrackBar.Value;

        _dragging = false;
    }
    private void ProgressTrackBar_MouseDown(object sender, MouseEventArgs e)
    {
        _dragging = true;
    }
    private void ProgressTrackBar_MouseWheel(object sender, MouseEventArgs e)
    {
        ((HandledMouseEventArgs)e).Handled = true;
    }
    private void ProgressTrackBar_KeyDown(object sender, KeyEventArgs e)
    {
        e.Handled = true;
    }
    private void ConfigFileButton_Click(object sender, EventArgs e)
    {
        _openFileDialog.Multiselect = false;
        _openFileDialog.Filter = "Threat config file|*.txt";

        if (_openFileDialog.ShowDialog() != DialogResult.OK)
            return;

        string[] lines = File.ReadAllLines(_openFileDialog.FileName);

        foreach (var line in lines)
        {
            if (line.StartsWith("Layer :"))
            {

            }
        }
    }
}
