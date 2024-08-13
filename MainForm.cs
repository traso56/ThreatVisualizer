using NAudio.Mixer;
using System.Windows.Forms;

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

        //PopulateDataGridView();
    }
    private void PopulateDataGridView()
    {
        GridView.ColumnCount = 6;

        GridView.Columns[0].Name = "1";
        GridView.Columns[1].Name = "2";
        GridView.Columns[2].Name = "3";
        GridView.Columns[3].Name = "4";
        GridView.Columns[4].Name = "5";
        GridView.Columns[5].Name = "6";


        string[] row0 = ["a", "b", "r", "a", "g", ""];
        string[] row1 = ["", "", "", "", "", ""];
        string[] row2 = ["a", "", "", "", "", ""];
        string[] row3 = ["nb", "", "", "", "", ""];
        string[] row4 = ["e", "", "", "", "", ""];
        string[] row5 = ["t", "", "", "", "", ""];

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

        GridView.Rows[2].Cells[2].Style.BackColor = Color.Teal;

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
        RerollButton.Enabled = false;
        OpenFileButton.Enabled = false;
        ConfigFileButton.Enabled = false;
    }
    private void StopButton_Click(object sender, EventArgs e)
    {
        _mixer.OutputDevice.Stop();

        _polling = false;

        PlayButton.Enabled = true;
        StopButton.Enabled = false;
        RerollButton.Enabled = true;
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

            if (!_mixer.AllSameLength)
                MessageBox.Show("Some files have different lengths. Playback might not work correctly.");

            ConfigFileButton.Enabled = true;

            ProgressTrackBar.Maximum = (int)_mixer.Readers[0].File.Length;
            ProgressTrackBar.Value = 0;

            PlayButton.Enabled = false;
            StopButton.Enabled = false;
            RerollButton.Enabled = false;
            VolumeSlider.Enabled = false;
            ProgressTrackBar.Enabled = false;
            ThreatTrackBar.Enabled = false;

            GridView.Rows.Clear();

            GridView.ColumnCount = 1;

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
    private void ConfigFileButton_Click(object sender, EventArgs e)
    {
        _openFileDialog.Multiselect = false;
        _openFileDialog.Filter = "Threat config file|*.txt";

        if (_openFileDialog.ShowDialog() != DialogResult.OK)
            return;

        _mixer.ReadConfig(_openFileDialog.FileName);
        _mixer.AdjustThreat(ThreatTrackBar.Value);
        _mixer.Draw(GridView);

        PlayButton.Enabled = true;
        RerollButton.Enabled = true;

        VolumeSlider.Enabled = true;
        ProgressTrackBar.Enabled = true;
        ThreatTrackBar.Enabled = true;
    }
    private void RerollButton_Click(object sender, EventArgs e)
    {
        _mixer.Randomize();
        _mixer.AdjustThreat(ThreatTrackBar.Value);
        _mixer.Draw(GridView);
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
    private void GridView_SelectionChanged(object sender, EventArgs e)
    {
        GridView.ClearSelection();
    }

    private void ThreatTrackBar_ValueChanged(object sender, EventArgs e)
    {
        ThreatPercentageLabel.Text = ThreatTrackBar.Value.ToString();
        _mixer.AdjustThreat(ThreatTrackBar.Value);
    }
}
