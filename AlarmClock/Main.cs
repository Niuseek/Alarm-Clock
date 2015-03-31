using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace AlarmClock
{
    public partial class Main : Form
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        Boolean alarmTriggered = true;
        
        String songNumber = "1";

        public Main()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            alarmTriggered = false;
            chkAlarmSet.Checked = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (timer1.Enabled == false)
            {
                timer1.Interval = 500;
                timer1.Start();
            }
            
            textBox1.Text = DateTime.Now.ToShortTimeString();

            if (alarmTriggered == false)
            {
                if (dateTimePicker1.Value.ToShortTimeString() == DateTime.Now.ToShortTimeString())
                {
                    alarmTriggered = true;
                    Console.WriteLine("test 1 " + dateTimePicker1.Value);
                   

                    chkAlarmSet.Checked = false;

                    mciSendString("open \"" + "c:\\" + songNumber + ".mp3" + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
                    mciSendString("play MediaFile", null, 0, IntPtr.Zero);

                    MessageBox.Show("Wake up mfer!");
                }
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            chkAlarmSet.Checked = false;
            alarmTriggered = true;
            Console.WriteLine("test 2 " + Path.GetDirectoryName(Application.ExecutablePath));
            mciSendString("close MediaFile", null, 0, IntPtr.Zero);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            songNumber = comboBox1.SelectedItem.ToString();
        }
    }
}
