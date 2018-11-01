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
    public partial class Form2 : Form
    {
        public Form2(Image inputImage)
        {
            InitializeComponent();
            Bitmap bmp = new Bitmap(inputImage);
            Bitmap r = new Bitmap(inputImage.Width,inputImage.Height);
            Bitmap g = new Bitmap(inputImage.Width, inputImage.Height);
            Bitmap b = new Bitmap(inputImage.Width, inputImage.Height);
            for (int i = 0; i < inputImage.Width; i++)
            {
                for (int j = 0; j < inputImage.Height; j++)
                {
                    r.SetPixel(i, j, Color.FromArgb(bmp.GetPixel(i,j).R,0,0));
                    g.SetPixel(i, j, Color.FromArgb(0, bmp.GetPixel(i, j).G, 0));
                    b.SetPixel(i, j, Color.FromArgb(0, 0, bmp.GetPixel(i, j).B));
                }
            }

            pictureBox1.Image = r;
            pictureBox2.Image = g;
            pictureBox3.Image = b;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
