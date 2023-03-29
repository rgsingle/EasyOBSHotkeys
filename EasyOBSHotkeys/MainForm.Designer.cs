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
            statusStrip = new StatusStrip();
            obsStatusLabel = new ToolStripStatusLabel();
            enableCheckbox = new CheckBox();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // selectFileDialog
            // 
            selectFileDialog.AddExtension = false;
            selectFileDialog.DefaultExt = "txt";
            selectFileDialog.Filter = "Text Files|*.txt|All files|*.*";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { obsStatusLabel });
            statusStrip.Location = new Point(0, 45);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(305, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // obsStatusLabel
            // 
            obsStatusLabel.Name = "obsStatusLabel";
            obsStatusLabel.Size = new Size(107, 17);
            obsStatusLabel.Text = "OBS: Disconnected";
            // 
            // enableCheckbox
            // 
            enableCheckbox.AutoSize = true;
            enableCheckbox.Location = new Point(12, 15);
            enableCheckbox.Name = "enableCheckbox";
            enableCheckbox.Size = new Size(108, 19);
            enableCheckbox.TabIndex = 3;
            enableCheckbox.Text = "Enable HotKeys";
            enableCheckbox.UseVisualStyleBackColor = true;
            enableCheckbox.CheckedChanged += enableCheckbox_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(305, 67);
            Controls.Add(enableCheckbox);
            Controls.Add(statusStrip);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "Easy OBS Hotkeys";
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private SaveFileDialog selectFileDialog;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel obsStatusLabel;
        private CheckBox enableCheckbox;
    }
}