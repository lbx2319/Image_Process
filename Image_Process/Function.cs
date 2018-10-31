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
                   // Gray = (int)(0.59 * r + 0.3 * g + 0.11 * b);

                    Histogram[Gray] = Histogram[Gray] + 1;
                    HistogramR[r] = HistogramR[r] + 1;
                    HistogramG[g] = HistogramG[g] + 1;
                    HistogramB[b] = HistogramB[b] + 1;

                }

            }

            //--------------------------------------------------------------
            
            
            //int max = 0;
            //foreach (int i in Histogram) max = max > i ? max : i;
            //Bitmap Bmp = new Bitmap(256, max+10);
            Bitmap Bmp = new Bitmap(300, 300);
            Bitmap BmpR = new Bitmap(300, 300);
            Bitmap BmpG = new Bitmap(300, 300);
            Bitmap BmpB = new Bitmap(300, 300);
            Graphics his = Graphics.FromImage(Bmp);
            Graphics hisR = Graphics.FromImage(BmpR);
            Graphics hisG = Graphics.FromImage(BmpG);
            Graphics hisB = Graphics.FromImage(BmpB);
            Pen pen = new Pen(Color.Black);

            his.DrawLine(pen, 10, 0, 10, 270);
            his.DrawLine(pen, 10, 270, 300, 270);
            pen.Color = Color.Red;
            hisR.DrawLine(pen, 10, 0, 10, 270);
            hisR.DrawLine(pen, 10, 270, 300, 270);
            pen.Color = Color.Green;
            hisG.DrawLine(pen, 10, 0, 10, 270);
            hisG.DrawLine(pen, 10, 270, 300, 270);
            pen.Color = Color.Blue;
            hisB.DrawLine(pen, 10, 0, 10, 270);
            hisB.DrawLine(pen, 10, 270, 300, 270);

            switch (count)
            {
                case 1:
                    
                    pen.Color = Color.Black;
                    var max = Histogram.Max();
                    double factor = max / 270;
                    for (int i = 0; i <= 255; i++)
                    {
                        double x = Histogram[i] / factor;
                        Console.WriteLine(Histogram[i] + "   "+x);
                        if (x % 1 != 0)
                            x += 1;
                        his.DrawLine(pen, i+11, 270 - (int)x, i + 11, 270);
                        //his.DrawLine(pen, 0, i, 0, Histogram[i]);
                        //drawhis(his, Histogram, max);
                    }
                    pb.Image = (Image)Bmp;
                    break;
                case 2:
                    pen.Color = Color.Red;
                    var maxR = HistogramR.Max();
                    double factorR = maxR / 270;
                    for (int i = 0; i <= 255; i++)
                    {
                        double x = HistogramR[i] / factorR;
                        Console.WriteLine(HistogramR[i] + "   " + x);
                        if (x % 1 != 0)
                            x += 1;
                        hisR.DrawLine(pen, i+11, 270 - (int)x, i + 11, 270);
                        //his.DrawLine(pen, 0, i, 0, Histogram[i]);
                        //drawhis(his, Histogram, max);
                    }
                    pb.Image = (Image)BmpR;
                    break;
                case 3:
                    pen.Color = Color.Green;
                    var maxG = HistogramG.Max();
                    double factorG = maxG / 270;
                    for (int i = 0; i <= 255; i++)
                    {
                        double x = (double)HistogramG[i] / factorG;
                        Console.WriteLine(HistogramG[i] + "   " + x);
                        if (x % 1 != 0)
                            x += 1;
                        hisG.DrawLine(pen, i+11, 270 -(int) x, i + 11, 270);
                        //his.DrawLine(pen, 0, i, 0, Histogram[i]);
                        //drawhis(his, Histogram, max);
                    }
                    pb.Image = (Image)BmpG;
                    break;
                case 4:
                    pen.Color = Color.Blue;
                    var maxB = HistogramB.Max();
                    double factorB = maxB / 270;
                    for (int i = 0; i <= 255; i++)
                    {
                        double x = ((double)HistogramB[i] / factorB);
                        Console.WriteLine(HistogramB[i] + "   " + x);
                        if (x % 1 != 0)
                            x += 1;
                        hisB.DrawLine(pen, i+11, 270 - (int)x, i+11, 270);
                        //his.DrawLine(pen, 0, i, 0, Histogram[i]);
                        //drawhis(his, Histogram, max);
                    }
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
