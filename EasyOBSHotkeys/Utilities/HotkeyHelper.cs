using System.Runtime.InteropServices;

namespace EasyOBSHotkeys.Utilities
{
    /// <summary>
    /// Just a helper because I am lazy and want to be mildly organized
    /// </summary>
    internal class HotkeyHelper
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

        public event EventHandler<uint>? HotKeyEvent;

        private readonly List<uint> _registeredHotkeys;
        private readonly IntPtr _windowHandle;

        public HotkeyHelper(IntPtr windowHandle)
        {
            _registeredHotkeys = new List<uint>();
            _windowHandle = windowHandle;
        }


        public void RegisterHotKeys(uint[] keyCodes)
        {
            _registeredHotkeys.Clear();

            // Register Hotkeys
            foreach (var keyCode in keyCodes)
            {
                RegisterHotKey(_windowHandle, (int)keyCode, KeyModifiers.None, keyCode);
                _registeredHotkeys.Add(keyCode);
            }
        }

        public void UnregisterHotKeys()
        {
            foreach (var hotkey in _registeredHotkeys)
            {
                UnregisterHotKey(_windowHandle, (int)hotkey);
            }

            _registeredHotkeys.Clear();
        }

        public void ProcessMessage(Message message)
        {
            // We're going to do the laziest possible job of file creation
            int id = message.WParam.ToInt32();

            HotKeyEvent?.Invoke(null, (uint)id);
        }
    }
}
