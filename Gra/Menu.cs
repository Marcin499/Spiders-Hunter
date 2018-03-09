using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Gra
{
    public partial class Menu : Form
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public Menu()
        {
            InitializeComponent();
            
            player.SoundLocation = "Granite.wav";
            player.Play();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int one = rand.Next(0, 255);
            int two = rand.Next(0, 255);
            int three = rand.Next(0, 255);
            int four = rand.Next(0, 255);

            label1.ForeColor = Color.FromArgb(one, two, three, four);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(opennewform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            player.Stop();

           
        }
        public void opennewform (object obj)
        {
            Application.Run(new Poziomy());

        }
    }
}
