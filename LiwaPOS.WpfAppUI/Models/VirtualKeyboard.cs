using LiwaPOS.WpfAppUI.Helpers;
using System.Runtime.InteropServices;

namespace LiwaPOS.WpfAppUI.Models
{
    public class VirtualKeyboard
    {
        public VirtualKey KeyA { get; set; }
        public VirtualKey KeyB { get; set; }
        public VirtualKey KeyC { get; set; }
        public VirtualKey KeyCTr { get; set; }
        public VirtualKey KeyD { get; set; }
        public VirtualKey KeyE { get; set; }
        public VirtualKey KeyF { get; set; }
        public VirtualKey KeyG { get; set; }
        public VirtualKey KeyGTr { get; set; }
        public VirtualKey KeyH { get; set; }
        public VirtualKey KeyI { get; set; }
        public VirtualKey KeyITr { get; set; }
        public VirtualKey KeyJ { get; set; }
        public VirtualKey KeyK { get; set; }
        public VirtualKey KeyL { get; set; }
        public VirtualKey KeyM { get; set; }
        public VirtualKey KeyN { get; set; }
        public VirtualKey KeyO { get; set; }
        public VirtualKey KeyOTr { get; set; }
        public VirtualKey KeyP { get; set; }
        public VirtualKey KeyQ { get; set; }
        public VirtualKey KeyR { get; set; }
        public VirtualKey KeyS { get; set; }
        public VirtualKey KeySTr { get; set; }
        public VirtualKey KeyT { get; set; }
        public VirtualKey KeyU { get; set; }
        public VirtualKey KeyUTr { get; set; }
        public VirtualKey KeyV { get; set; }
        public VirtualKey KeyW { get; set; }
        public VirtualKey KeyX { get; set; }
        public VirtualKey KeyY { get; set; }
        public VirtualKey KeyZ { get; set; }
        public VirtualKey Key1 { get; set; }
        public VirtualKey Key2 { get; set; }
        public VirtualKey Key3 { get; set; }
        public VirtualKey Key4 { get; set; }
        public VirtualKey Key5 { get; set; }
        public VirtualKey Key6 { get; set; }
        public VirtualKey Key7 { get; set; }
        public VirtualKey Key8 { get; set; }
        public VirtualKey Key9 { get; set; }
        public VirtualKey Key0 { get; set; }
        public VirtualKey KeyDoubleQuote { get; set; }
        public VirtualKey KeyTab { get; set; }
        public VirtualKey KeyCaps { get; set; }
        public VirtualKey KeyShift { get; set; }
        public VirtualKey KeyStar { get; set; }
        public VirtualKey KeyDash { get; set; }
        public VirtualKey KeyBack { get; set; }
        public VirtualKey KeyEnter { get; set; }
        public VirtualKey KeyComma { get; set; }
        public VirtualKey KeyPoint { get; set; }
        public VirtualKey KeyAt { get; set; }
        public VirtualKey KeySpace { get; set; }
        public VirtualKey UpArrow { get; set; }
        public VirtualKey DownArrow { get; set; }

        public IList<VirtualKey> VirtualKeys { get; set; }

        public VirtualKeyboard()
        {
            VirtualKeys = new List<VirtualKey>();

            KeyA = new VirtualKey(Keys.A); VirtualKeys.Add(KeyA);
            KeyB = new VirtualKey(Keys.B); VirtualKeys.Add(KeyB);
            KeyC = new VirtualKey(Keys.C); VirtualKeys.Add(KeyC);
            KeyCTr = new VirtualKey(Keys.Oem5); VirtualKeys.Add(KeyCTr);
            KeyD = new VirtualKey(Keys.D); VirtualKeys.Add(KeyD);
            KeyE = new VirtualKey(Keys.E); VirtualKeys.Add(KeyE);
            KeyF = new VirtualKey(Keys.F); VirtualKeys.Add(KeyF);
            KeyG = new VirtualKey(Keys.G); VirtualKeys.Add(KeyG);
            KeyGTr = new VirtualKey(Keys.Oem4); VirtualKeys.Add(KeyGTr);
            KeyH = new VirtualKey(Keys.H); VirtualKeys.Add(KeyH);
            KeyI = new VirtualKey(Keys.I); VirtualKeys.Add(KeyI);
            KeyITr = new VirtualKey(Keys.Oem7); VirtualKeys.Add(KeyITr);
            KeyJ = new VirtualKey(Keys.J); VirtualKeys.Add(KeyJ);
            KeyK = new VirtualKey(Keys.K); VirtualKeys.Add(KeyK);
            KeyL = new VirtualKey(Keys.L); VirtualKeys.Add(KeyL);
            KeyM = new VirtualKey(Keys.M); VirtualKeys.Add(KeyM);
            KeyN = new VirtualKey(Keys.N); VirtualKeys.Add(KeyN);
            KeyO = new VirtualKey(Keys.O); VirtualKeys.Add(KeyO);
            KeyOTr = new VirtualKey(Keys.Oem2); VirtualKeys.Add(KeyOTr);
            KeyP = new VirtualKey(Keys.P); VirtualKeys.Add(KeyP);
            KeyQ = new VirtualKey(Keys.Q); VirtualKeys.Add(KeyQ);
            KeyR = new VirtualKey(Keys.R); VirtualKeys.Add(KeyR);
            KeyS = new VirtualKey(Keys.S); VirtualKeys.Add(KeyS);
            KeySTr = new VirtualKey(Keys.Oem1); VirtualKeys.Add(KeySTr);
            KeyT = new VirtualKey(Keys.T); VirtualKeys.Add(KeyT);
            KeyU = new VirtualKey(Keys.U); VirtualKeys.Add(KeyU);
            KeyUTr = new VirtualKey(Keys.Oem6); VirtualKeys.Add(KeyUTr);
            KeyV = new VirtualKey(Keys.V); VirtualKeys.Add(KeyV);
            KeyW = new VirtualKey(Keys.W); VirtualKeys.Add(KeyW);
            KeyX = new VirtualKey(Keys.X); VirtualKeys.Add(KeyX);
            KeyY = new VirtualKey(Keys.Y); VirtualKeys.Add(KeyY);
            KeyZ = new VirtualKey(Keys.Z); VirtualKeys.Add(KeyZ);
            Key1 = new VirtualKey(Keys.D1); VirtualKeys.Add(Key1);
            Key2 = new VirtualKey(Keys.D2); VirtualKeys.Add(Key2);
            Key3 = new VirtualKey(Keys.D3); VirtualKeys.Add(Key3);
            Key4 = new VirtualKey(Keys.D4); VirtualKeys.Add(Key4);
            Key5 = new VirtualKey(Keys.D5); VirtualKeys.Add(Key5);
            Key6 = new VirtualKey(Keys.D6); VirtualKeys.Add(Key6);
            Key7 = new VirtualKey(Keys.D7); VirtualKeys.Add(Key7);
            Key8 = new VirtualKey(Keys.D8); VirtualKeys.Add(Key8);
            Key9 = new VirtualKey(Keys.D9); VirtualKeys.Add(Key9);
            Key0 = new VirtualKey(Keys.D0); VirtualKeys.Add(Key0);
            KeyDoubleQuote = new VirtualKey(Keys.Oem3); VirtualKeys.Add(KeyDoubleQuote);
            KeyTab = new VirtualKey("Tab", "Tab", Keys.Tab); VirtualKeys.Add(KeyTab);
            KeyCaps = new VirtualKey("Caps", "Caps", Keys.Capital); VirtualKeys.Add(KeyCaps);
            KeyShift = new VirtualKey("Shift", "Shift", Keys.Shift); VirtualKeys.Add(KeyShift);
            KeyStar = new VirtualKey(Keys.Oem8); VirtualKeys.Add(KeyStar);
            KeyDash = new VirtualKey(Keys.OemMinus); VirtualKeys.Add(KeyDash);
            KeyBack = new VirtualKey("BackSpace", "BackSpace", Keys.Back); VirtualKeys.Add(KeyBack);
            KeyEnter = new VirtualKey("Enter", "Enter", Keys.Enter); VirtualKeys.Add(KeyEnter);
            KeyComma = new VirtualKey(Keys.Oemcomma); VirtualKeys.Add(KeyComma);
            KeyPoint = new VirtualKey(Keys.OemPeriod); VirtualKeys.Add(KeyPoint);
            KeyAt = new VirtualKey("@", "€", Keys.Oem102); VirtualKeys.Add(KeyAt);
            KeySpace = new VirtualKey(" ", "Space", Keys.Space); VirtualKeys.Add(KeySpace);
            UpArrow = new VirtualKey("Up", "Up", Keys.Up); VirtualKeys.Add(UpArrow);
            DownArrow = new VirtualKey("Down", "Down", Keys.Down); VirtualKeys.Add(DownArrow);
        }

        public void PressKey(Keys keyCode)
        {
            var structInput = new INPUT();
            structInput.type = 1;
            structInput.ki.wScan = 0;
            structInput.ki.time = 0;
            structInput.ki.dwFlags = 0;
            structInput.ki.dwExtraInfo = 0;
            // Key down the actual key-code

            structInput.ki.wVk = (ushort)keyCode; //VK.SHIFT etc.
            NativeWin32.SendInput(1, ref structInput, Marshal.SizeOf(structInput));
        }

        public void ReleaseKey(Keys keyCode)
        {
            var structInput = new INPUT();
            structInput.type = 1;
            structInput.ki.wScan = 0;
            structInput.ki.time = 0;
            structInput.ki.dwFlags = 0;
            structInput.ki.dwExtraInfo = 0;

            // Key up the actual key-code
            structInput.ki.dwFlags = NativeWin32.KEYEVENTF_KEYUP;
            structInput.ki.wVk = (ushort)keyCode;// (ushort)NativeWin32.VK.SNAPSHOT;//vk;
            NativeWin32.SendInput(1, ref structInput, Marshal.SizeOf(structInput));
        }

        public void ProcessKey(Keys keyCode)
        {
            if (keyCode == Keys.Shift)
            {
                ToggleShift();
            }
            else
            {
                SendKey(keyCode);               
            }
        }

        private bool _shifted;

        private void ToggleShift()
        {
            _shifted = !_shifted;
            foreach (var virtualKey in VirtualKeys)
            {
                virtualKey.KeyState = _shifted ? KeyState.SecondSet : KeyState.FirstSet;
            }
            if (_shifted) PressKey(Keys.LShiftKey); else ReleaseKey(Keys.LShiftKey);
        }

        public void SendKey(Keys keyCode)
        {
            PressKey(keyCode);
            ReleaseKey(keyCode);
        }
    }
}
