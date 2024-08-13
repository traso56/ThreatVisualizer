namespace ThreatVisualizer;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        PlayButton = new Button();
        StopButton = new Button();
        OpenFileButton = new Button();
        VolumeSlider = new NAudio.Gui.VolumeSlider();
        ConfigFileButton = new Button();
        ProgressTrackBar = new TrackBar();
        GridView = new DataGridView();
        ThreatTrackBar = new TrackBar();
        ThreatLabel = new Label();
        ProgressLabel = new Label();
        RerollButton = new Button();
        ThreatPercentageLabel = new Label();
        ((System.ComponentModel.ISupportInitialize)ProgressTrackBar).BeginInit();
        ((System.ComponentModel.ISupportInitialize)GridView).BeginInit();
        ((System.ComponentModel.ISupportInitialize)ThreatTrackBar).BeginInit();
        SuspendLayout();
        // 
        // PlayButton
        // 
        PlayButton.Enabled = false;
        PlayButton.Location = new Point(655, 298);
        PlayButton.Name = "PlayButton";
        PlayButton.Size = new Size(105, 23);
        PlayButton.TabIndex = 1;
        PlayButton.Text = "Play";
        PlayButton.UseVisualStyleBackColor = true;
        PlayButton.Click += PlayButton_Click;
        // 
        // StopButton
        // 
        StopButton.Enabled = false;
        StopButton.Location = new Point(655, 327);
        StopButton.Name = "StopButton";
        StopButton.Size = new Size(105, 23);
        StopButton.TabIndex = 2;
        StopButton.Text = "Stop";
        StopButton.UseVisualStyleBackColor = true;
        StopButton.Click += StopButton_Click;
        // 
        // OpenFileButton
        // 
        OpenFileButton.Location = new Point(655, 12);
        OpenFileButton.Name = "OpenFileButton";
        OpenFileButton.Size = new Size(105, 23);
        OpenFileButton.TabIndex = 3;
        OpenFileButton.Text = "Open files";
        OpenFileButton.UseVisualStyleBackColor = true;
        OpenFileButton.Click += OpenFileButton_Click;
        // 
        // VolumeSlider
        // 
        VolumeSlider.Enabled = false;
        VolumeSlider.ImeMode = ImeMode.Katakana;
        VolumeSlider.Location = new Point(655, 392);
        VolumeSlider.Name = "VolumeSlider";
        VolumeSlider.Size = new Size(105, 45);
        VolumeSlider.TabIndex = 5;
        VolumeSlider.VolumeChanged += VolumeSlider_VolumeChanged;
        // 
        // ConfigFileButton
        // 
        ConfigFileButton.Enabled = false;
        ConfigFileButton.Location = new Point(655, 41);
        ConfigFileButton.Name = "ConfigFileButton";
        ConfigFileButton.Size = new Size(105, 23);
        ConfigFileButton.TabIndex = 12;
        ConfigFileButton.Text = "Read config file";
        ConfigFileButton.UseVisualStyleBackColor = true;
        ConfigFileButton.Click += ConfigFileButton_Click;
        // 
        // ProgressTrackBar
        // 
        ProgressTrackBar.Enabled = false;
        ProgressTrackBar.Location = new Point(12, 450);
        ProgressTrackBar.Name = "ProgressTrackBar";
        ProgressTrackBar.Size = new Size(637, 45);
        ProgressTrackBar.TabIndex = 13;
        ProgressTrackBar.KeyDown += ProgressTrackBar_KeyDown;
        ProgressTrackBar.MouseDown += ProgressTrackBar_MouseDown;
        ProgressTrackBar.MouseUp += ProgressTrackBar_MouseUp;
        ProgressTrackBar.MouseWheel += ProgressTrackBar_MouseWheel;
        // 
        // GridView
        // 
        GridView.AllowUserToAddRows = false;
        GridView.AllowUserToDeleteRows = false;
        GridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        GridView.Location = new Point(12, 12);
        GridView.Name = "GridView";
        GridView.ReadOnly = true;
        GridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
        GridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
        GridView.Size = new Size(637, 338);
        GridView.TabIndex = 14;
        GridView.SelectionChanged += GridView_SelectionChanged;
        // 
        // ThreatTrackBar
        // 
        ThreatTrackBar.Enabled = false;
        ThreatTrackBar.Location = new Point(12, 356);
        ThreatTrackBar.Maximum = 100;
        ThreatTrackBar.Name = "ThreatTrackBar";
        ThreatTrackBar.Size = new Size(637, 45);
        ThreatTrackBar.TabIndex = 15;
        ThreatTrackBar.ValueChanged += ThreatTrackBar_ValueChanged;
        // 
        // ThreatLabel
        // 
        ThreatLabel.AutoSize = true;
        ThreatLabel.Location = new Point(655, 356);
        ThreatLabel.Name = "ThreatLabel";
        ThreatLabel.Size = new Size(53, 15);
        ThreatLabel.TabIndex = 16;
        ThreatLabel.Text = "Threat %";
        // 
        // ProgressLabel
        // 
        ProgressLabel.AutoSize = true;
        ProgressLabel.Location = new Point(655, 450);
        ProgressLabel.Name = "ProgressLabel";
        ProgressLabel.Size = new Size(82, 15);
        ProgressLabel.TabIndex = 17;
        ProgressLabel.Text = "Song progress";
        // 
        // RerollButton
        // 
        RerollButton.Enabled = false;
        RerollButton.Location = new Point(655, 169);
        RerollButton.Name = "RerollButton";
        RerollButton.Size = new Size(105, 23);
        RerollButton.TabIndex = 18;
        RerollButton.Text = "Randomize";
        RerollButton.UseVisualStyleBackColor = true;
        RerollButton.Click += RerollButton_Click;
        // 
        // ThreatPercentageLabel
        // 
        ThreatPercentageLabel.AutoSize = true;
        ThreatPercentageLabel.Location = new Point(714, 356);
        ThreatPercentageLabel.Name = "ThreatPercentageLabel";
        ThreatPercentageLabel.Size = new Size(13, 15);
        ThreatPercentageLabel.TabIndex = 19;
        ThreatPercentageLabel.Text = "0";
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(771, 493);
        Controls.Add(ThreatPercentageLabel);
        Controls.Add(RerollButton);
        Controls.Add(ProgressLabel);
        Controls.Add(ThreatLabel);
        Controls.Add(ThreatTrackBar);
        Controls.Add(GridView);
        Controls.Add(ProgressTrackBar);
        Controls.Add(ConfigFileButton);
        Controls.Add(VolumeSlider);
        Controls.Add(OpenFileButton);
        Controls.Add(StopButton);
        Controls.Add(PlayButton);
        Name = "MainForm";
        Text = "Visualizer";
        ((System.ComponentModel.ISupportInitialize)ProgressTrackBar).EndInit();
        ((System.ComponentModel.ISupportInitialize)GridView).EndInit();
        ((System.ComponentModel.ISupportInitialize)ThreatTrackBar).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Button PlayButton;
    private Button StopButton;
    private Button OpenFileButton;
    private NAudio.Gui.VolumeSlider VolumeSlider;
    private Button ConfigFileButton;
    private TrackBar ProgressTrackBar;
    private DataGridView GridView;
    private TrackBar ThreatTrackBar;
    private Label ThreatLabel;
    private Label ProgressLabel;
    private Button RerollButton;
    private Label ThreatPercentageLabel;
}
