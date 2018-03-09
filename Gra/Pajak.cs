using Gra.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra
{
    class Pajak : Podstawa                
    {
        private Rectangle SpiderHotSpot = new Rectangle();
        

        public Pajak()
        : base(Resources.spider2)
        {
            SpiderHotSpot.X = Left + 20;
            SpiderHotSpot.Y = Top - 1;
            SpiderHotSpot.Width = 30;
            SpiderHotSpot.Height = 40;
        }
        

        public void Update(int x, int y)
        {
            Left = x;
            Top = y;
            SpiderHotSpot.X = Left + 20;
            SpiderHotSpot.Y = Top - 1;

        }

        public bool hit (int x, int y)
        {
            Rectangle c = new Rectangle(x, y, 1, 1);

            if (SpiderHotSpot.Contains(c))
            {
                return true;
            }
            return false;
        }
    }
}
