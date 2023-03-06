namespace BetterInputMacros
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is a gross override of the Form's process message function that captures windows messages.
        /// Filter out the hotkey messages we care about and use them to do stuff
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            // If we get a hotkey message, filter it out and don't pass it to the base function,
            // instead pass it to the hotkey helper
            if (m.Msg == 0x312) // WM_HOTKEY
            {
                HotkeyHelper.ProcessMessage(m);

                return;
            }

            base.WndProc(ref m);
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            HotkeyHelper.RegisterFunctionKeys(this.Handle);
        }

        private void unregisterButton_Click(object sender, EventArgs e)
        {
            HotkeyHelper.UnregisterFunctionKeys(this.Handle);
        }
    }
}