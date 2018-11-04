using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Process
{
    public partial class Form5 : Form
    {
        public Form5(Image image)
        {
            InitializeComponent();

            Bitmap bmp = new Bitmap(image);

            int mask = 0x01;
            Bitmap bmp_0 = new Bitmap(image);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (c.R + c.G + c.B) / 3;
                    int newgray = ((gray & mask) / mask) * 255;
                    bmp_0.SetPixel(x, y, Color.FromArgb(newgray, newgray, newgray));
                }
            }
            pictureBox1.Image = bmp_0;

            mask = 0x02;
            Bitmap bmp_1 = new Bitmap(image);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (c.R + c.G + c.B) / 3;
                    int newgray = ((gray & mask) / mask) * 255;
                    bmp_1.SetPixel(x, y, Color.FromArgb(newgray, newgray, newgray));
                }
            }
            pictureBox2.Image = bmp_1;

            mask = 0x04;
            Bitmap bmp_2 = new Bitmap(image);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (c.R + c.G + c.B) / 3;
                    int newgray = ((gray & mask) / mask) * 255;
                    bmp_2.SetPixel(x, y, Color.FromArgb(newgray, newgray, newgray));
                }
            }
            pictureBox3.Image = bmp_2;

            mask = 0x08;
            Bitmap bmp_3 = new Bitmap(image);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (c.R + c.G + c.B) / 3;
                    int newgray = ((gray & mask) / mask) * 255;
                    bmp_3.SetPixel(x, y, Color.FromArgb(newgray, newgray, newgray));
                }
            }
            pictureBox4.Image = bmp_3;

            mask = 0x10;
            Bitmap bmp_4 = new Bitmap(image);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (c.R + c.G + c.B) / 3;
                    int newgray = ((gray & mask) / mask) * 255;
                    bmp_4.SetPixel(x, y, Color.FromArgb(newgray, newgray, newgray));
                }
            }
            pictureBox5.Image = bmp_4;

            mask = 0x20;
            Bitmap bmp_5 = new Bitmap(image);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (c.R + c.G + c.B) / 3;
                    int newgray = ((gray & mask) / mask) * 255;
                    bmp_5.SetPixel(x, y, Color.FromArgb(newgray, newgray, newgray));
                }
            }
            pictureBox6.Image = bmp_5;

            mask = 0x40;
            Bitmap bmp_6 = new Bitmap(image);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (c.R + c.G + c.B) / 3;
                    int newgray = ((gray & mask) / mask) * 255;
                    bmp_6.SetPixel(x, y, Color.FromArgb(newgray, newgray, newgray));
                }
            }
            pictureBox7.Image = bmp_6;

            mask = 0x80;
            Bitmap bmp_7 = new Bitmap(image);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    int gray = (c.R + c.G + c.B) / 3;
                    int newgray = ((gray & mask) / mask) * 255;
                    bmp_7.SetPixel(x, y, Color.FromArgb(newgray, newgray, newgray));
                }
            }
            pictureBox8.Image = bmp_7;
        }
    }
}
