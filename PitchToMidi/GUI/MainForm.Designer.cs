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
            this.fader1 = new NAudio.Gui.Fader();
            this.SuspendLayout();
            // 
            // fader1
            // 
            this.fader1.Location = new System.Drawing.Point(382, 51);
            this.fader1.Maximum = 0;
            this.fader1.Minimum = 0;
            this.fader1.Name = "fader1";
            this.fader1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.fader1.Size = new System.Drawing.Size(8, 8);
            this.fader1.TabIndex = 0;
            this.fader1.Text = "fader1";
            this.fader1.Value = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.fader1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private NAudio.Gui.Fader fader1;
    }
}