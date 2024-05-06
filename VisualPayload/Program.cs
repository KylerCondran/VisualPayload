using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace VisualPayload
{
    public class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);
        static Rectangle r = new Rectangle();
        static Queue<Bitmap> q = new Queue<Bitmap>();
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            r.Width = 1920;
            r.Height = 1080;
            while (true) { 
                CopyScreen();
                DumpToScreen();
            }
        }
        public static void CopyScreen()
        {
            Size s = new Size(rnd.Next(50, 200), rnd.Next(50, 200));
            Bitmap screen = new Bitmap(s.Width, s.Height);
            Graphics g = Graphics.FromImage(screen);
            g.CopyFromScreen(rnd.Next(0, r.Width), rnd.Next(0, r.Height), 0, 0, s);
            switch (rnd.Next(0, 15))
            {
                case 0: screen.RotateFlip(RotateFlipType.Rotate180FlipNone); break;
                case 1: screen.RotateFlip(RotateFlipType.Rotate180FlipX); break;
                case 2: screen.RotateFlip(RotateFlipType.Rotate180FlipXY); break;
                case 3: screen.RotateFlip(RotateFlipType.Rotate180FlipY); break;
                case 4: screen.RotateFlip(RotateFlipType.Rotate270FlipNone); break;
                case 5: screen.RotateFlip(RotateFlipType.Rotate270FlipX); break;
                case 6: screen.RotateFlip(RotateFlipType.Rotate270FlipXY); break;
                case 7: screen.RotateFlip(RotateFlipType.Rotate270FlipY); break;
                case 8: screen.RotateFlip(RotateFlipType.Rotate90FlipNone); break;
                case 9: screen.RotateFlip(RotateFlipType.Rotate90FlipX); break;
                case 10: screen.RotateFlip(RotateFlipType.Rotate90FlipXY); break;
                case 11: screen.RotateFlip(RotateFlipType.Rotate90FlipY); break;
                case 12: screen.RotateFlip(RotateFlipType.RotateNoneFlipNone); break;
                case 13: screen.RotateFlip(RotateFlipType.RotateNoneFlipX); break;
                case 14: screen.RotateFlip(RotateFlipType.RotateNoneFlipXY); break;
                case 15: screen.RotateFlip(RotateFlipType.RotateNoneFlipY); break;
            }
            q.Enqueue(screen);
        }
        public static void DumpToScreen()
        {
            IntPtr screenDC = GetDC(IntPtr.Zero);
            while (q.Count > 0)
            {
                using (Bitmap b = q.Dequeue()) using (Graphics g = Graphics.FromHdc(screenDC)) g.DrawImage(b, rnd.Next(0, r.Width), rnd.Next(0, r.Height), b.Width, b.Height);
            }
            ReleaseDC(IntPtr.Zero, screenDC);
        }
    }
}