using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace PuzzlePalServer
{
    public class Page
    {
        public Puzzle Puzzle
        {
            get;
            set;
        }
        public string Directory {
            get;set;
        }


        public int StartPiece
        {
            get; set;
        }
        public int EndPiece
        {
            get; set;
        }
        public int Patch1Count
        {
            get; set;
        }
        public int Patch2Count
        {
            get; set;
        }
        public int Patch3Count
        {
            get; set;
        }
        public int Patch4Count
        {
            get; set;
        }
        public string AssociatedImageFile
        {
            get; set;
        }
        public int PageColumns
        {
            get; set;
        }
        public int PageRows
        {
            get; set;
        }
        public float ImageHeight
        {
            get; set;
        }
        public float ImageWidth
        {
            get; set;
        }
        public bool isValid
        {
            get; set;
        }
        public Color Patch1Color
        {
            get; set;
        }
        public Color Patch2Color
        {
            get; set;
        }
        public Color Patch3Color
        {
            get; set;
        }
        public Color Patch4Color
        {
            get; set;
        }
        public string invalidReason
        {
            get; set;
        }
        /// <summary>
        /// If the red,green,and blue pixel are all within  PatchDiscoveryThreshold, then 
        /// page pixels are assumed to belong to the color sought during the image processing 
        /// stage. 
        /// </summary>
        public int PatchDiscoveryThreshold
        {
            get; set;
        }
        public List<PuzzleVertex> PossiblePatchBoundaryPoints
        {
            get; set;
        }
        public int AnticipatedPieces {
            get
            {
                return (StartPiece + 1) - EndPiece;
            }
            set { }
        }
        public Color  PatchBoundaryColor
        {
            get; set;
        }
        public int DiscoveredPieces { get; set; }
        public double MinAreaOfPatches { get; set; }
        public double MaxAreaOfPatches { get; set; }
        public double MinSquarenessOfPatches { get; set; }
        public Dictionary<int, List<PuzzleVertex>> ContiguousPatchBoundariesColors
        {
            get;set;
        }
        public List<Patch> Patches
        {
            get;set;
        }




        public string ProcessPage()
        {
            try
            {
                GetPossiblePatchBoundaryPoints();
                if (this.PossiblePatchBoundaryPoints.Count == 0)
                {
                    return "No colors in the image were found to match the expected color of the patch boundary. ";
                }
                ContiguousPatchBoundariesColors = GetContiguousGroupsOfPatchBoundaries();
                //Now attempt to make patches
                ProcessPatches();
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return "UKNOWN ERROR " + ex.ToString();
            }
            
        }

        private void ProcessPatches()
        {
            Patches = new List<Patch>();
            double areaThreshold = (ImageHeight * ImageWidth * ValidPatchPercentageOfPageThreshold) / (PageRows * PageColumns );
            List<double> patchAreas = new List<double>();
            List<double> patchVerticies = new List<double>();
            int maxCluster = ContiguousPatchBoundariesColors.Max(x => x.Value.Count);
            int minCluster = ContiguousPatchBoundariesColors.Min(x => x.Value.Count);
            int averageCluster = (maxCluster + minCluster) / 2;
            double testCut = averageCluster;
            double cutSize = maxCluster - averageCluster;
            while (true)
            {
                cutSize = cutSize / 2;
                var test = ContiguousPatchBoundariesColors.Where(x => x.Value.Count > testCut);
                if(test.Count() < PageRows * PageColumns)
                {
                    testCut -= cutSize;
                }
                if(test.Count() > PageRows * PageColumns)
                {
                    testCut += cutSize;
                }
                if(test.Count() == PageRows * PageColumns)
                {
                    foreach(var v in test)
                    {
                        Patch patch = new Patch(v.Value);
                        Patches.Add(patch);
                    }
                    break;
                }
            }
            
            /*foreach (KeyValuePair<int, List<PuzzleVertex>> kvp in ContiguousPatchBoundariesColors)
            {
                Patch patch = new Patch(kvp.Value);
                patchAreas.Add(patch.Area);
                patchVerticies.Add(patch.Verticies.Count);
                if ( (patch.Verticies.Count > NumberOfPatchVerticesThreshold) && (patch.Area > areaThreshold) && (Math.Abs(patch.SquareNess - 1) < SquarenessOfPatchThreshold))
                {
                    patch.IsValid = true;
                    Patches.Add(patch);
                }
                patch.IsValid = false;

            }*/
        }

        /// <summary>
        /// This is the minimum area that all of the patches must have combined. Any contiguous area of patch color whose area is 
        /// less than (imageHeight * imageWidth) / (PatchPercentageOfPageThreshold * rows * columns) isn't a valid patch with a piece in it
        /// </summary>
        public double ValidPatchPercentageOfPageThreshold
        {
            get { return .3; }
        }
        //When processing the page image, adjacent verticies that make up the patch boundary colors are grouped together. For each patch, there are
        //four sides of a patch, and obviously a patch can fill no more than the width of the image divided by the number of columns.  Thus the max possible
        //would be  4 * (width of the image divided by the number of columns).  The calculation assumes only one pixel per patch boundary.  In practice, the
        //colors are seldom consistently close enough to form these boundaries, so 1/4 of that number is used. To get more pixels to fall into the bin of what
        //is considered a patch boundary, adjust the 6th argument of Properties.txt so that the sum of the RGB deltas is less than that value. 
        public double NumberOfPatchVerticesThreshold
        {
            get
            {
                return this.ImageWidth / PageColumns  / 4.0;
            }
        }
        public double SquarenessOfPatchThreshold
        {
            get { return .1; }
        }

        private Dictionary<int, List<PuzzleVertex>> GetContiguousGroupsOfPatchBoundaries()
        {
            //now, iterate through the points to find contiguous groups that are larger than xxx
            Dictionary<int, List<PuzzleVertex>> contiguousPatchesOfBoundary = new Dictionary<int, List<PuzzleVertex>>();
            var possiblePatchBoundaryDictionary = this.PossiblePatchBoundaryPoints.ToDictionary(vertex => vertex.X + ":" + vertex.Y);
            //hashset contains "X:Y"
            var accountedForVerticies = new HashSet<string>();
            //loop through each PossiblePatchBoundaryPoint (which will form many groups)
            for (int i = 0; i < this.PossiblePatchBoundaryPoints.Count; i++)
            {
                PuzzleVertex startVertex = PossiblePatchBoundaryPoints[i];
                if (accountedForVerticies.Contains(PuzzleVertex.GetIdentifier(startVertex)) == false)
                {
                    var listOfContiguousPuzzleVerticies = new List<PuzzleVertex>();
                    contiguousPatchesOfBoundary.Add(contiguousPatchesOfBoundary.Count, listOfContiguousPuzzleVerticies);

                    Queue<PuzzleVertex> qOfVerticiesToProcess = new Queue<PuzzleVertex>();
                    qOfVerticiesToProcess.Enqueue(startVertex);
                    while (qOfVerticiesToProcess.Count > 0)
                    {
                        PuzzleVertex currentVertex = qOfVerticiesToProcess.Dequeue();
                        accountedForVerticies.Add(PuzzleVertex.GetIdentifier(currentVertex));
                        listOfContiguousPuzzleVerticies.Add(currentVertex);
                        //now enque the 8 neighbors
                        for (int x = -1; x < 2; x++)
                        {
                            for (int y = -1; y < 2; y++)
                            {
                                if (x == 0 && y == 0) { continue; } // Don't process the middle square since it is the current vertex
                                string adjacentNeighborKey = (currentVertex.X + x) + ":" + (currentVertex.Y + y);
                                if (possiblePatchBoundaryDictionary.ContainsKey(adjacentNeighborKey))
                                {
                                    if (accountedForVerticies.Contains(adjacentNeighborKey) == false)
                                    {
                                        accountedForVerticies.Add(adjacentNeighborKey);
                                        qOfVerticiesToProcess.Enqueue(possiblePatchBoundaryDictionary[adjacentNeighborKey]);
                                    }
                                }
                            }
                        }

                    }
                }
            }
            return contiguousPatchesOfBoundary;
        }

        private void GetPossiblePatchBoundaryPointsOLD()
        {
            Bitmap im = new Bitmap(this.Directory + "\\" + this.AssociatedImageFile);
            this.ImageWidth = im.PhysicalDimension.Width;
            this.ImageHeight = im.PhysicalDimension.Height;
            this.PossiblePatchBoundaryPoints = new List<PuzzlePalServer.PuzzleVertex>();
            for (int col = 0; col < this.ImageWidth; col++)
            {
                for (int row = 0; row < this.ImageHeight; row++)
                {
                    if (IsPatchBoundary(im, col, row))
                    {
                        PuzzleVertex pv = new PuzzleVertex(col,row);
                        pv.Red = im.GetPixel(col, row).R;
                        pv.Green = im.GetPixel(col, row).G;
                        pv.Blue = im.GetPixel(col, row).B;
                        this.PossiblePatchBoundaryPoints.Add(pv);
                    }

                }
            }
        }
        private void GetPossiblePatchBoundaryPoints()
        {
            Bitmap im = new Bitmap(this.Directory + "\\" + this.AssociatedImageFile);
            this.ImageWidth = im.PhysicalDimension.Width;
            this.ImageHeight = im.PhysicalDimension.Height;
            this.PossiblePatchBoundaryPoints = new List<PuzzlePalServer.PuzzleVertex>();
            Queue<Point> q = new Queue<Point>();
            q.Enqueue(new Point(0, 0));
            HashSet<string> haveBeenInQ = new HashSet<string>();
            haveBeenInQ.Add("0,0");
            double averageL = 1.00;
            double averageR = im.GetPixel(0, 0).R;
            double averageG = im.GetPixel(0, 0).G;
            double averageB = im.GetPixel(0, 0).B;
            int samples = 1;
            while (q.Count > 0)
            {
                Point currentPoint = q.Dequeue();
                int x = currentPoint.X;
                int y = currentPoint.Y;
                //string key = x.ToString() + "," + y.ToString();
                //visitedPoints.Add(key);
                if (IsContrastingColor(im, x,y, ref averageR, ref averageG, ref averageB, ref averageL, ref samples))
                {
                    PuzzleVertex pv = new PuzzleVertex(x,y);
                    pv.Red = im.GetPixel(x,y).R;
                    pv.Green = im.GetPixel(x, y).G;
                    pv.Blue = im.GetPixel(x, y).B;
                    this.PossiblePatchBoundaryPoints.Add(pv);
                }
                else
                {
                    for(int i = -1; i < 2; i++)
                    {
                        int newX = x + i;
                        for (int j = -1; j < 2; j++)
                        {
                            int newY = y + j;
                            string key = newX + "," + newY;
                            if(newX > -1 && newY > -1 && newX < im.Width && newY < im.Height)
                            {
                                key = newX.ToString() + "," + newY.ToString();
                                if(haveBeenInQ.Contains(key) == false)
                                {
                                    q.Enqueue(new Point(newX, newY));
                                    haveBeenInQ.Add(key);
                                }
                            }
                        }
                    }
                }
            }

        }
        /// <summary>
        /// uses: https://ux.stackexchange.com/questions/82056/how-to-measure-the-contrast-between-any-given-color-and-white to determine how contrasting a 
        /// color is from its background
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="averageR"></param>
        /// <param name="averageG"></param>
        /// <param name="averageB"></param>
        /// <param name="averageL"></param>
        /// <param name="samples"></param>
        /// <returns></returns>
        public bool IsContrastingColor(Bitmap bmp, int column, int row, ref double averageR, ref double averageG, ref double averageB, ref double averageL, ref int samples)
        {

            if (column == 178 && row == 31)
            {

            }
            if (column == 26 && row == 37)
            {

            }
            try
            {
                Color color = bmp.GetPixel(column, row);
                double r = color.R;
                double b = color.B;
                double g = color.G;
                double Rg = 0;double Gg = 0; double Bg = 0;
                if (r <= 10)
                {
                    Rg = r / 3294.0;
                }
                else
                {
                    Rg = Math.Pow(r / 269 + .0513, 2.4);
                }
                if (b <= 10)
                {
                    Bg = b / 3294.0;
                }
                else
                {
                    Bg = Math.Pow(b / 269 + .0513, 2.4);
                }
                if (g <= 10)
                {
                    Gg = g / 3294.0;
                }
                else
                {
                    Gg = Math.Pow(g / 269 + .0513, 2.4);
                }
                double L = 0.2126 * Rg + 0.7152 * Gg + 0.0722 * Bg;
                double contrastRatio = 0;
                if(averageL > L)
                {
                    contrastRatio = (averageL + 0.05) / (L + 0.05);
                }
                else
                {
                    contrastRatio = (L + 0.05) / (averageL + 0.05);
                }
                
                samples = samples+1;
                averageR = (((samples-1) * averageR) + r) / samples;
                averageG = (((samples - 1) * averageG) + g) / samples;
                averageB = (((samples - 1) * averageB) + b) / samples;
                averageL = (((samples - 1) * averageL) + L) / samples;
                if (contrastRatio > 2 && samples > 10)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public bool IsPatchBoundary(Bitmap bmp, int column, int row)
        {
            try
            {
                Color color = bmp.GetPixel(column, row);
                int r = color.R;
                int b = color.B;
                int g = color.G;
                //magenta or black is the background patch color 
                int deltaR = this.PatchBoundaryColor.R - r;
                int deltaG = this.PatchBoundaryColor.G - g;
                int deltaB = this.PatchBoundaryColor.B - b;
                if ((Math.Abs(deltaR) < PatchDiscoveryThreshold) && (Math.Abs(deltaG) < PatchDiscoveryThreshold) && (Math.Abs(deltaB) < PatchDiscoveryThreshold))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }
        
    }
}
