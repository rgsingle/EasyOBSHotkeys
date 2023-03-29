using EasyOBSHotkeys.Utilities;

namespace BetterInputMacros
{
    public partial class MainForm : Form
    {
        AppConfiguration? configuration;
        readonly HotkeyHelper _hotkeyHelper;
        readonly ObsHelper _obsHelper;
        readonly FileSystemWatcher _watcher;

        public MainForm()
        {
            InitializeComponent();

            _hotkeyHelper = new HotkeyHelper(this.Handle);
            _hotkeyHelper.HotKeyEvent += hotkeyHelper_HotKeyEvent;

            _obsHelper = new ObsHelper();
            _obsHelper.ConnectedEvent += obsHelper_OnConnected;
            _obsHelper.DisconnectedEvent += obsHelper_OnDisconnected;

            _watcher = new FileSystemWatcher();
            _watcher.Changed += fileSystemWatcher_Changed;
            _watcher.Filter = "*.json";
            _watcher.Path = Path.GetDirectoryName(Application.ExecutablePath) ?? "";
            _watcher.EnableRaisingEvents = true;

            configuration = AppConfiguration.LoadConfig();
        }

        private void enableCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (enableCheckbox.Checked && configuration != null)
            {
                if (configuration.Server != null)
                {
                    if (_obsHelper.IsConnected)
                        _obsHelper.Disconnect();

                    _obsHelper.Connect(configuration.Server, configuration.Password ?? "");
                }

                _hotkeyHelper.RegisterHotKeys(configuration.CommandsDictionary.Keys.ToArray());
            }
            else if (enableCheckbox.Checked == false)
            {
                _obsHelper.Disconnect();

                _hotkeyHelper.UnregisterHotKeys();
            }
        }

        private void unregisterButton_Click(object sender, EventArgs e)
        {
            if (_obsHelper.IsConnected)
                _obsHelper.Disconnect();

            _hotkeyHelper.UnregisterHotKeys();
        }

        private void hotkeyHelper_HotKeyEvent(object? sender, uint e)
        {
            if (!_obsHelper.IsConnected)
                return;

            if (configuration?.CommandsDictionary.TryGetValue(e, out var scene) == true &&
                !string.IsNullOrWhiteSpace(scene))
            {
                _obsHelper.SetScene(scene);
            }
        }

        private void obsHelper_OnDisconnected(object? sender, string e)
        {
            obsStatusLabel.Text = "OBS: Disconnected";

            if (e == null && configuration?.Server != null)
            {
                _obsHelper.Connect(configuration.Server, configuration.Password ?? "");
            }
            else if (e != "User requested disconnect" && e != null)
            {
                if (e.StartsWith("Authentication"))
                    MessageBox.Show("OBS authentication failed!", "OBS Error");
                else
                    MessageBox.Show("OBS disconnected unexpectedly: " + e, "OBS Error");

                enableCheckbox.BeginInvoke(() => enableCheckbox.Checked = false);
            }
        }

        private void obsHelper_OnConnected(object? sender, EventArgs e)
        {
            obsStatusLabel.Text = "OBS: Connected";
        }

        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e?.Name?.ToLower() != "appsettings.json") 
                return;

            // Configuration has changed, disconnect/unregister and reload
            if (enableCheckbox.Checked)
                enableCheckbox.BeginInvoke(() => enableCheckbox.Checked = false);

            configuration = AppConfiguration.LoadConfig();
            MessageBox.Show("appsettings.json updated, please re-enable hotkeys!", "Settings Updated");
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
                _hotkeyHelper.ProcessMessage(m);

                return;
            }

            base.WndProc(ref m);
        }
    }
}