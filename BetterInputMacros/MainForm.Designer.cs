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
            registerButton = new Button();
            unregisterButton = new Button();
            SuspendLayout();
            // 
            // selectFileDialog
            // 
            selectFileDialog.AddExtension = false;
            selectFileDialog.DefaultExt = "txt";
            selectFileDialog.Filter = "Text Files|*.txt|All files|*.*";
            // 
            // registerButton
            // 
            registerButton.Location = new Point(12, 12);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(137, 23);
            registerButton.TabIndex = 0;
            registerButton.Text = "Register Hotkeys";
            registerButton.UseVisualStyleBackColor = true;
            registerButton.Click += registerButton_Click;
            // 
            // unregisterButton
            // 
            unregisterButton.Location = new Point(155, 12);
            unregisterButton.Name = "unregisterButton";
            unregisterButton.Size = new Size(137, 23);
            unregisterButton.TabIndex = 1;
            unregisterButton.Text = "Unregister Hotkeys";
            unregisterButton.UseVisualStyleBackColor = true;
            unregisterButton.Click += unregisterButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(305, 48);
            Controls.Add(unregisterButton);
            Controls.Add(registerButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainForm";
            Text = "Better Input Macros";
            ResumeLayout(false);
        }

        #endregion
        private SaveFileDialog selectFileDialog;
        private Button registerButton;
        private Button unregisterButton;
    }
}