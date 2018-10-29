using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Process
{
    class Function
    {
        int count = 1;
        #region invert
        public Bitmap Invert(Bitmap inputImg)
        {
            Bitmap image = new Bitmap(inputImg);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = image.GetPixel(x, y);

                    Color newColor = Color.FromArgb(pixel.A, 255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                    image.SetPixel(x, y, newColor);
                }
            }
            return image;
        }
        #endregion

        #region histogram
        
        public void Draw_Histogram(Image image, ref PictureBox pb)
        {
            
            int height = image.Height;
            int width = image.Width;
            Bitmap process = (Bitmap)image;
            Color pixel;
            int[] Histogram = new int[256];
            int[] HistogramR = new int[256];
            int[] HistogramG = new int[256];
            int[] HistogramB = new int[256];
            int  Gray, r, g, b;
            for (int i = 0; i <= 255; i++)
            {
                Histogram[i] = 0;
                HistogramR[i] = 0;
                HistogramG[i] = 0;
                HistogramB[i] = 0;
            }   

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pixel = process.GetPixel(i, j);
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;

                    //Gray = (int)(0.3 * r + 0.59 * g + 0.11 * b);
                    Gray = (r + g + b) / 3;

                    Histogram[Gray] = Histogram[Gray] + 1;
                    HistogramR[r] = Histogram[r] + 1;
                    HistogramG[g] = Histogram[g] + 1;
                    HistogramB[b] = Histogram[b] + 1;

                }

            }

            //--------------------------------------------------------------
            
            
            int max = 0;
            foreach (int i in Histogram) max = max > i ? max : i;
            Bitmap Bmp = new Bitmap(256, max+10);
            Bitmap BmpR = new Bitmap(256, max + 10);
            Bitmap BmpG = new Bitmap(256, max + 10);
            Bitmap BmpB = new Bitmap(256, max + 10);
            Graphics his = Graphics.FromImage(Bmp);
            Graphics hisR = Graphics.FromImage(BmpR);
            Graphics hisG = Graphics.FromImage(BmpG);
            Graphics hisB = Graphics.FromImage(BmpB);
            Pen pen = new Pen(Color.Black);
                


            switch(count)
            {
                case 1:
                    
                    pen.Color = Color.Black;
                    for (int i = 0; i <= 255; i++)
                    {
                        //his.DrawLine(pen, i, height - Histogram[i] + 10, i, height + 10);
                        his.DrawLine(pen, 0, i, 0, Histogram[i]);
                        //drawhis(his, Histogram, max);
                    }
                    pb.Image = (Image)Bmp;
                    break;
                case 2:
                    pen.Color = Color.Red;
                    for (int i = 0; i <= 255; i++)
                        hisR.DrawLine(pen, i, height - HistogramR[i] + 10, i, height + 10);
                    pb.Image = (Image)BmpR;
                    break;
                case 3:
                    pen.Color = Color.Green;
                    for (int i = 0; i <= 255; i++)
                        hisG.DrawLine(pen, i, height - HistogramG[i] + 10, i, height + 10);
                    pb.Image = (Image)BmpG;
                    break;
                case 4:
                    pen.Color = Color.Blue;
                    for (int i = 0; i <= 255; i++)
                        hisB.DrawLine(pen, i, height - HistogramB[i] + 10, i, height + 10);                
                    pb.Image = (Image)BmpB;
                    count = 0;
                    break;
            }
            
            count++;
            pb.Refresh();
        }
        
        #endregion histr

        



        #region gray
        public Bitmap gray(Image image)
        {
            Bitmap bmp = new Bitmap(image);
            for  (int y = 0; y <= image.Height-1; y++)
            {
                for (int x = 0; x <= image.Width - 1; x++)
                {
                    Color c1 = bmp.GetPixel(x, y);
                    int r1 = c1.R;
                    int g1 = c1.G;
                    int b1 = c1.B;
                    int avg1 = (r1 + g1 + b1) / 3;
                    bmp.SetPixel(x, y, Color.FromArgb(avg1, avg1, avg1));
                }
            }

            return bmp;
        }
        #endregion
    }
}
