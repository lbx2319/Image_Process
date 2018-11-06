using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Process
{
    public partial class Form1 : Form
    {
        Image currentimage;
        Function function;
        Filter filter;
        public Bitmap color = new Bitmap(400, 400, PixelFormat.Format24bppRgb);
        public Form1()
        {
            InitializeComponent();
            function = new Function();
            filter = new Filter();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openPic = new OpenFileDialog();
            if (openPic.ShowDialog() == DialogResult.OK) {
                try
                {
                    pcx openPcx = new pcx(openPic.FileName);
                    pictureBox1.Image = openPcx.PcxImage;
                    currentimage = openPcx.PcxImage;
                    label1.Text = string.Format("FileName: {0}     Dimension:{1}x{2}",Path.GetFileName(openPic.FileName)
                        ,openPcx.header.Width.ToString(),openPcx.header.Height.ToString());
                    function.Draw_Histogram(currentimage, ref pictureBox4);
                    label33.Text = openPcx.header.Manufacturer.ToString();
                    label32.Text = openPcx.header.Version.ToString();
                    label31.Text = openPcx.header.Encoding.ToString();
                    label30.Text = openPcx.header.Bits_Per_Pixel.ToString();
                    label29.Text = openPcx.header.Xmin.ToString();
                    label28.Text = openPcx.header.Ymin.ToString();
                    label27.Text = openPcx.header.Xmax.ToString();
                    label26.Text = openPcx.header.Ymax.ToString();
                    label25.Text = openPcx.header.Hres1.ToString();
                    label24.Text = openPcx.header.Vres1.ToString();
                    label23.Text = openPcx.header.Reserved.ToString();
                    label22.Text = openPcx.header.Colour_Planes.ToString();
                    label21.Text = openPcx.header.Bytes_Per_Line.ToString();
                    label20.Text = openPcx.header.Palette_Type.ToString();
                    int hs = openPcx.header.HscreenSize + openPcx.header.HscreenSize1 * 256;
                    int vs = openPcx.header.VscreenSize + openPcx.header.VscreenSize1 * 256;
                    label19.Text = hs.ToString();
                    label18.Text = vs.ToString();
                    FileStream file = new FileStream(openPic.FileName, FileMode.Open);                   
                    int dl = System.Convert.ToInt32(file.Length);
                    int n = 0;
                    for (int y = 0; y < color.Height; y++)//調色盤
                    {
                        for (int x = 0; x < color.Width; x++)
                        {
                            n = (dl - 768) + (((y / 25) * 16 + (x / 25)) * 3);//0~24都是同一個color map 格子 8/8/8
                            color.SetPixel(x, y, Color.FromArgb(openPcx.data[n], openPcx.data[n + 1], openPcx.data[n + 2]));
                        }
                    }
                    pictureBox5.Image = color;
                    functionToolStripMenuItem.Enabled = true;
                    filterToolStripMenuItem.Enabled = true;
                }
                catch (NullReferenceException ex) { MessageBox.Show(ex.Message); }
            }

            
        }
        
        private void getPixel(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Bitmap tempimg = (Bitmap)currentimage;
            toolStripStatusLabel1.Text = string.Format("X:{0}, Y:{1}", e.X, e.Y);
            if(tempimg!=null)
            if ((e.X < tempimg.Width) && (e.Y < tempimg.Height))
                toolStripStatusLabel2.Text = string.Format("R:{0}, G:{1}, B:{2}",tempimg.GetPixel(e.X, e.Y).R.ToString(),
                tempimg.GetPixel(e.X,e.Y).G.ToString(), tempimg.GetPixel(e.X, e.Y).B.ToString());
        }
        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            Bitmap tempimg = (Bitmap)pictureBox4.Image;
            toolStripStatusLabel1.Text = string.Format("X:{0}, Y:{1}", e.X, e.Y);
            /*if (tempimg != null)
                if ((e.X < tempimg.Width) && (e.Y < tempimg.Height))
                    toolStripStatusLabel2.Text = string.Format("R:{0}, G:{1}, B:{2}", tempimg.GetPixel(e.X, e.Y).R.ToString(),
                    tempimg.GetPixel(e.X, e.Y).G.ToString(), tempimg.GetPixel(e.X, e.Y).B.ToString());*/
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = function.Negative((Bitmap)currentimage);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            function.Draw_Histogram(currentimage, ref pictureBox4);
        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = function.gray((Bitmap)currentimage);
        }

        private void smoothingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = filter.smoothing(currentimage);
        }

        private void rGB分量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RGB form2 = new RGB(currentimage);
            form2.Show();
        }

        private void thresholdingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(currentimage);
            form3.Show();
        }

        private void transparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(currentimage);
            form4.Show();
        }

        private void bitplaneSlicingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(currentimage);
            form5.Show();
        }

        private void hToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = function.mirrorh(currentimage);
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = function.mirrorv2(currentimage);
        }

        private void diagonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = function.mirrord(currentimage);
        }

        private void histogramEqualizationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = function.gray(currentimage);
            pictureBox2.Image = function.His_Equal(currentimage);
        }

        private void contrastStretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = function.gray(currentimage);
            pictureBox2.Image = function.contrast_stretch(currentimage);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6(currentimage);
            form6.Show();
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = function.mirrorh2(currentimage);
        }

        private void verticalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = function.mirrorv(currentimage);
        }

        private void diagonalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = function.mirrord2(currentimage);
        }

        private void diagonalToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = function.mirrord3(currentimage);
        }

        private void diagonalvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = function.mirrord4(currentimage);
        }

        private void rotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(currentimage);
            form7.Show();
        }

        private void watermarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = function.watermark(currentimage);
            Form5 form5 = new Form5(pictureBox3.Image);
            form5.Show();
        }
    }
}
