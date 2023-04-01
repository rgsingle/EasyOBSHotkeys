using EasyOBSHotkeys.Utilities;
using System.Windows.Forms;

namespace BetterInputMacros
{
    public partial class MainForm : Form
    {
        AppConfiguration? configuration;
        readonly HotkeyHelper _hotkeyHelper;
        readonly ObsHelper _obsHelper;

        int connectionAttempts = 0;

        public MainForm()
        {
            InitializeComponent();

            _hotkeyHelper = new HotkeyHelper(this.Handle);

            _obsHelper = new ObsHelper();
            _obsHelper.ConnectedEvent += obsHelper_OnConnected;
            _obsHelper.DisconnectedEvent += obsHelper_OnDisconnected;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load Config
            configuration = AppConfiguration.LoadConfig();

            if (configuration?.Server == null)
            {
                Application.Exit();
                return;
            }

            // Connect to OBS
            bool obs = _obsHelper.Connect(configuration.Server, configuration.Password ?? "");

            if (obs == false)
            {
                Application.Exit();
                return;
            }

            // Register Hotkeys
            int keys = _hotkeyHelper.RegisterHotKeys(configuration.CommandsDictionary.Keys.ToArray());

            if (keys <= 0)
            {
                if (keys == 0)
                    MessageBox.Show("No hotkeys registered! We will now close so you can go add some hotkeys.", "No Hotkeys?",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                Application.Exit();
                return;
            }
        }

        /// <summary>
        /// This is a gross override of the Form's process message function that captures windows messages.
        /// Filter out the hotkey messages we care about and use them to do stuff
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            // If we get a hotkey message, filter it out and don't pass it to the base function,
            // instead switch to the scene it represents
            if (m.Msg == 0x312) // WM_HOTKEY
            {
                int id = m.WParam.ToInt32();

                if (!_obsHelper.IsConnected)
                    return;

                if (configuration?.CommandsDictionary.TryGetValue((uint)id, out var scene) == true &&
                    !string.IsNullOrWhiteSpace(scene))
                {
                    _obsHelper.SetScene(scene);
                }
            }

            base.WndProc(ref m);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Disconnect and unregister
            _obsHelper.Disconnect();

            _hotkeyHelper.UnregisterHotKeys();
        }

        private void obsHelper_OnConnected(object? sender, EventArgs e)
        {
            obsStatusLabel.BeginInvoke(() => obsStatusLabel.Text = "OBS: Connected");

            connectionAttempts = 0;
        }

        private void obsHelper_OnDisconnected(object? sender, string e)
        {
            connectionAttempts++;
            obsStatusLabel.BeginInvoke(() => obsStatusLabel.Text = "OBS: Disconnected");

            if (connectionAttempts > 5)
            {
                MessageBox.Show("Lost connection to OBS! Probably because OBS closed or smth, idk.", "Uh oh, Stinky",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                Application.Exit();
            }
            else if (e == null && configuration?.Server != null)
            {
                _obsHelper.Connect(configuration.Server, configuration.Password ?? "");
            }
            else if (e != "User requested disconnect" && e != null)
            {
                if (e.StartsWith("Authentication"))
                {
                    MessageBox.Show("OBS authentication failed!", "OBS Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }
    }
}