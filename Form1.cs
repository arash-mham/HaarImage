using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace HaarImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Load_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "bmp files (*.png)|*.png";

                if (dlg.ShowDialog() == DialogResult.OK)
                {               
                    pictureBox1.Image = new Bitmap(dlg.FileName);
                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Haar_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap OriginalBitmap = new Bitmap(pictureBox1.Image);
                int OriginWidth = (OriginalBitmap.Width / 3) * 3;
                int OriginHeight = (OriginalBitmap.Height / 3) * 3;
                Bitmap RowCompressedBitmap = new Bitmap(OriginHeight, pictureBox1.Image.Height / 3);
                Bitmap CompressedBitmap = new Bitmap(pictureBox1.Image.Width / 3, pictureBox1.Image.Height / 3);
               
                for (int i = 0; i < OriginWidth; i++)
                {
                    for (int j = 0; j < OriginHeight; j += 3)
                    {
                        byte R = (byte)(((int)OriginalBitmap.GetPixel(i, j).R + (int)OriginalBitmap.GetPixel(i, j + 1).R + (int)OriginalBitmap.GetPixel(i, j + 2).R) / 3);
                        byte G = (byte)(((int)OriginalBitmap.GetPixel(i, j).G + (int)OriginalBitmap.GetPixel(i, j + 1).G + (int)OriginalBitmap.GetPixel(i, j + 2).G) / 3);
                        byte B = (byte)(((int)OriginalBitmap.GetPixel(i, j).B + (int)OriginalBitmap.GetPixel(i, j + 1).B + (int)OriginalBitmap.GetPixel(i, j + 2).B) / 3);
                        Color myRgbColor = new Color();
                        myRgbColor = Color.FromArgb(R, G, B);
                        RowCompressedBitmap.SetPixel(i, j / 3, myRgbColor);
                    }
                }
                for (int i = 0; i < RowCompressedBitmap.Width ; i += 3)
                {
                    for (int j = 0; j < RowCompressedBitmap.Height; j++)
                    {
                        byte R = (byte)(((int)RowCompressedBitmap.GetPixel(i, j).R + (int)RowCompressedBitmap.GetPixel(i + 1, j).R + (int)RowCompressedBitmap.GetPixel(i + 2, j).R) / 3);
                        byte G = (byte)(((int)RowCompressedBitmap.GetPixel(i, j).G + (int)RowCompressedBitmap.GetPixel(i + 1, j).G + (int)RowCompressedBitmap.GetPixel(i + 2, j).G) / 3);
                        byte B = (byte)(((int)RowCompressedBitmap.GetPixel(i, j).B + (int)RowCompressedBitmap.GetPixel(i + 1, j).B + (int)RowCompressedBitmap.GetPixel(i + 2, j).B) / 3);
                        Color myRgbColor = new Color();
                        myRgbColor = Color.FromArgb(R, G, B);
                        CompressedBitmap.SetPixel(i / 3, j, myRgbColor);
                    }
                }
                pictureBox1.Image = new Bitmap(CompressedBitmap);
            }
            
        }

        private void Save_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog save_image = new SaveFileDialog())
            {
                save_image.ShowDialog();
                string name = save_image.FileName;
                pictureBox1.Image.Save(name, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

       }
}
