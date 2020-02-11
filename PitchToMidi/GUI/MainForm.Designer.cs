namespace PitchToMidi.GUI {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.mainTab = new System.Windows.Forms.TabPage();
            this.audioTab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.settingsButton = new System.Windows.Forms.Button();
            this.playSoundButton = new System.Windows.Forms.Button();
            this.analyzeAudioButton = new System.Windows.Forms.Button();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.waveViewer1 = new NAudio.Gui.WaveViewer();
            this.analyzeTab = new System.Windows.Forms.TabPage();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.frequencyChart = new PitchToMidi.GUI.FrequencyChart();
            this.playMidiButton = new System.Windows.Forms.Button();
            this.generateAudioButton = new System.Windows.Forms.Button();
            this.mainTabs.SuspendLayout();
            this.audioTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.analyzeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyChart)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabs
            // 
            this.mainTabs.Controls.Add(this.mainTab);
            this.mainTabs.Controls.Add(this.audioTab);
            this.mainTabs.Controls.Add(this.analyzeTab);
            this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabs.Location = new System.Drawing.Point(0, 0);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
            this.mainTabs.Size = new System.Drawing.Size(800, 450);
            this.mainTabs.TabIndex = 0;
            // 
            // mainTab
            // 
            this.mainTab.BackColor = System.Drawing.Color.Transparent;
            this.mainTab.Location = new System.Drawing.Point(4, 22);
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Windows.Forms.Padding(3);
            this.mainTab.Size = new System.Drawing.Size(792, 424);
            this.mainTab.TabIndex = 0;
            this.mainTab.Text = "Main";
            // 
            // audioTab
            // 
            this.audioTab.BackColor = System.Drawing.Color.Transparent;
            this.audioTab.Controls.Add(this.splitContainer1);
            this.audioTab.Location = new System.Drawing.Point(4, 22);
            this.audioTab.Name = "audioTab";
            this.audioTab.Padding = new System.Windows.Forms.Padding(3);
            this.audioTab.Size = new System.Drawing.Size(792, 424);
            this.audioTab.TabIndex = 1;
            this.audioTab.Text = "Audio";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.waveViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(786, 418);
            this.splitContainer1.SplitterDistance = 209;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.settingsButton);
            this.splitContainer2.Panel2.Controls.Add(this.playSoundButton);
            this.splitContainer2.Panel2.Controls.Add(this.analyzeAudioButton);
            this.splitContainer2.Panel2.Controls.Add(this.loadFileButton);
            this.splitContainer2.Size = new System.Drawing.Size(786, 209);
            this.splitContainer2.SplitterDistance = 652;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.fileNameLabel);
            this.splitContainer3.Size = new System.Drawing.Size(652, 209);
            this.splitContainer3.SplitterDistance = 104;
            this.splitContainer3.TabIndex = 0;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(5, 20);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(65, 13);
            this.fileNameLabel.TabIndex = 0;
            this.fileNameLabel.Text = "Loaded file: ";
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(2, 159);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(122, 46);
            this.settingsButton.TabIndex = 3;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            // 
            // playSoundButton
            // 
            this.playSoundButton.Location = new System.Drawing.Point(2, 55);
            this.playSoundButton.Name = "playSoundButton";
            this.playSoundButton.Size = new System.Drawing.Size(122, 46);
            this.playSoundButton.TabIndex = 2;
            this.playSoundButton.Text = "Play audio";
            this.playSoundButton.UseVisualStyleBackColor = true;
            // 
            // analyzeAudioButton
            // 
            this.analyzeAudioButton.Location = new System.Drawing.Point(2, 107);
            this.analyzeAudioButton.Name = "analyzeAudioButton";
            this.analyzeAudioButton.Size = new System.Drawing.Size(122, 46);
            this.analyzeAudioButton.TabIndex = 1;
            this.analyzeAudioButton.Text = "Analyze frequency data";
            this.analyzeAudioButton.UseVisualStyleBackColor = true;
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(3, 3);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(122, 46);
            this.loadFileButton.TabIndex = 0;
            this.loadFileButton.Text = "Load file";
            this.loadFileButton.UseVisualStyleBackColor = true;
            // 
            // waveViewer1
            // 
            this.waveViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waveViewer1.Location = new System.Drawing.Point(0, 0);
            this.waveViewer1.Name = "waveViewer1";
            this.waveViewer1.SamplesPerPixel = 128;
            this.waveViewer1.Size = new System.Drawing.Size(786, 205);
            this.waveViewer1.StartPosition = ((long)(0));
            this.waveViewer1.TabIndex = 0;
            this.waveViewer1.WaveStream = null;
            // 
            // analyzeTab
            // 
            this.analyzeTab.BackColor = System.Drawing.Color.Transparent;
            this.analyzeTab.Controls.Add(this.splitContainer4);
            this.analyzeTab.Location = new System.Drawing.Point(4, 22);
            this.analyzeTab.Name = "analyzeTab";
            this.analyzeTab.Padding = new System.Windows.Forms.Padding(3);
            this.analyzeTab.Size = new System.Drawing.Size(792, 424);
            this.analyzeTab.TabIndex = 2;
            this.analyzeTab.Text = "Analysis";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.frequencyChart);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.playMidiButton);
            this.splitContainer4.Panel2.Controls.Add(this.generateAudioButton);
            this.splitContainer4.Size = new System.Drawing.Size(786, 418);
            this.splitContainer4.SplitterDistance = 626;
            this.splitContainer4.TabIndex = 0;
            // 
            // frequencyChart
            // 
            this.frequencyChart.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.frequencyChart.ChartAreas.Add(chartArea1);
            this.frequencyChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.frequencyChart.Legends.Add(legend1);
            this.frequencyChart.Location = new System.Drawing.Point(0, 0);
            this.frequencyChart.Name = "frequencyChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.frequencyChart.Series.Add(series1);
            this.frequencyChart.Size = new System.Drawing.Size(626, 418);
            this.frequencyChart.TabIndex = 0;
            this.frequencyChart.Text = "frequencyChart1";
            // 
            // playMidiButton
            // 
            this.playMidiButton.Location = new System.Drawing.Point(3, 60);
            this.playMidiButton.Name = "playMidiButton";
            this.playMidiButton.Size = new System.Drawing.Size(151, 51);
            this.playMidiButton.TabIndex = 2;
            this.playMidiButton.Text = "Play as MIDI";
            this.playMidiButton.UseVisualStyleBackColor = true;
            // 
            // generateAudioButton
            // 
            this.generateAudioButton.Location = new System.Drawing.Point(2, 3);
            this.generateAudioButton.Name = "generateAudioButton";
            this.generateAudioButton.Size = new System.Drawing.Size(151, 51);
            this.generateAudioButton.TabIndex = 1;
            this.generateAudioButton.Text = "Generate audio";
            this.generateAudioButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainTabs);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.mainTabs.ResumeLayout(false);
            this.audioTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.analyzeTab.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.frequencyChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabs;
        private System.Windows.Forms.TabPage mainTab;
        private System.Windows.Forms.TabPage audioTab;
        private NAudio.Gui.WaveViewer waveViewer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Button analyzeAudioButton;
        private System.Windows.Forms.TabPage analyzeTab;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private FrequencyChart frequencyChart;
        private System.Windows.Forms.Button playSoundButton;
        private System.Windows.Forms.Button generateAudioButton;
        private System.Windows.Forms.Button playMidiButton;
        private System.Windows.Forms.Button settingsButton;
    }
}