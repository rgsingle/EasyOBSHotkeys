using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace BetterInputMacros
{
    /// <summary>
    /// Just a helper because I am lazy and want to be mildly organized
    /// </summary>
    internal static class HotkeyHelper
    {
        #region User32 API Stuff - Not Exported Outside This Class

        [Flags]
        internal enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public UIntPtr wParam;
            public IntPtr lParam;
            public int time;
            public POINT pt;
            public int lPrivate;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, KeyModifiers fsModifiers, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion

        const string appsettingsFilename = "appsettings.json";

        private static string? server;
        private static string? password = null;
        private static readonly Dictionary<int, Action> commands = new();

        public static void RegisterFunctionKeys(IntPtr handle)
        {
            try
            {
                commands.Clear();

                var appsettings = JsonDocument.Parse(File.ReadAllText(appsettingsFilename), new JsonDocumentOptions()
                {
                    AllowTrailingCommas = true,
                    CommentHandling = JsonCommentHandling.Skip
                });

                // Load Server
                if (appsettings.RootElement.TryGetProperty("server", out var serverProp) &&
                    serverProp.ValueKind == JsonValueKind.String)
                    server = serverProp.GetString();
                else
                    server = null;

                server ??= "127.0.0.1:4444";

                // Load Password
                if (appsettings.RootElement.TryGetProperty("password", out var passProp) && 
                    passProp.ValueKind == JsonValueKind.String)
                    password = passProp.GetString();
                else
                    password = null;

                // Register Hotkeys
                if(appsettings.RootElement.TryGetProperty("commands", out var commandsProp))
                {
                    foreach(var commandProp in commandsProp.EnumerateArray())
                    {
                        if(commandProp.TryGetProperty("keycode", out var keycodeProp) &&
                            keycodeProp.ValueKind == JsonValueKind.Number &&
                           commandProp.TryGetProperty("scene", out var sceneProp) &&
                           sceneProp.ValueKind == JsonValueKind.String)
                        {
                            var keycode = keycodeProp.GetInt32();

                            var action = () =>
                            {
                                var args = $"/server={server}";
                                if (password != null)
                                    args += $" /password={password}";

                                args += $" /scene=\"{sceneProp.GetString()}\"";

                                try
                                {
                                    var obscommand = Process.Start(new ProcessStartInfo("OBSCommand.exe")
                                    {
                                        Arguments = args,
                                        RedirectStandardOutput = true,
                                        WindowStyle = ProcessWindowStyle.Hidden,
                                        CreateNoWindow = true
                                    });

                                    if (obscommand != null)
                                    {
                                        obscommand?.WaitForExit();

                                        var result = obscommand?.StandardOutput.ReadToEnd();

                                        if (result != "Ok")
                                        {
                                            MessageBox.Show(result, "Error");
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString(), "Error Running OBSCommand");
                                }
                            };

                            commands.Add(keycode, action);
                            RegisterHotKey(handle, keycode, KeyModifiers.None, (uint)keycode);
                        }
                    }
                }

                MessageBox.Show("Registered!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Oops: " + ex.ToString());
            }
        }

        public static void UnregisterFunctionKeys(IntPtr handle)
        {
            foreach(var item in commands)
            {
                UnregisterHotKey(handle, item.Key);
            }
        }

        public static void ProcessMessage(Message message)
        {
            // We're going to do the laziest possible job of file creation
            int id = message.WParam.ToInt32();

            if(commands.TryGetValue(id, out var action))
            {
                action.Invoke();
            }
        }
    }
}
