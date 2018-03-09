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
    public partial class Poziomy : Form
    {
        public Poziomy()
        {
            InitializeComponent();
        }

        private void Poziomy_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {            
            this.Close();
            Thread th = new Thread(opennewform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            
        }

        public void opennewform(object e)
        {
            Application.Run(new Gra());

        }
    }
}
