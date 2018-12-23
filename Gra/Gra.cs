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
        int gameFrame = 0;
        int boomTime = 0;
        bool uruchomiono = false;
        bool _boom = false;
        int uderzeniaCelne = 0;
        int uderzeniaChybione = 0;
        int wszystkieUderzenia = 0;
        int poziomStrachu = 0;
        int liczbapPunktow = 0;
        Stopwatch stopWatch = new Stopwatch();
        public static string wynik = "";
        int zmiennaCzasu = 0;
        int zmiennaCzasu2 = 0;
        
#if Debug
        int cursX = 0;
        int cursY = 0;
#endif
        Pajak spider;
        PodMenu podmenu;
        Boom boom;
        Tablica tablica;
        
        public Gra(int a, int b)
        {
            zmiennaCzasu = a;
            zmiennaCzasu2 = b;
            InitializeComponent();
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = "";
            player.Play();
            spider = new Pajak() { Left = 10, Top = 200 };
            podmenu = new PodMenu() { Left = 900, Top = 0 };
            tablica = new Tablica() { Left = 10, Top = 10 };
            boom = new Boom(); 
        }       

        private void TimerTick(object sender, EventArgs e)
        {
            if (gameFrame >= zmiennaCzasu)
            {
                UpdatePajak();
                gameFrame = 0;
            }

            if (boomTime >=zmiennaCzasu2)
            {
                _boom = false;
                boomTime = 0;
                UpdatePajak();
            }
            boomTime++;
            gameFrame++;
            this.Refresh();
        }

        private void UpdatePajak()
        {
            Random rnd = new Random();
            spider.Update(rnd.Next(Resources.spider2.Width, this.Width - Resources.spider2.Width), rnd.Next(this.Height / 2, this.Height - Resources.spider2.Height * 2));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics gf = e.Graphics;

            if (_boom == true)
            {
                boom.DrawImage(gf);
            }
            else
            {
                if (uruchomiono == true)
                {
                    spider.DrawImage(gf);
                }
            }
            
            podmenu.DrawImage(gf);
            tablica.DrawImage(gf);


#if Debug
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis;
            Font font = new Font("Arial", 12, FontStyle.Regular);
            TextRenderer.DrawText(gp, "X=" + cursX.ToString() + ":" + "Y=" + cursY.ToString(), font, new Rectangle(0, 0, 120, 20), SystemColors.ControlText, flags);
#endif
            TextFormatFlags flags = TextFormatFlags.Left;
            Font font = new Font("Arial", 14, FontStyle.Regular);
            Font font1 = new Font("Arial", 14, FontStyle.Bold);
            TextRenderer.DrawText(e.Graphics, "Uderzenia: " + wszystkieUderzenia.ToString(), font, new Rectangle(30, 32, 120, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Uderzenia celne: " + uderzeniaCelne.ToString(), font, new Rectangle(30, 52, 200, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Uderzenia chybione: " + uderzeniaChybione.ToString(), font, new Rectangle(30, 72, 220, 20), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Poziom strachu: " + poziomStrachu.ToString()+ "%", font1, new Rectangle(30, 117, 220, 20), SystemColors.ControlText, flags);            
            TextRenderer.DrawText(e.Graphics, "Czas gry: " + stopWatch.Elapsed.ToString(), font, new Rectangle(30, 92, 165, 25), SystemColors.ControlText, flags);
            TextRenderer.DrawText(e.Graphics, "Liczba punktów: " + liczbapPunktow.ToString(), font, new Rectangle(30, 137, 220, 25), SystemColors.ControlText, flags);
            base.OnPaint(e);
        }

        private void GraMouseMove(object sender, MouseEventArgs e)
        {
#if Debug
            cursX = e.X;
            cursY = e.Y;
#endif
            this.Refresh();
        }

        private void GraMouseClick(object sender, MouseEventArgs e)
        {
            System.Media.SoundPlayer slap = new System.Media.SoundPlayer();
            slap.SoundLocation = "Slap.wav";
            slap.Play();

            if (e.X > 923 && e.X < 1182 && e.Y > 93 && e.Y < 140)
            {
                uruchomiono = true;
                timer1.Start();
                slap.Stop();
                stopWatch.Start();
                
            }
            else if (e.X > 923 && e.X < 1182 && e.Y > 140 && e.Y < 194)
            {
                timer1.Stop();
                slap.Stop();
                stopWatch.Stop();

            }
            else if (e.X > 923 && e.X < 1182 && e.Y > 194 && e.Y < 249)
            {
                spider = new Pajak() { Left = 10, Top = 200 };
                timer1.Stop();                
                uderzeniaCelne = 0;
                uderzeniaChybione = 0;
                wszystkieUderzenia = 0;
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
                    uderzeniaCelne++;                    
                }
                uderzeniaChybione++;
                wszystkieUderzenia = uderzeniaChybione + uderzeniaCelne;
                poziomStrachu = uderzeniaChybione * 5;
                liczbapPunktow = uderzeniaCelne * 100 - uderzeniaChybione *2;
                label3.Text = wynik;
                wynik = liczbapPunktow.ToString();

                if (poziomStrachu >= 100)
                {
                    pictureBox1.Visible = true;
                    timer1.Stop();
                    label1.BackColor = Color.Transparent;
                    label1.Visible = true;
                    button1.Visible = true;
                    System.Media.SoundPlayer krzyk = new System.Media.SoundPlayer();
                    krzyk.SoundLocation = "Krzyk.wav";
                    krzyk.Play();
                    stopWatch.Stop();
                    button3.Visible = true;
                }

                if (uderzeniaCelne == 5)
                {
                    pictureBox2.Visible = true;
                    label2.Visible = true;
                    label2.BackColor = Color.Transparent;
                    button1.Visible = true;
                    System.Media.SoundPlayer aplauz = new System.Media.SoundPlayer();
                    aplauz.SoundLocation = "Aplauz.wav";
                    aplauz.Play();
                    stopWatch.Stop();
                    timer1.Stop();
                    button3.Visible = true;
                }                
            }
        }

        private void ButtonWyjdzdoMenu(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(WyjdzDoMenu);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        public void WyjdzDoMenu(object obj)
        {
            Application.Run(new Menu());
        }

        private void ButtonWroc(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(WyjdzDoMenu);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void Wyniki(object sender, EventArgs e)
        {
            Wynik wk = new Wynik();
            wk.Show();
        }
    }    
}
