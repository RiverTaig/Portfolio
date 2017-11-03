using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace PuzzlePalServer
{
    public class Patch
    {
        public Patch(List<PuzzleVertex> verticies) {
            Verticies = verticies;
        }
        public List<PuzzleVertex> Verticies
        {
            get;set;
        }
        public bool IsValid
        {
            get;set;
        }
        public Piece Piece
        {
            get;set;
        }
        private Envelope _envelope = null;
        public Envelope Envelope{
            get{
                if (_envelope == null)
                {
                    Envelope returnEnv = new Envelope();
                    foreach (PuzzleVertex pv in Verticies)
                    {
                        if (pv.X < returnEnv.XMin)
                        {
                            returnEnv.XMin = pv.X;
                        }
                        if (pv.X > returnEnv.XMax)
                        {
                            returnEnv.XMax = pv.X;
                        }
                        if (pv.Y < returnEnv.YMin)
                        {
                            returnEnv.YMin = pv.Y;
                        }
                        if (pv.Y > returnEnv.YMax)
                        {
                            returnEnv.YMax = pv.Y;
                        }
                    }
                    _envelope = returnEnv;
                }
                return _envelope;
            }
        }
        public double Area
        {
            get
            {
                return (Envelope.XMax - Envelope.XMin) * (Envelope.YMax - Envelope.YMin);
            }
        }
        public double SquareNess
        {
            get
            {
                double denominator = Envelope.YMax - Envelope.YMin;
                if (denominator == 0)
                {
                    denominator =1;
                }
                double sq = ((double)Envelope.XMax - (double)Envelope.XMin*1.00) / (denominator);
                return (Envelope.XMax - Envelope.XMin) / (Envelope.YMax - Envelope.YMin);
            }
        }
        public Color PatchBackgroundColor
        {
            get;
            set;
        }
        public PageLocation PageLocation
        {
            get
            {
                return new PageLocation(this);
            }
        }
    }
    public class PageLocation
    {
        public PageLocation(Patch patch)
        {
            Point ll = patch.Envelope.LL;
            Point ul = patch.Envelope.UL;
            Point lr = patch.Envelope.LR;
            Point ur = patch.Envelope.UR;
            double minDistLL = double.MaxValue;
            double minDistLR = double.MaxValue;
            double minDistUR = double.MaxValue;
            double minDistUL = double.MaxValue;
            foreach (PuzzleVertex pv in patch.Verticies)
            {
                double llD = GetDistance(pv, ll);
                double lrD = GetDistance(pv, lr);
                double urD = GetDistance(pv, ur);
                double ulD = GetDistance(pv, ul);
                if (llD < minDistLL)
                {
                    minDistLL = llD;
                    PageLocationLL = new Point( pv.X, pv.Y);
                }
                if (lrD < minDistLR)
                {
                    minDistLR = lrD;
                    PageLocationLR = new Point(pv.X, pv.Y);
                }
                if (ulD < minDistUL)
                {
                    minDistUL = ulD;
                    PageLocationUL = new Point(pv.X, pv.Y);
                }
                if (urD < minDistUR)
                {
                    minDistUR = urD;
                    PageLocationUR = new Point(pv.X, pv.Y);
                }
            }
        }
        double GetDistance(PuzzleVertex pv, Point pnt)
        {
            //no need to calculate the square root here?
            return Math.Sqrt(((pv.X - pnt.X) * (pv.X - pnt.X)) + ((pv.Y - pnt.Y) * (pv.Y - pnt.Y)));
        }
        public Point PageLocationUR
        {
            get;

        }

        public Point PageLocationUL
        {
            get;
        }
        public Point PageLocationLL
        {
            get;
        }
        public Point PageLocationLR
        {
            get;
        }
    }
    public class Envelope {
        public Envelope(){
            XMin = int.MaxValue;
            XMax = int.MinValue;
            YMin = XMin;
            YMax = XMax;
        }
        public int XMin { get; set; }
        public int YMin { get; set; }
        public int XMax { get; set; }
        public int YMax { get; set; }
        public Point LL {
            get { return new Point(XMin, YMax); }
        }
        public Point LR
        {
            get { return new Point(XMax, YMax); }
        }
        public Point UL
        {
            get { return new Point(XMin, YMin); }
        }
        public Point UR
        {
            get { return new Point(XMax, YMin); }
        }
    }
}
