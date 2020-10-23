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

namespace DefectMarker
{
    public partial class Form1 : Form
    {
        List<string> Files = new List<string>();
        Marker Marker = new Marker();
        int showPicSN = 0;
        bool isDrawing = false;
        Point PointStart = Point.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        public void RefreshPicBox(string file)
        {
            if (pbMain.Image != null) { pbMain.Image.Dispose(); }
            using (FileStream fs = File.OpenRead(file))
            {
                pbMain.Image = Image.FromStream(fs);
            }
        }

        private void preImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showPicSN - 1 >= 0)
            {
                showPicSN--;
                RefreshPicBox(Files[showPicSN]);
            }
            else
            {
                MessageBox.Show("已經是第一張圖片");
            }
        }

        private void nextImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showPicSN + 1 < Files.Count())
            {
                showPicSN++;
                RefreshPicBox(Files[showPicSN]);
            }
            else
            {
                MessageBox.Show("已經是最後一張圖片");
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue.ToString())
            {
                //PageUp
                case "33":
                    preImageToolStripMenuItem_Click(this, e);
                    break;
                //PageDown
                case "34":
                    nextImageToolStripMenuItem_Click(this, e);
                    break;
                default:
                    break;
            }
        }

        private void loadSelectRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear File List
            Files.Clear();
            //Get File List
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"E:\Hitachi Qtz\algoSample";
            ofd.Filter = "Png files (*.png)|*.png|Jpg files (*.jpg)|*.jpg";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var fileName in ofd.FileNames)
                {
                    Files.Add(fileName);
                }
                showPicSN = 0;
                RefreshPicBox(Files[showPicSN].ToString());
            }
        }

        private void Paint()
        {
            Graphics g = this.pbMain.CreateGraphics();

        }

        class point
        {
            int x, y;
        }

        private void pbMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                PointStart = new Point() { X = e.X, Y = e.Y };
            }
            else
            {
                Bitmap b = new Bitmap(pbMain.Width, pbMain.Height);

                Graphics g = Graphics.FromImage(b);

                Pen p = new Pen(Color.Black);

                g.DrawLine(p, 10, 10, 20, 20);

                pbMain.Image = b;
            }
        }

        private void pbMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Bitmap bt = new Bitmap(pbMain.Width, pbMain.Height);
                Graphics g = Graphics.FromImage(bt);
                g.DrawRectangle(new Pen(Color.Red,1), new Rectangle() { X = PointStart.X, Y = PointStart.Y, Width = e.X - PointStart.X, Height = e.Y - PointStart.Y });
                pbMain.Image = bt;
            }
        }

        private void pbMain_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                isDrawing = false;
            }
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
