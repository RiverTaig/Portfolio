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
    public partial class Form2 : Form
    {
        bool _refresh = false;
        bool _rotate = false;
        bool _drawing = false;
        List<Point> _points = new List<Point>(100);
        List<Point> _allPoints = new List<Point>();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                lblVertexCount.Text = "0";
                _rotate = false;
                _drawing = true;
                //_refresh = true;
                _allPoints.Clear();
                timer1.Enabled = false;
                this.Refresh();
                
            }
            _refresh = false;
            
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            _refresh = true;
            _drawing = false;
            _rotate = true;
            timer1.Enabled = true;
            
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            lblXY.Text = e.X.ToString() + "," + e.Y.ToString();
            if (_drawing)
            {
                Point pnt = new Point(e.X, e.Y);
                _allPoints.Add(pnt);
                _refresh = true;
                this.Refresh();
                lblVertexCount.Text = (Convert.ToInt16(lblVertexCount.Text) + 1).ToString();
            }
            
        }
        private Dictionary<int,List<Point>> RotationCoordsAllVerticies
        {
            get;set;
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }
        int _rotateCounter = 0;
        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (_refresh)
                {
                    double cullAfter = _allPoints.Count / 100.0;
                    List<Point> points = new List<Point>();
                    points.Add(_allPoints[0]);
                    int counter = 1;

                    for (int i = 0; (i < _allPoints.Count);i = Convert.ToInt16( counter * cullAfter))
                    {
                        points.Add( _allPoints[i]);
                        counter++;
                    }
                    //lblAllCulled.Text = _allPoints.Count + "-" + points.Count;
                    _points = points;
                    e.Graphics.DrawLines(Pens.Black, points.ToArray());
                    _refresh = false;
                }
                if (_rotate)
                {
                    if (RotationCoordsAllVerticies == null)
                    {
                        SetRotationCoords();
                    }
                    if (_degree == 0)
                    {
                    }
                    var verticiesAtRotation = RotationCoordsAllVerticies;
                    e.Graphics.DrawLines(Pens.Blue, verticiesAtRotation[_degree].ToArray());
                    //List<Point> rotatedPoints = new List<Point>();
                    //List<int> pointIDS = new List<int>();
                    //for (int i = _points.Count - 1; i > 0; i--)
                    //{
                    //    var rotationCoordsForThisVertex = verticiesAtRotation[i];
                    //    var rotationCoordsForThisVertexAtCurrentDegree = rotationCoordsForThisVertex.Where(j => j.Item2 == _degree);
                    //    foreach (var whatever in rotationCoordsForThisVertexAtCurrentDegree)
                    //    {
                    //        pointIDS.Add((int)whatever.Item1);
                    //        int xAsInt = Convert.ToInt16(whatever.Item3);
                    //        int yasInt = Convert.ToInt16(whatever.Item4);
                    //        rotatedPoints.Add(new Point(xAsInt, yasInt));
                    //    }

                    //    //var rotationCoordsForThisVertexAtCurrentDegree = rotationCoordsForThisVertex[_degree];
                    //    //int xAsInt = Convert.ToInt16(rotationCoordsForThisVertexAtCurrentDegree.Item3);
                    //    //int yasInt = Convert.ToInt16(rotationCoordsForThisVertexAtCurrentDegree.Item4);
                    //    //rotatedPoints.Add(new Point(xAsInt, yasInt));
                    //}
                    //e.Graphics.DrawLines(Pens.Blue, rotatedPoints.ToArray());
                    //foreach(var v in rotatedPoints)
                    //{
                    //    Console.WriteLine(v.X + ":" + v.Y);
                    //    System.Diagnostics.Trace.WriteLine(v.X + "," + v.Y);
                    //}
                    //label5.Text = _rotateCounter.ToString();
                }
            }
            catch
            {

            }
        }
        private void SetRotationCoords()
        {
            Point firstPoint = _points[50];
            var xOffset = firstPoint.X;
            var yOffset = firstPoint.Y;
            Point lastPoint = _points[_points.Count - 1];
            var radianConversion = Math.PI / 180.0;
            List<double> ds = new List<double>();
            Dictionary<int, List<Point>> rotationDictionary = new Dictionary<int, List<Point>>();
            for(int angle = 0; angle < 360; angle++)
            {
                if(angle == 89)
                {

                }
                List<Point> rotatedCoordinates = new List<Point>();
                for (int p = 0; p < _points.Count; p++)
                {
                    var thisPoint = _points[_points.Count - 1 - p];
                    var x0 = thisPoint.X - xOffset;
                    var y0 = thisPoint.Y - yOffset;
                    var sinPhi = Math.Sin(angle* radianConversion);
                    var cosPhi = Math.Cos(angle * radianConversion);
                    var x1 = (x0 * cosPhi) - (y0 * sinPhi);
                    var y1 = (y0 * cosPhi) + (x0 * sinPhi);
                    Point rotatedPoint = new Point((int)(x1 + xOffset), (int)(y1 + yOffset));
                    rotatedCoordinates.Add(rotatedPoint);
                }
                rotationDictionary.Add(angle,rotatedCoordinates);
            }
            RotationCoordsAllVerticies = rotationDictionary;
        }
        private void SetRotationCoords2()
        {
            Point firstPoint = _points[0];
            Point lastPoint = _points[_points.Count - 1];

            List<double> ds = new List<double>();
            //pointID,angle,x,y
            List<List<Tuple<double, int, double, double>>> rotationCoordsAllVerticies = new List<List<Tuple<double, int, double, double>>>();
            for (int p = 0; p < _points.Count; p++)
            {
                var thisPoint = _points[_points.Count - 1-p];
                var dd = Math.Pow(firstPoint.X - thisPoint.X, 2) + Math.Pow(firstPoint.Y - thisPoint.Y, 2);
                double d = Math.Sqrt(dd);
                ds.Add(d);
                double h = firstPoint.X;// 0?
                double k = firstPoint.Y; // 0?
                var xAtZeroDegrees = firstPoint.X + d;  //ultimately xAtZeroDegrees is just d
                var partionsOfX = d * 2.0 / 180.0;
                //pointID,angle,x,y
                List<Tuple<double, int, double, double>> rotationCoordsThisVertex = new List<Tuple<double, int, double, double>>();
                double minY = 99999;
                double maxY = -9999;
                double minX = 99999;
                double maxX = -9999;
                for (int i = 0; i < 180; i++)
                {

                    // ((x-h)^2 + (y-k)^2 = r^2
                    double xAtThisPartition = xAtZeroDegrees - (i * partionsOfX);
                    //Quadratic equation
                    double a = 1;
                    double b = -2 * k;
                    double c =  ((k * k) + ((xAtThisPartition - h) * (xAtThisPartition - h)) - (d*d));
                    var xHereMiush = xAtThisPartition - h;
                    var xHereMinushSquared = Math.Pow(xHereMiush, 2.0);
                    if (i == 0)
                    {

                    }
                    double bSquaredMinus4ac = (b * b) - (4 * a * c);
                    double sqrtOfbSquaredMinus4ac = Math.Sqrt(bSquaredMinus4ac);
                    double negBPlussqrtOfbSquaredMinus4ac = (-1 * b) + sqrtOfbSquaredMinus4ac;
                    double negBMinussqrtOfbSquaredMinus4ac = (-1 * b) - sqrtOfbSquaredMinus4ac;
                    double negBPlussqrtOfbSquaredMinus4acOver2a = negBPlussqrtOfbSquaredMinus4ac / (2 * a);
                    double negBMinussqrtOfbSquaredMinus4acOver2a = negBMinussqrtOfbSquaredMinus4ac / (2 * a);
                    Tuple<double, int, double, double> rotationCoord1 = new Tuple<double, int, double, double>(p, i, xAtThisPartition, negBPlussqrtOfbSquaredMinus4acOver2a);
                    Tuple<double, int, double, double> rotationCoord2= new Tuple<double, int, double, double>(p, 360-i, xAtThisPartition, negBMinussqrtOfbSquaredMinus4acOver2a);
                    /*if(negBMinussqrtOfbSquaredMinus4acOver2a > maxY)
                    {
                        maxY = negBMinussqrtOfbSquaredMinus4acOver2a;
                    }
                    if (negBPlussqrtOfbSquaredMinus4acOver2a > maxY)
                    {
                        maxY = negBPlussqrtOfbSquaredMinus4acOver2a;
                    }
                    if (negBMinussqrtOfbSquaredMinus4acOver2a <minY)
                    {
                        minY = negBMinussqrtOfbSquaredMinus4acOver2a;
                    }
                    if (negBPlussqrtOfbSquaredMinus4acOver2a < minY)
                    {
                        minY = negBPlussqrtOfbSquaredMinus4acOver2a;
                    }
                    //
                    if (negBMinussqrtOfbSquaredMinus4acOver2a > maxY)
                    {
                        maxY = negBMinussqrtOfbSquaredMinus4acOver2a;
                    }
                    if (negBPlussqrtOfbSquaredMinus4acOver2a > maxY)
                    {
                        maxY = negBPlussqrtOfbSquaredMinus4acOver2a;
                    }
                    if (negBMinussqrtOfbSquaredMinus4acOver2a < minY)
                    {
                        minY = negBMinussqrtOfbSquaredMinus4acOver2a;
                    }
                    if (negBPlussqrtOfbSquaredMinus4acOver2a < minY)
                    {
                        minY = negBPlussqrtOfbSquaredMinus4acOver2a;
                    }*/
                    if (i == 0)
                    {

                    }
                    rotationCoordsThisVertex.Add(rotationCoord1);
                    rotationCoordsThisVertex.Add(rotationCoord2);
                }
                rotationCoordsAllVerticies.Add(rotationCoordsThisVertex);
                //foreach (var v in rotationCoordsThisVertex)
                //{
                //    //Console.WriteLine(v.Item3 + ":" + v.Item4);
                //    System.Diagnostics.Trace.WriteLine(v.Item3 + "," + v.Item4);
                //}
            }

            //RotationCoordsAllVerticies = rotationCoordsAllVerticies;
        }

        private void numRPM_ValueChanged(object sender, EventArgs e)
        {
            if(numRPM.Value == 0)
            {
                timer1.Enabled = false;
                return;
            }
            timer1.Enabled = true;
            var interval = Convert.ToInt32(100.0 / Convert.ToDouble(numRPM.Value));
            timer1.Interval = interval == 0 ? 1 : interval ;
        }
        int _degree = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            return;
            if (_rotate)
            {
                _degree++;
                if(_degree == 360)
                {
                    _degree = 0;
                }
                if(_degree == 45)
                {
                    //this.Refresh();
                }
                //this.Refresh();
            }
        }

        private void numAngle_ValueChanged(object sender, EventArgs e)
        {
            _degree = (int) numAngle.Value;
            _rotate = true;
            this.Refresh();

        }
    }
}

