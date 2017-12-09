using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzlePalServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.X + "," + e.Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Puzzle puzzle = new Puzzle();
            puzzle.PuzzleID = new Guid("A1F6CDDF-8BD2-4985-B765-DF218DCF7AC5");
            puzzle.PuzzleRows = 5;
            puzzle.PuzzleColumns = 3;
            puzzle.DefaultPatchBoundaryColor = Color.Magenta;
            puzzle.IsDirty = true;
            puzzle.Low = 1;
            puzzle.High = 1024;
            puzzle.Owner = "River Taig";
            puzzle.PaidInFull = false;
            puzzle.PuzzleWidth = 40.0;
            puzzle.Comments = "This is a test puzzle";
            puzzle.PuzzleHeight = 30.0;
            puzzle.TotalPieces = 1024;
            puzzle.DefaultPatchDiscoveryThreshold = textBox1.Text.ToInt();
            puzzle.ProcessPuzzle();
            Bitmap bmp2 = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);

            label2.Text = puzzle.Pages[0].PossiblePatchBoundaryPoints.Count.ToString();
            Random r = new Random();
            int i = 0;
            foreach (var patch in puzzle.Pages[0].Patches)
            {
                i++;
                listBox1.Items.Add("UL " + i.ToString() + " : " +   patch.PageLocation.PageLocationUL.X + "," + patch.PageLocation.PageLocationUL.Y);
                listBox1.Items.Add("UR " + i.ToString() + " : " + patch.PageLocation.PageLocationUR.X + "," + patch.PageLocation.PageLocationUR.Y);
                listBox1.Items.Add("LL " + i.ToString() + " : " + patch.PageLocation.PageLocationLL.X + "," + patch.PageLocation.PageLocationLL.Y);
                listBox1.Items.Add("LR " + i.ToString() + " : " + patch.PageLocation.PageLocationLR.X + "," + patch.PageLocation.PageLocationLR.Y);
                Color randomColor = Color.FromArgb(255, r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
                foreach (PuzzleVertex pv in patch.Verticies)
                {

                    bmp2.SetPixel(pv.X, pv.Y, randomColor);
                }

            }
            bmp2.Save("C:\\temp\\bmp2.bmp");
            pictureBox1.Image = bmp2;
            this.Refresh();
        }
        private class SideFromTo
        {
            public SideFromTo (Point from, Point to,int xIncrementer, int yIncrementer)
            {
                FromPoint = from;
                ToPoint = to;
            }
            public Point FromPoint { get; set; }
            public Point ToPoint { get; set; }
        }

        private void DelineatePieces(Puzzle puzzle, int pageNumber)
        {
            
            Bitmap bmp = (Bitmap)Image.FromFile(puzzle.Pages[pageNumber].AssociatedImageFile);
            var page = puzzle.Pages[pageNumber];
            foreach (var patch in puzzle.Pages[pageNumber].Patches)
            {
                Point llPoint = patch.PageLocation.PageLocationLL;
                Queue <Point> qPoints = new Queue<Point>();
                for(int i = 5; i < 15; i++)
                {
                    if(patch.IsBackgroundColor(bmp, llPoint.X + i, llPoint.Y + i))
                    {
                        qPoints.Enqueue(new Point(llPoint.X + i, llPoint.Y + i));
                    }
                }
                HashSet<string> visitedCells = new HashSet<string>();
                List<Point> pieceBoundaryPoints = new List<Point>();
                while(qPoints.Count > 0)
                {
                    Point currentPoint = qPoints.Dequeue();

                    if (patch.IsBackgroundColor(bmp, currentPoint.X, currentPoint.Y) == false &&
                        page.IsPatchBoundary(bmp, currentPoint.X, currentPoint.Y) == false)
                    {
                        if (patch.IsBackgroundColor(bmp, currentPoint.X, currentPoint.Y) == false)
                        {
                            pieceBoundaryPoints.Add(new Point(currentPoint.X, currentPoint.Y));
                        }
                        continue;
                    }

                    //get the adjacent cells
                    for (int ax = -1; ax < 2; ax++)
                    {
                        for (int ay = -1; ay < 2; ay++)
                        {
                            if(ax  == 0 && ay == 0)
                            {
                                continue;
                            }
                            string key = currentPoint.X + ax + ":" + currentPoint.Y + ay;
                            if(visitedCells.Contains(key) == false)
                            {
                                qPoints.Enqueue(new Point(currentPoint.X + ax , currentPoint.Y + ay));
                            }
                        }
                    }
                }
            }
        }


        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            try
            {
                label1.Text = e.X + "," + e.Y;
                Bitmap bmp = pictureBox1.Image as Bitmap;
                if (e.X > bmp.Width || e.Y > bmp.Height)
                {
                    return;
                }
                LabelMaxMin(e, bmp);
            }
            catch { }
        }

        private void LabelMaxMin(MouseEventArgs e, Bitmap bmp)
        {
            int r = bmp.GetPixel(e.X, e.Y).R;
            int g = bmp.GetPixel(e.X, e.Y).G;
            int b = bmp.GetPixel(e.X, e.Y).B;
            lblCurrent.Text = r + "," + g + "," + b;
            Color currentColor = bmp.GetPixel(e.X, e.Y);
            panCurrent.BackColor = currentColor;
            if (r < (g+35) || r < (b+35))
            {
                panRed.BackColor = currentColor;
            }
            if (g < (b + 35) || g < (r + 35))
            {
                panGreen.BackColor = currentColor;
            }
            if (b < (r + 35) || b < (g + 35))
            {
                panBlue.BackColor = currentColor;
            }
            if (panCurrent.BackColor.R - 50 > panCurrent.BackColor.G && panCurrent.BackColor.R - 50 > panCurrent.BackColor.B && panCurrent.BackColor.R - 50 > panCurrent.BackColor.G)
            {
                lblIsBorder.Text = "border";
            }
            else
            {
                lblIsBorder.Text = "not";
            }

            if (_minR > r) { _minR = r; }
            if (_maxR < r) { _maxR = r; }
            if (_minG > g) { _minG = g; }
            if (_maxG < g) { _maxG = g; }
            if (_minB > b) { _minB = b; }
            if (_maxB < b) { _maxB = b; }
            Color mincolor = Color.FromArgb(_minR, _minG, _minB);
            Color maxcolor = Color.FromArgb(_maxR, _maxG, _maxB);
            panMax.BackColor = maxcolor;
            panMin.BackColor = mincolor;
            lblMin.Text = mincolor.R + "," + mincolor.G + "," + mincolor.B;
            lblMax.Text = maxcolor.R + "," + maxcolor.G + "," + maxcolor.B;
            label3.Text = string.Format("{0},{1},{2} - {3},{4},{5}", _minR, _minG, _minB, _maxR, _maxG, _maxB);
        }

        private int _maxR = -1;
        private int _minR = 300;
        private int _maxG = -1;
        private int _minG = 300;
        private int _maxB = -1;
        private int _minB = 300;
        private void label3_Click(object sender, EventArgs e)
        {


            _maxR = -1;
            _minR = 300;
            _maxG = -1;
            _minG = 300;
            _maxB = -1;
            _minB = 300;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _maxR = -1;
            _minR = 300;
            _maxG = -1;
            _minG = 300;
            _maxB = -1;
            _minB = 300;
            panMin.BackColor = Color.White;
            panMin.BackColor = Color.Black;
            panRed.BackColor = Color.Red;
            panWhite.BackColor = Color.White;
            panBlack.BackColor = Color.Black;
            panBlue.BackColor = Color.Blue;
            panGreen.BackColor = Color.Green;

            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            var listOfStrings = (new String[] { "One", "Two", "Three" }).ToList();
        }
    }
   
}
