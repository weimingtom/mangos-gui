using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ConsoleHookWIN32;

//<summary>
//Modified from ConsoleHackerAPI
//Author unknown
//</summary>

namespace ConsoleHook
{
    public class ProcInfos
    {
        public ProcInfos()
        {
        }
        public ProcInfos(IntPtr handle, string name)
        {
            Handle = handle;
            Name = name;
        }

        public IntPtr Handle = IntPtr.Zero;
        public string Name = "";
    }

    public static class HookToConsole
    {
        static IntPtr m_consoleOutputHandle = IntPtr.Zero;
        static bool m_isAttached = false;

        public static bool IsAttached
        {
            get
            {
                return m_isAttached;
            }
        }

        public static void AttachTo(IntPtr consoleProc)
        {
            m_isAttached = true;
            Win32.AttachConsole(consoleProc);

            m_consoleOutputHandle = Win32.GetStdHandle(StandardHandle.STD_OUTPUT_HANDLE);
        }

        public static void AttachTo(ProcInfos consoleProc)
        {
            AttachTo(consoleProc.Handle);
        }

        public static void RemoveFrom()
        {
            m_isAttached = false;
            Win32.FreeConsole();
            Win32.CloseHandle(m_consoleOutputHandle);
            m_consoleOutputHandle = IntPtr.Zero;
        }


        public static CharInfo[] ReadOutput(Point startPoint, Size sizeOf)
        {
            if (IsAttached == true && m_consoleOutputHandle != IntPtr.Zero)
            {
                CharInfo[] buffer = new CharInfo[sizeOf.Width * sizeOf.Height];
                Coord bufferSize = new Coord((short)sizeOf.Width, (short)sizeOf.Height);
                Coord bufferCoord = new Coord((short)startPoint.X, (short)startPoint.Y);
                SmallRect readReg = new SmallRect((short)startPoint.X, (short)startPoint.Y,
                    (short)(startPoint.X + sizeOf.Width), (short)(startPoint.Y + sizeOf.Height));

                Win32.ReadConsoleOutput(m_consoleOutputHandle, buffer, bufferSize, bufferCoord, ref readReg);

                return buffer;
            }
            throw new Exception("Error: No console attached to that process");
        }
    }
}
