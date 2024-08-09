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
        ListBox = new ListBox();
        IncreaseButton = new Button();
        DecreaseButton = new Button();
        ThreatTitleLabel = new Label();
        ThreatLevelLabel = new Label();
        ConfigFileButton = new Button();
        ProgressTrackBar = new TrackBar();
        ((System.ComponentModel.ISupportInitialize)ProgressTrackBar).BeginInit();
        SuspendLayout();
        // 
        // PlayButton
        // 
        PlayButton.Enabled = false;
        PlayButton.Location = new Point(12, 289);
        PlayButton.Name = "PlayButton";
        PlayButton.Size = new Size(75, 23);
        PlayButton.TabIndex = 1;
        PlayButton.Text = "Play";
        PlayButton.UseVisualStyleBackColor = true;
        PlayButton.Click += PlayButton_Click;
        // 
        // StopButton
        // 
        StopButton.Enabled = false;
        StopButton.Location = new Point(12, 318);
        StopButton.Name = "StopButton";
        StopButton.Size = new Size(75, 23);
        StopButton.TabIndex = 2;
        StopButton.Text = "Stop";
        StopButton.UseVisualStyleBackColor = true;
        StopButton.Click += StopButton_Click;
        // 
        // OpenFileButton
        // 
        OpenFileButton.Location = new Point(342, 318);
        OpenFileButton.Name = "OpenFileButton";
        OpenFileButton.Size = new Size(116, 23);
        OpenFileButton.TabIndex = 3;
        OpenFileButton.Text = "Open files";
        OpenFileButton.UseVisualStyleBackColor = true;
        OpenFileButton.Click += OpenFileButton_Click;
        // 
        // VolumeSlider
        // 
        VolumeSlider.ImeMode = ImeMode.Katakana;
        VolumeSlider.Location = new Point(12, 238);
        VolumeSlider.Name = "VolumeSlider";
        VolumeSlider.Size = new Size(365, 45);
        VolumeSlider.TabIndex = 5;
        VolumeSlider.VolumeChanged += VolumeSlider_VolumeChanged;
        // 
        // ListBox
        // 
        ListBox.AllowDrop = true;
        ListBox.FormattingEnabled = true;
        ListBox.ItemHeight = 15;
        ListBox.Location = new Point(12, 12);
        ListBox.Name = "ListBox";
        ListBox.Size = new Size(365, 169);
        ListBox.TabIndex = 6;
        ListBox.DragDrop += ListBox_DragDrop;
        ListBox.DragOver += ListBox_DragOver;
        ListBox.MouseDown += ListBox_MouseDown;
        // 
        // IncreaseButton
        // 
        IncreaseButton.Location = new Point(383, 12);
        IncreaseButton.Name = "IncreaseButton";
        IncreaseButton.Size = new Size(75, 23);
        IncreaseButton.TabIndex = 8;
        IncreaseButton.Text = "+";
        IncreaseButton.UseVisualStyleBackColor = true;
        IncreaseButton.Click += IncreaseButton_Click;
        // 
        // DecreaseButton
        // 
        DecreaseButton.Location = new Point(383, 158);
        DecreaseButton.Name = "DecreaseButton";
        DecreaseButton.Size = new Size(75, 23);
        DecreaseButton.TabIndex = 9;
        DecreaseButton.Text = "-";
        DecreaseButton.UseVisualStyleBackColor = true;
        DecreaseButton.Click += DecreaseButton_Click;
        // 
        // ThreatTitleLabel
        // 
        ThreatTitleLabel.AutoSize = true;
        ThreatTitleLabel.Location = new Point(383, 74);
        ThreatTitleLabel.Name = "ThreatTitleLabel";
        ThreatTitleLabel.Size = new Size(70, 15);
        ThreatTitleLabel.TabIndex = 10;
        ThreatTitleLabel.Text = "Threat Level";
        // 
        // ThreatLevelLabel
        // 
        ThreatLevelLabel.AutoSize = true;
        ThreatLevelLabel.Location = new Point(383, 98);
        ThreatLevelLabel.Name = "ThreatLevelLabel";
        ThreatLevelLabel.Size = new Size(30, 15);
        ThreatLevelLabel.TabIndex = 11;
        ThreatLevelLabel.Text = "0 / 0";
        // 
        // ConfigFileButton
        // 
        ConfigFileButton.Enabled = false;
        ConfigFileButton.Location = new Point(342, 289);
        ConfigFileButton.Name = "ConfigFileButton";
        ConfigFileButton.Size = new Size(116, 23);
        ConfigFileButton.TabIndex = 12;
        ConfigFileButton.Text = "Read config file";
        ConfigFileButton.UseVisualStyleBackColor = true;
        // 
        // ProgressTrackBar
        // 
        ProgressTrackBar.Enabled = false;
        ProgressTrackBar.Location = new Point(12, 187);
        ProgressTrackBar.Maximum = 1;
        ProgressTrackBar.Name = "ProgressTrackBar";
        ProgressTrackBar.Size = new Size(365, 45);
        ProgressTrackBar.TabIndex = 13;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(469, 355);
        Controls.Add(ProgressTrackBar);
        Controls.Add(ConfigFileButton);
        Controls.Add(ThreatLevelLabel);
        Controls.Add(ThreatTitleLabel);
        Controls.Add(DecreaseButton);
        Controls.Add(IncreaseButton);
        Controls.Add(ListBox);
        Controls.Add(VolumeSlider);
        Controls.Add(OpenFileButton);
        Controls.Add(StopButton);
        Controls.Add(PlayButton);
        Name = "MainForm";
        Text = "Visualizer";
        ((System.ComponentModel.ISupportInitialize)ProgressTrackBar).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Button PlayButton;
    private Button StopButton;
    private Button OpenFileButton;
    private NAudio.Gui.VolumeSlider VolumeSlider;
    private ListBox ListBox;
    private Button IncreaseButton;
    private Button DecreaseButton;
    private Label ThreatTitleLabel;
    private Label ThreatLevelLabel;
    private Button ConfigFileButton;
    private TrackBar ProgressTrackBar;
}
