#define Debug

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gra
{
    public partial class Gra : Form
    {
#if Debug
        int cursX = 0;
        int cursY = 0;
#endif
        Pajak spider;
        
        public Gra()
        {
            InitializeComponent();
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = "";
            player.Play();
            spider = new Pajak() { Left = 10, Top = 200 };
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics gp = e.Graphics;
            spider.DrawImage(gp);
            
#if Debug
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis;
            Font font = new Font("Arial", 12, FontStyle.Regular);
            TextRenderer.DrawText(gp, "X=" + cursX.ToString() + ":" + "Y=" + cursY.ToString(), font, new Rectangle(0, 0, 120, 20), SystemColors.ControlText, flags);
#endif
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
        
        
    }
}
