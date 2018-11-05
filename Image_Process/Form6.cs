using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Process
{
    public partial class Form6 : Form
    {
        double times;
        Bitmap bmp;
        public Form6(Image image)
        {
            InitializeComponent();
            pictureBox1.Image = image;
            bmp = new Bitmap(image);
        }
        private static float Lerp(float s, float e, float t)
        {
            return s + (e - s) * t;
        }

        private static float Blerp(float c00, float c10, float c01, float c11, float tx, float ty)
        {
            return Lerp(Lerp(c00, c10, tx), Lerp(c01, c11, tx), ty);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.Text;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        public Bitmap zoomin(Bitmap bbmp)
        {
            int w = (int)(bbmp.Width * times);
            int h = (int)(bbmp.Height * times);
            if (radioButton1.Checked)
            {
                Console.WriteLine("select1");
                Bitmap bigbmp = new Bitmap(w,h);

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        Color c = bmp.GetPixel((int)(x / times), (int)(y / times));
                        bigbmp.SetPixel(x, y, Color.FromArgb(c.R, c.G, c.B));
                    }
                }

                return bigbmp;
            }
            else {
                Console.WriteLine("select2");
                Bitmap bigbmp = new Bitmap(bbmp,w,h);
                Graphics g = Graphics.FromImage(bigbmp);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                /*for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        Color color1,color2,color3,color4;
                        if ((int)(x / times + 1) >= w)
                        {
                            color1 = bbmp.GetPixel((int)(x / times), (int)(y / times));
                            color2 = bbmp.GetPixel((int)((x) / times), (int)(y / times));
                            color3 = bbmp.GetPixel((int)(x / times), (int)(y / times) + 1);
                            color4 = bbmp.GetPixel((int)((x) / times), (int)(y / times) + 1);
                        }
                        else if ((int)(y / times) + 1 >= h)
                        {
                             color1 = bbmp.GetPixel((int)(x / times), (int)(y / times));
                             color2 = bbmp.GetPixel((int)((x) / times) + 1, (int)(y / times));
                             color3 = bbmp.GetPixel((int)(x / times), (int)(y / times));
                             color4 = bbmp.GetPixel((int)((x) / times) + 1, (int)(y / times));
                        }
                        else if (((int)(x / times + 1) >= w) && ((int)(y / times) + 1 >= h))
                        {
                             color1 = bbmp.GetPixel((int)(x / times), (int)(y / times));
                             color2 = bbmp.GetPixel((int)((x) / times), (int)(y / times));
                             color3 = bbmp.GetPixel((int)(x / times), (int)(y / times));
                             color4 = bbmp.GetPixel((int)((x) / times), (int)(y / times));
                        }
                        else
                        {
                             color1 = bbmp.GetPixel((int)(x / times), (int)(y / times));
                             color2 = bbmp.GetPixel((int)((x) / times) + 1, (int)(y / times));
                             color3 = bbmp.GetPixel((int)(x / times), (int)(y / times)+1);
                             color4 = bbmp.GetPixel((int)((x) / times) + 1, (int)(y / times)+1);
                        }

                        /*float gx = ((float)x) / w * (bbmp.Width - 1);
                        float gy = ((float)y) / h * (bbmp.Height - 1);
                        int gxi = (int)gx;
                        int gyi = (int)gy;
                        Color c00 = bbmp.GetPixel(gxi, gyi);
                        Color c10 = bbmp.GetPixel(gxi + 1, gyi);
                        Color c01 = bbmp.GetPixel(gxi, gyi + 1);
                        Color c11 = bbmp.GetPixel(gxi + 1, gyi + 1);

                        int red = (int)Blerp(c00.R, c10.R, c01.R, c11.R, gx - gxi, gy - gyi);
                        int green = (int)Blerp(c00.G, c10.G, c01.G, c11.R, gx - gxi, gy - gyi);
                        int blue = (int)Blerp(c00.B, c10.B, c01.B, c11.R, gx - gxi, gy - gyi);
                        //Color rgb = Color.FromArgb(red, green, blue);
                        int red = (color1.R + color2.R + color3.R + color4.R) / 4;
                        int green = (color1.G + color2.G + color3.G + color4.G) / 4;
                        int blue = (color1.B + color2.B + color3.B + color4.B) / 4;

                        bigbmp.SetPixel(x, y, Color.FromArgb(red, green, blue));
                    }
                }*/
                return bigbmp;
            }            
        }

        public Bitmap zoomout(Bitmap bbmp)
        {
            int w = (int)(bbmp.Width * times);
            int h = (int)(bbmp.Height * times);
            if (radioButton3.Checked)
            {
                Console.WriteLine("select3");
                Bitmap bigbmp = new Bitmap(w, h);

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        Color c = bmp.GetPixel((int)(x / times), (int)(y / times));
                        bigbmp.SetPixel(x, y, Color.FromArgb(c.R, c.G, c.B));
                    }
                }

                return bigbmp;
            }
            else
            {
                Console.WriteLine("select4");
                Bitmap bigbmp = new Bitmap(w, h, bmp.PixelFormat);
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        Color color1 = bbmp.GetPixel((int)(x / times), (int)(y / times));
                        Color color2 = bbmp.GetPixel((int)((x) / times)+1, (int)(y / times));
                        Color color3 = bbmp.GetPixel((int)(x / times), (int)(y  / times)+1);
                        Color color4 = bbmp.GetPixel((int)((x) / times)+1, (int)(y  / times)+1);

                        int avgR = (color1.R + color2.R + color3.R + color4.R) / 4;
                        int avgG = (color1.G + color2.G + color3.G + color4.G) / 4;
                        int avgB = (color1.B + color2.B + color3.B + color4.B) / 4;

                        bigbmp.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                    }
                }
                return bigbmp;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            times = Convert.ToDouble(textBox1.Text)/100;
            if (times > 1) {
                Console.WriteLine("BIG");
                pictureBox2.Image = zoomin(bmp);
                Console.WriteLine("BIG2");
            }
            else if (times < 1)
            {
                Console.WriteLine("SMALL");
                pictureBox2.Image = zoomout(bmp);
                Console.WriteLine("SMALL2");
            }
            else {
                pictureBox2.Image = null;
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
        }
    }
}
