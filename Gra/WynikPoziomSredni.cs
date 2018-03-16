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

namespace Gra
{
    public partial class WynikPoziomSredni : Form
    {
        public WynikPoziomSredni()
        {
            InitializeComponent();
            label3.Text = Sredni.wynik;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                string location = @"C:\Users\Marcin\Documents\Visual Studio 2017\Projects\Gra\Gra\bin\Debug\wynik.txt";
                using (StreamWriter sw = new StreamWriter(location, true))
                {
                    sw.Write(textBox1.Text);
                    sw.WriteLine(label3.Text);
                    sw.Close();
                    sw.Dispose();

                }
                MessageBox.Show("Zapisano wynik", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

