using NAudio.Wave;
using System.IO;

namespace ThreatVisualizer;

public partial class MainForm : Form
{
    private readonly OpenFileDialog _openFileDialog = new OpenFileDialog();
    private bool _polling = false;

    private int _maxThreat = 0;
    private int _currentThreat = 0;

    private bool _dragging = false;

    public MainForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        FormClosing += (s, a) => EmptyList();

        ListBox.Items.Add("No files selected");
    }
    /********************************************
        FORM EVENTS
    ********************************************/
    private void PlayButton_Click(object sender, EventArgs e)
    {
        foreach (var item in ListBox.Items)
        {
            if (item is ListBoxItem l)
            {
                l.OutputDevice.Play();
            }
        }
        _polling = true;

        _ = Task.Run(async () =>
        {
            var audiofile = ((ListBoxItem)ListBox.Items[0]).AudioFile;
            while (_polling)
            {
                if (!_dragging)
                    Invoke(new Action(() => ProgressTrackBar.Value = (int)audiofile.Position));

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
        foreach (var item in ListBox.Items)
        {
            if (item is ListBoxItem l)
            {
                l.OutputDevice.Stop();
            }
        }
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
        if (_openFileDialog.ShowDialog() == DialogResult.OK)
        {
            EmptyList();
            try
            {
                long length = 0;
                for (int i = 0; i < _openFileDialog.FileNames.Length; i++)
                {
                    ListBox.Items.Add(new ListBoxItem(_openFileDialog.FileNames[i], _openFileDialog.SafeFileNames[i]));

                    if (i == 0)
                    {
                        length = ((ListBoxItem)ListBox.Items[0]).AudioFile.Length;
                    }
                    else
                    {
                        if (((ListBoxItem)ListBox.Items[i]).AudioFile.Length != length)
                        {
                            MessageBox.Show("Sound files have different lengths\nPlayback might not work correctly");
                        }
                    }
                }

                _maxThreat = ListBox.Items.Count;
                _currentThreat = 0;

                PlayButton.Enabled = true;
                ConfigFileButton.Enabled = true;
                UpdateThreatLevel();

                ProgressTrackBar.Maximum = (int)((ListBoxItem)ListBox.Items[0]).AudioFile.Length;
                ProgressTrackBar.Value = 0;
                ProgressTrackBar.Enabled = true;
                VolumeSlider.Enabled = true;
            }
            catch
            {
                EmptyList();
                ListBox.Items.Add("No files selected");
                _maxThreat = 0;
                _currentThreat = 0;
            }
        }
    }
    private void IncreaseButton_Click(object sender, EventArgs e)
    {
        _currentThreat++;
        if (_currentThreat > _maxThreat)
            _currentThreat = _maxThreat;

        UpdateThreatLevel();
    }
    private void DecreaseButton_Click(object sender, EventArgs e)
    {
        _currentThreat--;
        if (_currentThreat < 0)
            _currentThreat = 0;

        UpdateThreatLevel();
    }
    private void VolumeSlider_VolumeChanged(object sender, EventArgs e)
    {
        if (ListBox.Items[0] is ListBoxItem l)
        {
            l.OutputDevice.Volume = VolumeSlider.Volume;
        }
    }
    private void ProgressTrackBar_MouseUp(object sender, MouseEventArgs e)
    {
        foreach (var item in ListBox.Items)
        {
            if (item is ListBoxItem l)
            {
                l.AudioFile.Position = ProgressTrackBar.Value;
            }
        }
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
        if (_openFileDialog.ShowDialog() == DialogResult.OK)
        {
            string[] lines = File.ReadAllLines(_openFileDialog.FileName);

            int sortedFiles = 0;

            foreach (string line in lines)
            {
                for (int i = sortedFiles; i < ListBox.Items.Count; i++)
                {
                    var l = (ListBoxItem)ListBox.Items[i];
                    string onlyName = Path.GetFileNameWithoutExtension(l.FileName);

                    if (line.Contains(onlyName))
                    {
                        var data = ListBox.Items[i];
                        ListBox.Items.Remove(data);
                        ListBox.Items.Insert(sortedFiles++, data);
                        continue;
                    }
                }
                if (sortedFiles >= ListBox.Items.Count)
                    break;
            }

            if (sortedFiles < ListBox.Items.Count)
                MessageBox.Show("Not all files were found in the configuration file. Order may be wrong");

            UpdateThreatLevel();
        }
    }
    /********************************************
        LIST BOX
    ********************************************/
    private void ListBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (ListBox.SelectedItem is null)
            return;
        ListBox.DoDragDrop(ListBox.SelectedItem, DragDropEffects.Move);
    }

    private void ListBox_DragOver(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.Move;
    }

    private void ListBox_DragDrop(object sender, DragEventArgs e)
    {
        Point point = ListBox.PointToClient(new Point(e.X, e.Y));
        int index = ListBox.IndexFromPoint(point);
        if (index < 0)
            index = ListBox.Items.Count - 1;
        object data = ListBox.SelectedItem!;
        ListBox.Items.Remove(data);
        ListBox.Items.Insert(index, data);

        UpdateThreatLevel();
    }
    /********************************************
        CUSTOM METHODS
    ********************************************/
    private void EmptyList()
    {
        foreach (var item in ListBox.Items)
        {
            if (item is IDisposable disposable)
                disposable.Dispose();
        }
        ListBox.Items.Clear();
    }
    private void UpdateThreatLevel()
    {
        ThreatLevelLabel.Text = $"{_currentThreat} / {_maxThreat}";

        for (int i = 0; i < ListBox.Items.Count; i++)
        {
            if (ListBox.Items[i] is ListBoxItem l)
            {
                l.VolumeProvider.Volume = i < _currentThreat ? 1f : 0f;
            }
        }
    }
}
