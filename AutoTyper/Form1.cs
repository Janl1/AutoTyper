using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTyper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "settings.conf"))
            {
                string[] settings_string = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "settings.conf");

                foreach(string line in settings_string)
                {
                    if (line[0] == '#') continue;

                    string[] parts = line.Split('=');
                    settings.Add(parts[0], parts[1]);
                }
            } else
            {
                StreamWriter ws = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "settings.conf");
                ws.WriteLine("# Delay in ms the autotyper will wait after launch");
                ws.WriteLine("TypeDelay=2000");
                ws.WriteLine("# Play sound when text is typed {yes/no}");
                ws.WriteLine("FinishSound=yes");
                ws.Flush();
                ws.Close();
            }

            if(settings.ContainsKey("TypeDelay"))
            {
                Thread.Sleep(int.Parse(settings["TypeDelay"]));
            } else
            {
                Thread.Sleep(2000);
            }

            SendKeys.Send(Clipboard.GetText());

            if (settings.ContainsKey("FinishSound") && settings["FinishSound"] == "yes") SystemSounds.Beep.Play();

            this.Close();
        }
    }
}
