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

        public void Connect(string host, string? password)
        {
            try
            {
                if (!host.StartsWith("ws://"))
                    host = "ws://" + host;
                
                _obs.ConnectAsync(host, password ?? "");
            }
            catch(Exception ex) 
            {
                // TODO: Logging
            }
        }

        public void SetScene(string scene)
        {
            try
            {
                if (!_obs.IsConnected)
                    return;

                _obs.SetCurrentProgramScene(scene);
            }
            catch(Exception  ex)
            {
                // TODO: Logging
            }
        }

        public void Disconnect()
        {
            if(_obs.IsConnected)
                _obs.Disconnect();
        }
    }
}
