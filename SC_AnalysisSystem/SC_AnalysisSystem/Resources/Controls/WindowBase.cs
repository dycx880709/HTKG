using DNBSoft.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Shapes;

namespace SC_AnalysisSystem.Resources.Controls
{
    public class WindowBase : Window
    {
        private Border layoutBorder;

        private ResourceDictionary Resource;
        /// <summary>
        /// 响应最小化事件
        /// </summary>
        public EventHandler<RoutedEventArgs> MinimizedHandle;
        /// <summary>
        /// 响应最大化事件
        /// </summary>
        public EventHandler<RoutedEventArgs> MaximizedHandle;
        /// <summary>
        /// 响应关闭窗体事件
        /// </summary>
        public EventHandler<RoutedEventArgs> CloseHandle;
        /// <summary>
        /// 响应窗体移动放大缩小事件
        /// </summary>
        public EventHandler<MouseButtonEventArgs> MouseClickHandle;

        public bool CanResize
        {
            get { return (bool)GetValue(CanResizeProperty); }
            set { SetValue(CanResizeProperty, value); }
        }

        public static readonly DependencyProperty CanResizeProperty =
            DependencyProperty.Register("CanResize", typeof(bool), typeof(WindowBase), new PropertyMetadata(true));

        public WindowBase()
        {
            InitializeStyle();
            InitializeMaxScreenSize();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SourceInitialized += new EventHandler(win_SourceInitialized);
            MinimizedHandle += (o, e) => WindowState = WindowState.Minimized;
            MaximizedHandle += (o, e) => win_ResizeWindow(o, e);
            StateChanged += WindowBase_StateChanged;
            CloseHandle += (o, e) => Application.Current.Dispatcher.Invoke(Close);
            MouseClickHandle += (o, e) => TopGrid_MouseLeftButtonDown(o, e);
            Loaded += (o, e) => GetControlProperty();
        }

        private void WindowBase_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                IsResizeRectangleVisible(false);
            else if (WindowState == WindowState.Normal)
                IsResizeRectangleVisible(true);
            this.Margin = new Thickness(WindowState == WindowState.Maximized ? 0 : 12);
        }

        private void GetControlProperty()
        {
            ControlTemplate baseWindowTemplate = (ControlTemplate)Resource["CustomWindowControlTemplate"];
            layoutBorder = (Border)baseWindowTemplate.FindName("layoutBorder", this);
            this.Margin = new Thickness(WindowState == WindowState.Maximized ? 0 : 12);
        }

        /// <summary>
        /// 加载自定义窗体样式
        /// </summary>
        private void InitializeStyle()
        {
            Resource = new ResourceDictionary();
            Resource.Source = new Uri("Resources/Controls/WindowBase.xaml", UriKind.Relative);
            Style = (Style)Resource["CustomWindowStyle"];
        }

        private void InitializeMaxScreenSize()
        {
            MaxWidth = SystemParameters.PrimaryScreenWidth;
            MaxHeight = SystemParameters.PrimaryScreenHeight;
        }

        /// <summary>
        /// 加载WindowResizer内容
        /// </summary>
        protected void InitializeWindowResizer(int minHeight, int minWidth)
        {
            ControlTemplate baseWindowTemplate = (ControlTemplate)Resource["CustomWindowControlTemplate"];
            var left = (Rectangle)baseWindowTemplate.FindName("left", this);
            var right = (Rectangle)baseWindowTemplate.FindName("right", this);
            var top = (Rectangle)baseWindowTemplate.FindName("top", this);
            var topLeft = (Rectangle)baseWindowTemplate.FindName("topLeft", this);
            var topRight = (Rectangle)baseWindowTemplate.FindName("topRight", this);
            var bottom = (Rectangle)baseWindowTemplate.FindName("bottom", this);
            var bottomLeft = (Rectangle)baseWindowTemplate.FindName("bottomLeft", this);
            var bottomRight = (Rectangle)baseWindowTemplate.FindName("bottomRight", this);

            WindowResizer resizer = new WindowResizer(this);
            resizer.minHeight = minHeight;
            resizer.minWidth = minWidth;
            resizer.addResizerLeft(left);
            resizer.addResizerDown(bottom);
            resizer.addResizerRight(right);
            resizer.addResizerUp(top);
            resizer.addResizerLeftUp(topLeft);
            resizer.addResizerRightUp(topRight);
            resizer.addResizerLeftDown(bottomLeft);
            resizer.addResizerRightDown(bottomRight);
        }

        /// <summary>
        /// 隐藏WindowResizer
        /// </summary>
        protected void IsResizeRectangleVisible(bool isVisible)
        {
            ControlTemplate baseWindowTemplate = (ControlTemplate)Resource["CustomWindowControlTemplate"];
            var left = (Rectangle)baseWindowTemplate.FindName("left", this);
            var right = (Rectangle)baseWindowTemplate.FindName("right", this);
            var top = (Rectangle)baseWindowTemplate.FindName("top", this);
            var topLeft = (Rectangle)baseWindowTemplate.FindName("topLeft", this);
            var topRight = (Rectangle)baseWindowTemplate.FindName("topRight", this);
            var bottom = (Rectangle)baseWindowTemplate.FindName("bottom", this);
            var bottomLeft = (Rectangle)baseWindowTemplate.FindName("bottomLeft", this);
            var bottomRight = (Rectangle)baseWindowTemplate.FindName("bottomRight", this);

            left.Visibility = right.Visibility = top.Visibility = topLeft.Visibility = topRight.Visibility = 
                bottom.Visibility = bottomLeft.Visibility = bottomRight.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void TopGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                win_ResizeWindow(sender, null);
                e.Handled = true;
                return;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
                e.Handled = true;
            }
        }

        private void win_ResizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        #region 解决最大化覆盖任务栏问题

        /// <summary>
        /// 获得并设置窗体大小信息
        /// </summary>
        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
            }
            Marshal.StructureToPtr(mmi, lParam, true);
        }

        /// <summary>
        /// 最大化最小化切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void win_ResizeWindow(object sender, EventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

            if (WindowState == WindowState.Maximized)
                layoutBorder.Margin = new Thickness(0);
            else
                layoutBorder.Margin = new Thickness(40);
        }

        /// <summary>
        /// 重绘窗体大小
        /// </summary>
        void win_SourceInitialized(object sender, EventArgs e)
        {
            IntPtr handle = (new WindowInteropHelper(this)).Handle;
            HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(WindowProc));
        }

        //////////////////////////////////////////////////////////////////////////
        // 使用Window API处理窗体大小  
        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// POINT aka POINTAPI
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>
            /// x coordinate of point.
            /// </summary>
            public int x;

            /// <summary>
            /// y coordinate of point.
            /// </summary>
            public int y;

            /// <summary>
            /// Construct a point of coordinates (x,y).
            /// </summary>
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        /// <summary>
        /// 窗体大小信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };

        /// <summary> Win32 </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT
        {
            /// <summary> Win32 </summary>
            public int left;
            /// <summary> Win32 </summary>
            public int top;
            /// <summary> Win32 </summary>
            public int right;
            /// <summary> Win32 </summary>
            public int bottom;

            /// <summary> Win32 </summary>
            public static readonly RECT Empty = new RECT();

            /// <summary> Win32 </summary>
            public int Width
            {
                get { return Math.Abs(right - left); }  // Abs needed for BIDI OS
            }
            /// <summary> Win32 </summary>
            public int Height
            {
                get { return bottom - top; }
            }

            /// <summary> Win32 </summary>
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            /// <summary> Win32 </summary>
            public RECT(RECT rcSrc)
            {
                left = rcSrc.left;
                top = rcSrc.top;
                right = rcSrc.right;
                bottom = rcSrc.bottom;
            }

            /// <summary> Win32 </summary>
            public bool IsEmpty
            {
                get
                {
                    // BUGBUG : On Bidi OS (hebrew arabic) left > right
                    return left >= right || top >= bottom;
                }
            }

            /// <summary> Return a user friendly representation of this struct </summary>
            public override string ToString()
            {
                if (this == Empty) { return "RECT {Empty}"; }
                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }

            /// <summary> Determine if 2 RECT are equal (deep compare) </summary>
            public override bool Equals(object obj)
            {
                if (!(obj is Rect)) { return false; }
                return (this == (RECT)obj);
            }

            /// <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
            public override int GetHashCode()
            {
                return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            }

            /// <summary> Determine if 2 RECT are equal (deep compare)</summary>
            public static bool operator ==(RECT rect1, RECT rect2)
            {
                return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom);
            }

            /// <summary> Determine if 2 RECT are different(deep compare)</summary>
            public static bool operator !=(RECT rect1, RECT rect2)
            {
                return !(rect1 == rect2);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            /// <summary>
            /// </summary>            
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));

            /// <summary>
            /// </summary>            
            public RECT rcMonitor = new RECT();

            /// <summary>
            /// </summary>            
            public RECT rcWork = new RECT();

            /// <summary>
            /// </summary>            
            public int dwFlags = 0;
        }

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }
            return (IntPtr)0;
        }

        #endregion
    }
}
