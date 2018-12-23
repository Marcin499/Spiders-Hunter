using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gra
{
    class Podstawa : IDisposable
    {
        bool disposed = false;

        Bitmap bitmap;
        private int x;
        private int y;

        public int Left { get { return x; } set { x = value; } }
        public int Top { get { return y; } set { y = value; } }

        public Podstawa (Bitmap Resourses)
        {
            bitmap = new Bitmap(Resourses);
        }

        public void DrawImage (Graphics gfx)
        {
            gfx.DrawImage(bitmap, x, y);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                bitmap.Dispose();
            }
            disposed = true;
        }
    }
}
