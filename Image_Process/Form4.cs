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
    public partial class Form4 : Form
    {
        
        Bitmap img1,img2,frontimg,backimg;
        Graphics pic ;

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            frontimg = img1;
            backimg = img2;
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Transparent();
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            frontimg = img2;
            backimg = img1;
            
        }

        public Form4(Image img)
        {
            InitializeComponent();
            img1 = (Bitmap)img;
            OpenFileDialog openPic = new OpenFileDialog();
            if (openPic.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pcx openPcx = new pcx(openPic.FileName);
                    img2 = openPcx.PcxImage;
                }
                catch (NullReferenceException ex) { MessageBox.Show(ex.Message); }
            }
            pictureBox1.Image = img1;
            pictureBox2.Image = img2;
        }

        private void Transparent()
        {
            Bitmap temp = new Bitmap(img2.Width,img2.Height);

            if (radioButton1.Checked && radioButton2.Checked)
                MessageBox.Show("請選擇前/後景");
            else
            {
                for (int i = 0; i < frontimg.Height; i++)
                {
                    for (int j = 0; j < frontimg.Width; j++)
                    {
                        Color color = frontimg.GetPixel(i, j);
                        Color color1 = backimg.GetPixel(i, j);
                        temp.SetPixel(i, j, Color.FromArgb((byte)(((double)trackBar1.Value / 10.0) * color.R + (1.0 - ((double)trackBar1.Value / 10.0)) * color1.R),
                            (byte)(((double)trackBar1.Value / 10.0) * color.G + (1.0 - ((double)trackBar1.Value / 10.0)) * color1.G),
                            (byte)(((double)trackBar1.Value / 10.0) * color.B + (1.0 - ((double)trackBar1.Value / 10.0)) * color1.B)));
                    }
                }
                pictureBox3.Image = temp;
            }
        }
    }
}
