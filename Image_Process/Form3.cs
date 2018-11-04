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
    public partial class Form3 : Form
    {
        private const int GrayNum = 256;        //灰度值
        private int[] GrayArr = new int[GrayNum];   //灰度值数组
        private int[] PixArr;
        private double w0;  //背景灰度概率
        private double w1;  //前景灰度概率
        private int IMG_HEIGHT;
        private int IMG_WIDTH;
        private int PixNum;
        private Bitmap pic;
        private Bitmap oppic;

        public Form3(Image inputimg)
        {
            InitializeComponent();
            pictureBox1.Image = inputimg;
            Bitmap tempbmp = (Bitmap)inputimg;
            pic =tempbmp.Clone(new Rectangle(0,0,inputimg.Width,inputimg.Height),PixelFormat.Format24bppRgb);
            oppic = new Bitmap(pic.Height, pic.Width);
            Ostu(pic);
        }

        public void Ostu(Bitmap pic)
        {
            //this.pic = pic;
            IMG_HEIGHT = pic.Height;
            IMG_WIDTH = pic.Width;
            PixNum = IMG_HEIGHT * IMG_WIDTH;
            PixArr = new int[PixNum];
            SetGrayArr();
        }
        /// <summary>
        /// 获取灰度值数组，并求每种灰度所占的比例
        /// </summary>
        /// <returns>无</returns>
        private void SetGrayArr()
        {

            for (int i = 0; i < IMG_HEIGHT; i++)
            {
                for (int j = 0; j < IMG_WIDTH; j++)
                {
                    Color color = pic.GetPixel(j, i);
                    int gray = (int)(0.39 * color.R + 0.50 * color.G + 0.11 * color.B);
                    PixArr[i * IMG_WIDTH + j] = gray;            //设置像素数组
                    GrayArr[PixArr[i * IMG_WIDTH + j]]++;   //求灰度数组,元素为每种灰度的数量
                }
            }
        }
        /// <summary>
        /// 获取阈值
        /// </summary>
        /// <returns>阈值</returns>
        private int GetVal()
        {
            double u0;  //背景灰度均值
            double u1;  //前景灰度均值
            double maxVal = 0;  //类间方差最大值
            int endval = 0;
            for (int i = 0; i < GrayNum; i++)
            {
                w1 = w0 = u0 = u1 = 0;
                for (int j = 0; j < GrayNum; j++)
                {
                    if (j <= i)
                    {
                        w0 += GrayArr[j] / (PixNum * 1.0);  //每种灰度的概率
                        u0 += GrayArr[j] / (PixNum * 1.0) * j;
                    }
                    else
                    {
                        w1 += GrayArr[j] / (PixNum * 1.0);
                        u1 += GrayArr[j] / (PixNum * 1.0) * j;
                    }
                }
                u0 = u0 / w0;
                u1 = u1 / w1;
                double val = w1 * w0 * Math.Pow(u0 - u1, 2);
                if (maxVal < val)
                {
                    maxVal = val;
                    endval = i;  //阈值
                }
            }
            return endval;
        }
        /// <summary>
        /// 图像二值化
        /// </summary>
        /// <returns>无</returns>
        private void TurnGray(int val)
        {
            //Console.WriteLine("Val: {0}", val);
            for (int i = 0; i < IMG_HEIGHT; i++)
            {
                for (int j = 0; j < IMG_WIDTH; j++)
                {
                    Color color = pic.GetPixel(j, i);
                    int gray = (int)(0.39 * color.R + 0.50 * color.G + 0.11 * color.B);
                    //Console.Write("GRAY: {0}", gray);
                    if (gray > val)
                    {
                        oppic.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                     //   Console.WriteLine("X:{0} Y:{1} VALUE: 1", j, i);
                    }
                    else
                    {
                        oppic.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                     //   Console.WriteLine("X:{0} Y:{1} VALUE: 0", j, i);
                    }
                }
            }
        }
        /// <summary>
        /// 返回二值化后的图像
        /// </summary>
        /// <returns>图像</returns>
        public Bitmap RetrunPicture()
        {
            TurnGray(GetVal());
            return oppic;
        }
        public Bitmap RetrunPicture2(int val)
        {
            TurnGray(val);
            return oppic;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = RetrunPicture();
            trackBar1.Value = GetVal();
            textBox1.Text = GetVal().ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
            pictureBox2.Image = RetrunPicture2(trackBar1.Value);
           // Console.WriteLine(trackBar1.Value);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
            pictureBox2.Image = RetrunPicture2(trackBar1.Value);
            //Console.WriteLine(trackBar1.Value);
            //pictureBox2.Update();
        }
    }
}
