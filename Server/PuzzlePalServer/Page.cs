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
            foreach (KeyValuePair<int, List<PuzzleVertex>> kvp in ContiguousPatchBoundariesColors)
            {
                Patch patch = new Patch(kvp.Value);
                if ( (patch.Verticies.Count > NumberOfPatchVerticesThreshold) && (patch.Area > areaThreshold) && (Math.Abs(patch.SquareNess - 1) < SquarenessOfPatchThreshold))
                {
                    patch.IsValid = true;
                    Patches.Add(patch);
                }
                patch.IsValid = false;

            }
        }

        /// <summary>
        /// This is the minimum area that all of the patches must have combined. Any contiguous area of patch color whose area is 
        /// less than (imageHeight * imageWidth) / (PatchPercentageOfPageThreshold * rows * columns) isn't a valid patch with a piece in it
        /// </summary>
        public double ValidPatchPercentageOfPageThreshold
        {
            get { return .3; }
        }
        public double NumberOfPatchVerticesThreshold
        {
            get
            {
                return this.ImageWidth / PageColumns;
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
                }//
            }
            return contiguousPatchesOfBoundary;
        }

        private void GetPossiblePatchBoundaryPoints()
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

        public bool IsPatchBoundary(Bitmap bmp, int column, int row)
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
            else {
                return false;
            }
        }
        
    }
}
