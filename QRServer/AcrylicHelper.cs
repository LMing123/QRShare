using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace QRServer
{
    public class AcrylicHelper
    {
        private IntPtr _handle = IntPtr.Zero;
        public AcrylicHelper(IntPtr handle)
        {
            this._handle = handle;
        }

        public int SetAcrylic()
        {
            var accent = new ACCENT_POLICY() { AccentState = ACCENT_STATE.ACCENT_ENABLE_BLURBEHIND, AccentFlags = 0, GradientColor = 5553, AnimationId = 0 };
            IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(accent));
            Marshal.StructureToPtr(accent, intPtr, false);
            var data = new WINDOWCOMPOSITIONATTRIBDATA();
            data.Attrib = WINDOWCOMPOSITIONATTRIB.WCA_ACCENT_POLICY;
            data.pvData = intPtr;
            data.cbData = Marshal.SizeOf(accent);
            return SetWindowCompositionAttribute(_handle, data);
        }
        public enum WINDOWCOMPOSITIONATTRIB
        {
            WCA_UNDEFINED = 0,
            WCA_NCRENDERING_ENABLED = 1,
            WCA_NCRENDERING_POLICY = 2,
            WCA_TRANSITIONS_FORCEDISABLED = 3,
            WCA_ALLOW_NCPAINT = 4,
            WCA_CAPTION_BUTTON_BOUNDS = 5,
            WCA_NONCLIENT_RTL_LAYOUT = 6,
            WCA_FORCE_ICONIC_REPRESENTATION = 7,
            WCA_EXTENDED_FRAME_BOUNDS = 8,
            WCA_HAS_ICONIC_BITMAP = 9,
            WCA_THEME_ATTRIBUTES = 10,
            WCA_NCRENDERING_EXILED = 11,
            WCA_NCADORNMENTINFO = 12,
            WCA_EXCLUDED_FROM_LIVEPREVIEW = 13,
            WCA_VIDEO_OVERLAY_ACTIVE = 14,
            WCA_FORCE_ACTIVEWINDOW_APPEARANCE = 15,
            WCA_DISALLOW_PEEK = 16,
            WCA_CLOAK = 17,
            WCA_CLOAKED = 18,
            WCA_ACCENT_POLICY = 19,
            WCA_FREEZE_REPRESENTATION = 20,
            WCA_EVER_UNCLOAKED = 21,
            WCA_VISUAL_OWNER = 22,
            WCA_LAST = 23
        }
       public enum ACCENT_STATE
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_Acrylic = 4,
            ACCENT_INVALID_STATE = 5
        }
        [StructLayout(LayoutKind.Sequential)]
        struct WINDOWCOMPOSITIONATTRIBDATA
        {
           public WINDOWCOMPOSITIONATTRIB Attrib;
            public IntPtr pvData;
            public int cbData;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct ACCENT_POLICY
        {
            public ACCENT_STATE AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }
        [DllImport("user32.dll")]
        static extern int SetWindowCompositionAttribute(IntPtr hwnd, WINDOWCOMPOSITIONATTRIBDATA data);
    }
}
