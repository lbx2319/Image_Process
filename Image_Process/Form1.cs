using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            pictureBox2.Image = function.Invert((Bitmap)currentimage);
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
            Form2 form2 = new Form2(currentimage);
            form2.Show();
        }
    }
}
