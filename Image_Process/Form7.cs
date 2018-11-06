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
    public partial class Form7 : Form
    {
        Image image;
        public Form7(Image imageb)
        {
            InitializeComponent();
            pictureBox1.Image = imageb;
            image = imageb;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int angle = Convert.ToInt16(comboBox1.Text);
            int width = image.Width;
            int height = image.Height;
            double r = Math.Sqrt(Math.Pow((double)width / 2d, 2d) + Math.Pow((double)height / 2d, 2d)); //半徑L
            double OriginalAngle = Math.Acos((width / 2d) / r) / Math.PI * 180d;  //對角線和X軸的角度θ
            double minW = 0d, maxW = 0d, minH = 0d, maxH = 0d; //最大和最小的 X、Y座標
            double[] drawPoint = new double[4];

            drawPoint[0] = (-OriginalAngle + angle) * Math.PI / 180d;
            drawPoint[1] = (OriginalAngle + angle) * Math.PI / 180d;
            drawPoint[2] = (180f - OriginalAngle + angle) * Math.PI / 180d;
            drawPoint[3] = (180f + OriginalAngle + angle) * Math.PI / 180d;

            foreach (double point in drawPoint) //由四個角的點算出X、Y的最大值及最小值
            {
                double x = r * Math.Cos(point);
                double y = r * Math.Sin(point);

                if (x < minW)
                    minW = x;
                if (x > maxW)
                    maxW = x;
                if (y < minH)
                    minH = y;
                if (y > maxH)
                    maxH = y;
            }            

            if (radioButton1.Checked)
            {

            }
            else
            {
                PointF offset = new PointF((float)image.Width / 2, (float)image.Height / 2);

                //create a new empty bitmap to hold rotated image
                Bitmap rotatedBmp = new Bitmap((int)(maxW - minW), (int)(maxH - minH));
                //rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                //make a graphics object from the empty bitmap
                Graphics g = Graphics.FromImage(rotatedBmp);

                //Put the rotation point in the center of the image
                //g.TranslateTransform(offset.X, offset.Y);
                g.TranslateTransform(offset.X, offset.Y);
                //rotate the image
                g.RotateTransform(angle);

                //move the image back
                g.TranslateTransform(-offset.X, -offset.Y);

                //draw passed in image onto graphics object
                //g.DrawImage(image, (float)((int)(maxW - minW) - image.Width) / 2f, (float)((int)(maxH - minH) - image.Height)/2f, image.Width, image.Height);
                //g.DrawImage(image, (float)(newSize.Width - image.Width) / 2f, (float)(newSize.Height - image.Height) / 2f, image.Width, image.Height);
                g.DrawImage(image, (float)(r * Math.Cos(drawPoint[3])), (float)(r * Math.Sin(drawPoint[3])), image.Width, image.Height);
                //g.DrawImage(image, new Point(80, 36));
                pictureBox2.Image = rotatedBmp;
            }
        }
    }
}
