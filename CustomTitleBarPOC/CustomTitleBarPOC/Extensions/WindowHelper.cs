using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace CustomTitleBarPOC.Extensions
{
	public static class WindowHelper
	{
		public static void DragMove(Window window, Point offset)
		{
			if (window.WindowState == WindowState.Normal)
			{
				window.Left = System.Windows.Forms.Cursor.Position.X - offset.X;
				window.Top = System.Windows.Forms.Cursor.Position.Y - offset.Y;
			}
		}

		/// <summary>
		/// Gets the window left.
		/// </summary>
		/// <param name="window">The window.</param>
		/// <returns></returns>
		public static double GetWindowLeft(this Window window)
		{
			var leftField = typeof(Window).GetField("_actualLeft", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			return (double)leftField.GetValue(window);
		}

		/// <summary>
		/// Known issue in WPF's design: Window Left and Top properties are not updated accurately 
		/// when the WindowState is set to Maximized and the window is on a secondary monitor. 
		/// </summary>
		public static Point GetActualWindowTopLeftPosition(Window window)
		{
			var topProperty = typeof(Window).GetProperty("_actualTop", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			var leftProperty = typeof(Window).GetProperty("_actualLeft", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

			double top = (double)topProperty.GetValue(window, null);
			double left = (double)leftProperty.GetValue(window, null);

			return new Point(top, left);
		}

		/// <summary>
		/// Gets the window top.
		/// </summary>
		/// <param name="window">The window.</param>
		/// <returns></returns>
		public static double GetWindowTop(this Window window)
		{
			var topField = typeof(Window).GetField("_actualTop", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			return (double)topField.GetValue(window);
		}


		// RESENI CO NEFACHA
		public static (double Left, double Top) GetWindowPositionLeftTop(Window window)
		{
			if (window.WindowState == WindowState.Maximized)
			{
				var handle = new WindowInteropHelper(window).Handle;
				var monitor = User32.MonitorFromWindow(handle, User32.MONITOR_DEFAULTTONEAREST);

				// Get monitor info
				var monitorInfo = new User32.MONITORINFO();
				User32.GetMonitorInfo(monitor, monitorInfo);

				return (monitorInfo.rcWork.left, monitorInfo.rcWork.top);
			}
			else
			{
				return (window.Left, window.Top);
			}
		}

		public static class User32
		{
			[DllImport("user32.dll")]
			internal static extern IntPtr MonitorFromWindow([In] IntPtr hwnd, [In] int dwFlags);

			[DllImport("user32.dll")]
			public static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

			public const int MONITOR_DEFAULTTONEAREST = 0x00000002;

			public struct MONITORINFO
			{
				public int cbSize;
				public RECT rcMonitor;
				public RECT rcWork;
				public int dwFlags;
			}

			public struct RECT
			{
				public int left;
				public int top;
				public int right;
				public int bottom;
			}
		}


		[StructLayout(LayoutKind.Sequential)]
		struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);


		public static double ActualTop(this Window window)
		{
			switch (window.WindowState)
			{
				case WindowState.Normal:
					return window.Top;
				case WindowState.Minimized:
					return window.RestoreBounds.Top;
				case WindowState.Maximized:
					{
						RECT rect;
						GetWindowRect((new WindowInteropHelper(window)).Handle, out rect);
						return rect.Top;
					}
			}
			return 0;
		}
		public static double ActualLeft(this Window window)
		{
			switch (window.WindowState)
			{
				case WindowState.Normal:
					return window.Left;
				case WindowState.Minimized:
					return window.RestoreBounds.Left;
				case WindowState.Maximized:
					{
						RECT rect;
						GetWindowRect((new WindowInteropHelper(window)).Handle, out rect);
						return rect.Left;
					}
			}
			return 0;
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

		public const UInt32 SC_MOVE = 0xF010;
		public const UInt32 SC_SIZE = 0xF000;
		public const UInt32 HTCAPTION = 0x2;
		public const UInt32 WM_SYSCOMMAND = 0x112;

		public static void MoveWindow(Window window)
		{
			SendMessage(new WindowInteropHelper(window).Handle, WM_SYSCOMMAND, (IntPtr)SC_MOVE, (IntPtr)HTCAPTION);
		}

		public static void ResizeWindow(Window window)
		{
			SendMessage(new WindowInteropHelper(window).Handle, WM_SYSCOMMAND, (IntPtr)SC_SIZE, IntPtr.Zero);
		}


	}
}
