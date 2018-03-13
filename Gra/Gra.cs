//#define Debug

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
using Gra.Properties;
using System.Diagnostics;

namespace Gra
{
    public partial class Gra : Form
    {
        int gameframe = 0;
        int boomtime = 0;
        bool uruchomiono = false;
        bool _boom = false;
        int uderzeniacelne = 0;
        int uderzeniachybione = 0;
        int wszystkieuderzenia = 0;
        int poziomstrachu = 0;
        Stopwatch stopwatch = new Stopwatch();
#if Debug
        int cursX = 0;
        int cursY = 0;
#endif
        Pajak spider;
        PodMenu podmenu;
        Boom boom;
        Tablica tablica;
        
        public Gra()
        {
            InitializeComponent();
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = "";
            player.Play();
            spider = new Pajak() { Left = 10, Top = 200 };
            podmenu = new PodMenu() { Left = 900, Top = 0 };
            tablica = new Tablica() { Left = 10, Top = 10 };
            boom = new Boom(); 

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gameframe >= 10)
            {
                UpdatePajak();
                gameframe = 0;
            }

            if (boomtime >=5)
            {
                _boom = false;
                boomtime = 0;
                UpdatePajak();
            }
            boomtime++;
            gameframe++;
            this.Refresh();
        }

        private void UpdatePajak()
        {
            Random rnd = new Random();
            spider.Update(rnd.Next(Resources.spider2.Width, this.Width - Resources.spider2.Width), rnd.Next(this.Height / 2, this.Height - Resources.spider2.Height * 2));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics gp = e.Graphics;

            if (_boom == true)
            {
                boom.DrawImage(gp);
            }
            else
            {
                if (uruchomiono == true)
                {
                    spider.DrawImage(gp);
                }
            }
            
            podmenu.DrawImage(gp);
            tablica.DrawImage(gp);


#if Debug
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis;
            Font font = new Font("Arial", 12, FontStyle.Regular);
            TextRenderer.DrawText(gp, "X=" + cursX.ToString() + ":" + "Y=" + cursY.ToString(), font, new Rectangle(0, 0, 120, 20), SystemColors.ControlText, flags);
#endif
            TextFormatFlags flags = TextFormatFlags.Left;
            Font font = new Font("Arial", 14, FontStyle.Regular);
            Font font1 = new Font("Arial", 14, FontStyle.Bold);
            TextRenderer.DrawText(e.Graphics, "Uderzenia: " + wszystkieuderzenia.ToString(), font, new Rectangle(30, 32, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Uderzenia celne: " + uderzeniacelne.ToString(), font, new Rectangle(30, 52, 200, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Uderzenia chybione: " + uderzeniachybione.ToString(), font, new Rectangle(30, 72, 220, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Poziom strachu: " + poziomstrachu.ToString()+ "%", font1, new Rectangle(30, 117, 220, 20), SystemColors.ControlText, flags);            
            TextRenderer.DrawText(e.Graphics, "Czas gry: " + stopwatch.Elapsed.ToString(), font, new Rectangle(30, 92, 165, 25), SystemColors.ControlText, flags);
            base.OnPaint(e);
        }

        private void Gra_MouseMove(object sender, MouseEventArgs e)
        {
#if Debug
            cursX = e.X;
            cursY = e.Y;
#endif
            this.Refresh();

        }

        private void Gra_MouseClick(object sender, MouseEventArgs e)
        {
            System.Media.SoundPlayer slap = new System.Media.SoundPlayer();
            slap.SoundLocation = "Slap.wav";
            slap.Play();

            if (e.X > 923 && e.X < 1182 && e.Y > 93 && e.Y < 140)
            {
                uruchomiono = true;
                timer1.Start();
                slap.Stop();
                stopwatch.Start();
                
            }
            else if (e.X > 923 && e.X < 1182 && e.Y > 140 && e.Y < 194)
            {
                timer1.Stop();
                slap.Stop();
                stopwatch.Stop();

            }
            else if (e.X > 923 && e.X < 1182 && e.Y > 194 && e.Y < 249)
            {
                spider = new Pajak() { Left = 10, Top = 200 };
                timer1.Stop();                
                uderzeniacelne = 0;
                uderzeniachybione = 0;
                wszystkieuderzenia = 0;
                slap.Stop();
            }
            else
            {
                if (spider.hit(e.X, e.Y))
                {
                    _boom = true;
                    boom.Left = spider.Left - Resources.boom2.Width / 3;
                    boom.Top = spider.Top - Resources.boom2.Height / 3;
                    System.Media.SoundPlayer bitch = new System.Media.SoundPlayer();
                    slap.Stop();
                    bitch.SoundLocation = "Got ya bitch.wav";                    
                    bitch.Play();
                    uderzeniacelne++;
                    
                }
                uderzeniachybione++;
                wszystkieuderzenia = uderzeniachybione + uderzeniacelne;
                poziomstrachu = uderzeniachybione * 5;
                if (poziomstrachu >= 100)
                {
                    pictureBox1.Visible = true;
                    timer1.Stop();
                    label1.BackColor = Color.Transparent;
                    label1.Visible = true;
                    button1.Visible = true;
                    System.Media.SoundPlayer krzyk = new System.Media.SoundPlayer();
                    krzyk.SoundLocation = "Krzyk.wav";
                    krzyk.Play();
                    stopwatch.Stop();
                    
                }
                if (uderzeniacelne == 5)
                {
                    pictureBox2.Visible = true;
                    label2.Visible = true;
                    label2.BackColor = Color.Transparent;
                    button1.Visible = true;
                    System.Media.SoundPlayer aplauz = new System.Media.SoundPlayer();
                    aplauz.SoundLocation = "Aplauz.wav";
                    aplauz.Play();
                    stopwatch.Stop();
                    timer1.Stop();
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(opennewform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        public void opennewform(object obj)
        {
            Application.Run(new Menu());

        }
    }
    
}
