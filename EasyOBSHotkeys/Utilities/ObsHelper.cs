using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Communication;
using System.Runtime.CompilerServices;

namespace EasyOBSHotkeys.Utilities
{
    internal class ObsHelper
    {
        private readonly OBSWebsocket _obs;

        public bool IsConnected => _obs?.IsConnected ?? false;

        public event EventHandler? ConnectedEvent;
        public event EventHandler<string>? DisconnectedEvent;

        public ObsHelper()
        {
            _obs = new OBSWebsocket
            {
                WSTimeout = TimeSpan.FromSeconds(3)
            };

            _obs.Connected += Obs_Connected;
            _obs.Disconnected += Obs_Disconnected;
        }

        private void Obs_Disconnected(object? sender, ObsDisconnectionInfo e)
        {
            DisconnectedEvent?.Invoke(this, e.DisconnectReason);
        }

        private void Obs_Connected(object? sender, EventArgs e)
        {
            ConnectedEvent?.Invoke(this, EventArgs.Empty);
        }

        public bool Connect(string host, string? password)
        {
            try
            {
                if (!host.StartsWith("ws://"))
                    host = "ws://" + host;
                
                _obs.ConnectAsync(host, password ?? "");

                return true;
            }
            catch(Exception) 
            {
                MessageBox.Show("An error occurred connecting to the OBS WebSocket!", "OBS Connection Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public void SetScene(string scene)
        {
            try
            {
                _obs.SetCurrentProgramScene(scene);
            }
            catch
            {
                // TODO: Logging

                MessageBox.Show($"Errrrr, failed to switch to scene '{scene}'...", "Scene Switch F",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Disconnect()
        {
            if(_obs.IsConnected)
                _obs.Disconnect();
        }
    }
}
