using System.Runtime.InteropServices;

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

        public static void RegisterFunctionKeys(IntPtr handle)
        {
            // Register hotkeys for the F13-F24 keys, set to id's 13-24
            RegisterHotKey(handle, 13, KeyModifiers.None, 124);
            RegisterHotKey(handle, 14, KeyModifiers.None, 125);
            RegisterHotKey(handle, 15, KeyModifiers.None, 126);
            RegisterHotKey(handle, 16, KeyModifiers.None, 127);
            RegisterHotKey(handle, 17, KeyModifiers.None, 128);
            RegisterHotKey(handle, 18, KeyModifiers.None, 129);
            RegisterHotKey(handle, 19, KeyModifiers.None, 130);
            RegisterHotKey(handle, 20, KeyModifiers.None, 131);
            RegisterHotKey(handle, 21, KeyModifiers.None, 132);
            RegisterHotKey(handle, 22, KeyModifiers.None, 133);
            RegisterHotKey(handle, 23, KeyModifiers.None, 134);
            RegisterHotKey(handle, 24, KeyModifiers.None, 135);
        }

        public static void UnregisterFunctionKeys(IntPtr handle)
        {
            // Unregister hotkeys for the F13-F24 keys, set to id's 13-24
            UnregisterHotKey(handle, 13);
            UnregisterHotKey(handle, 14);
            UnregisterHotKey(handle, 15);
            UnregisterHotKey(handle, 16);
            UnregisterHotKey(handle, 17);
            UnregisterHotKey(handle, 18);
            UnregisterHotKey(handle, 19);
            UnregisterHotKey(handle, 20);
            UnregisterHotKey(handle, 21);
            UnregisterHotKey(handle, 22);
            UnregisterHotKey(handle, 23);
            UnregisterHotKey(handle, 24);
        }

        public static void ProcessMessage(Message message)
        {
            // We're going to do the laziest possible job of file creation
            int id = message.WParam.ToInt32();

            if (id >= 13 && id <= 24)
            {
                File.WriteAllText($"F{id}.txt", $"F{id}");
            }
        }
    }
}
