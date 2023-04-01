namespace BetterInputMacros
{
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
            selectFileDialog = new SaveFileDialog();
            obsStatusLabel = new Label();
            SuspendLayout();
            // 
            // selectFileDialog
            // 
            selectFileDialog.AddExtension = false;
            selectFileDialog.DefaultExt = "txt";
            selectFileDialog.Filter = "Text Files|*.txt|All files|*.*";
            // 
            // obsStatusLabel
            // 
            obsStatusLabel.AutoSize = true;
            obsStatusLabel.Location = new Point(12, 9);
            obsStatusLabel.Name = "obsStatusLabel";
            obsStatusLabel.Size = new Size(107, 15);
            obsStatusLabel.TabIndex = 3;
            obsStatusLabel.Text = "OBS: Disconnected";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(305, 38);
            Controls.Add(obsStatusLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Easy OBS Hotkeys";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private SaveFileDialog selectFileDialog;
        private Label obsStatusLabel;
    }
}