using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace IOBot
{
    public static class ScreenCapture
    {
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInf);

        public static void Screenshot()
        {
            try
            {
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics gfx = Graphics.FromImage(bmp);

                // Fill the gfx with the screenshot.
                gfx.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);

                // Lock the bitmap's bits.  
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                // Get the address of the first line.
                IntPtr ptr = bmpData.Scan0;

                // Declare an array to hold the bytes of the bitmap.
                int bytes = bmpData.Stride * bmp.Height;
                byte[] rgbValues = new byte[bytes];
                byte[] r = new byte[bytes / 3];
                byte[] g = new byte[bytes / 3];
                byte[] b = new byte[bytes / 3];

                // Copy the RGB values into the array.
                Marshal.Copy(ptr, rgbValues, 0, bytes);
                bmp.UnlockBits(bmpData);

                int count = 0;
                int stride = bmpData.Stride;

                for (int column = 0; column < bmpData.Height; column++)
                {
                    for (int row = 0; row < bmpData.Width; row++)
                    {
                        b[count] = (byte)(rgbValues[(column * stride) + (row * 3)]);
                        g[count] = (byte)(rgbValues[(column * stride) + (row * 3) + 1]);
                        r[count++] = (byte)(rgbValues[(column * stride) + (row * 3) + 2]);
                    }
                }

                for (int i = 0; i < r.Length; i++)
                {
                    if (b[i] == 147 && g[i] == 211 && r[i] == 255)
                    {
                        int x = i % Screen.PrimaryScreen.Bounds.Width;
                        int y = i / Screen.PrimaryScreen.Bounds.Width;
                        //MessageBox.Show("BonusBox Found!" + i + "Position: " + x + " " + y);

                        Cursor.Position = new Point(x, y);
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);//make left button down
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);//make left button up
                    }
                }

                Thread.Sleep(600);
                Screenshot();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
