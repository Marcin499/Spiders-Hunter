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
        
        private void ButtonLatwy(object sender, EventArgs e)
        {            
            this.Close();
            Thread th = new Thread(OpenNewFormLatwy);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        public void OpenNewFormLatwy(object e)
        {
            Application.Run(new Gra(10,5));
        }

        private void ButtonSredni(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(OpenNewFormSredni);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        public void OpenNewFormSredni(object e)
        {
            Application.Run(new Gra(6,4));
        }

        private void ButtonTrudny(object sender, EventArgs e)
        {
            this.Close();
            Thread th = new Thread(OpenNewFormTrudny);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        public void OpenNewFormTrudny(object e)
        {
            Application.Run(new Gra(4,3));
        }
    }
}
