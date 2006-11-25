using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

//<summary>
//Taken from ConsoleHackerAPI
//Author unknown
//</summary>

namespace ConsoleHookWIN32
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CharInfo
	{
		public char Char;
		public CharInfoAttributes Attributes;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Coord
	{
		public Coord(short x, short y)
		{
			X = x;
			Y = y;
		}

		public short X;
		public short Y;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SmallRect
	{
		public SmallRect(short left, short top, short right, short bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public short Left;
		public short Top;
		public short Right;
		public short Bottom;
	}

	public enum CharInfoAttributes : short
	{
		/// <summary>
		/// Text color contains blue. 
		/// </summary>
		FOREGROUND_BLUE = 0x0001,

		/// <summary>
		/// Text color contains green. 
		/// </summary>
		FOREGROUND_GREEN = 0x0002,
		
		/// <summary>
		/// Text color contains red. 
		/// </summary>
		FOREGROUND_RED = 0x0004,

		/// <summary>
		/// Text color is intensified. 
		/// </summary>
		FOREGROUND_INTENSITY = 0x0008,
		 
		/// <summary>
		/// Background color contains blue. 
		/// </summary>
		BACKGROUND_BLUE = 0x0010,

		/// <summary>
		/// Background color contains green.
		/// </summary>
		BACKGROUND_GREEN = 0x0020,
		  
		/// <summary>
		/// Background color contains red. 
		/// </summary>
		BACKGROUND_RED = 0x0040,
		 
		/// <summary>
		/// Background color is intensified. 
		/// </summary>
		BACKGROUND_INTENSITY = 0x0080,

		/// <summary>
		/// Leading byte. 
		/// </summary>
		COMMON_LVB_LEADING_BYTE = 0x0100,
		 
		/// <summary>
		/// Trailing byte. 
		/// </summary>
		COMMON_LVB_TRAILING_BYTE = 0x0200,
		
		/// <summary>
		/// Top horizontal
		/// </summary>
		COMMON_LVB_GRID_HORIZONTAL = 0x0400,
		  
		/// <summary>
		/// Left vertical.
		/// </summary>
		COMMON_LVB_GRID_LVERTICAL = 0x0800,
		 
		/// <summary>
		/// Right vertical. 
		/// </summary>
		COMMON_LVB_GRID_RVERTICAL = 0x1000,
		 
		/// <summary>
		/// Reverse foreground and background attribute. 
		/// </summary>
		COMMON_LVB_REVERSE_VIDEO = 0x4000,
		 
		///// <summary>
		///// Underscore. 
		///// </summary>
		//COMMON_LVB_UNDERSCORE = 0x8000
	}
	
	public enum ProcAccessRights
	{
		ALL_ACCESS = 0x1F0FFF, 
		CREATE_PROCESS = 0x0080, 
		CREATE_THREAD = 0x0002,
		DUP_HANDLE = 0x0040,
		QUERY_INFORMATION = 0x0400,
		QUERY_LIMITED_INFORMATION = 0x1000,
		SET_QUOTA = 0x0100,
		SET_INFORMATION = 0x0200,
		SUSPEND_RESUME = 0x0800, 
		TERMINATE = 0x0001,
		VM_OPERATION = 0x0008, 
		VM_READ = 0x0010,
		VM_WRITE = 0x0020
	}

	public enum StandardHandle
	{
		/// <summary>
		/// Handle to the standard input device. Initially, this is a handle to the console input buffer.
		/// </summary>
		STD_INPUT_HANDLE = -10,

		/// <summary>
		/// Handle to the standard output device. Initially, this is a handle to the active console screen buffer.
		/// </summary>
		STD_OUTPUT_HANDLE = -11,

		/// <summary>
		/// Handle to the standard error device. Initially, this is a handle to the active console screen buffer.
		/// </summary>
		STD_ERROR_HANDLE = -12 
	}

	public class Win32
	{
		[DllImport("Psapi.dll")]
		public static extern Boolean EnumProcesses(uint[] buffer, int sizeOfBuffer, 
			ref int returnBufferSize);

		[DllImport("Kernel32.dll")]
		public static extern IntPtr OpenProcess(ProcAccessRights dwDesiredAccess, bool bInheritHandle, 
			IntPtr dwProcessId);

		[DllImport("Psapi.dll")]
		public static extern Boolean EnumProcessModules(IntPtr hProcess, uint[] buffer, int sizeOfBuffer,
			ref int returnedBufferSize);

		[DllImport("Psapi.dll")]
		public static extern int GetModuleBaseName(IntPtr hProcess, IntPtr hModule, StringBuilder lpBaseName,
			int sizeOfBuffer);

		[DllImport("Kernel32.dll")]
		public static extern Boolean CloseHandle(IntPtr hObject);

		[DllImport("Kernel32.dll")]
		public static extern Boolean AttachConsole(IntPtr dwProcessId);

		[DllImport("Kernel32.dll")]
		public static extern Boolean FreeConsole();

		[DllImport("Kernel32.dll")]
		public static extern Boolean ReadConsoleOutput(IntPtr hConsoleOutput, [Out] CharInfo[] lpBuffer,
			Coord dwBufferSize, Coord dwBufferCoord, ref SmallRect lpReadRegion);

		[DllImport("Kernel32.dll")]
		public static extern IntPtr GetStdHandle(StandardHandle nStdHandle);

		[DllImport("Kernel32.dll")]
		public static extern Boolean ReadConsoleOutputCharacter(IntPtr hConsoleOutput, 
			StringBuilder lpCharacter, int nLength, Coord dwReadCoord, ref int lpNumberOfCharsRead);

        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
	}
}
