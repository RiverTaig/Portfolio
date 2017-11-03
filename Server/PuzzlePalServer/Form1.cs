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

        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            label1.Text = e.X + "," + e.Y;
        }
    }
}
