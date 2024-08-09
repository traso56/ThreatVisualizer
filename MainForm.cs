using NAudio.Wave;

namespace ThreatVisualizer;

public partial class MainForm : Form
{
    private readonly OpenFileDialog _openFileDialog;
    private bool _polling = false;

    private int _maxThreat = 0;
    private int _currentThreat = 0;

    public MainForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        FormClosing += (s, a) => EmptyList();

        _openFileDialog = new OpenFileDialog
        {
            Multiselect = true
        };

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
        if (_openFileDialog.ShowDialog() == DialogResult.OK)
        {
            EmptyList();
            try
            {
                for (int i = 0; i < _openFileDialog.FileNames.Length; i++)
                {
                    ListBox.Items.Add(new ListBoxItem(_openFileDialog.FileNames[i], _openFileDialog.SafeFileNames[i]));
                }

                _maxThreat = ListBox.Items.Count;
                _currentThreat = 0;

                PlayButton.Enabled = true;
                ConfigFileButton.Enabled = true;
                UpdateThreatLevel();

                ProgressTrackBar.Maximum = (int)((ListBoxItem)ListBox.Items[0]).AudioFile.Length;
                ProgressTrackBar.Enabled = true;
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
