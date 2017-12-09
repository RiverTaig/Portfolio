using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;
namespace PuzzlePalServer
{
    //{A1F6CDDF-8BD2-4985-B765-DF218DCF7AC5}
    public class Puzzle
    {
        public Guid PuzzleID
        {
            get;set;
        }
        //Under Puzzle/PuzzleID
        public string Directory 
        {
            get;set;
        }
        public int TotalPieces
        {
            get;set;
        }
        //Do we have all the pieces imaged or just some?
        public bool FullPuzzle
        {
            get;set;
        }
        public int PuzzleVerticiesPerPuzzleSide
        {
            get;set;
        }
        public string Comments
        {
            get;set;
        }
        //Width and Height in cm
        public double PuzzleWidth
        {
            get;set;
        }
        //Width and Height in cm
        public double PuzzleHeight
        {
            get; set;
        }
        public Color DefaultPatchBoundaryColor
        {
            get; set;
        }
        public int PiecePixelsWidth { get; set; }
        public int PiecePixelsHeight { get; set; }
        public int Low
        {
            get;set;
        }
        public int High
        {
            get; set;
        }
        public int PuzzleColumns
        {
            get; set;
        }
        public int PuzzleRows
        {
            get; set;
        }
        public List<Page> Pages
        {
            get;set;
        }
        public DateTime CreateDate
        {
            get;set;
        }
        public DateTime ExpirationDate
        {
            get; set;
        }
        public string Owner
        {
            get;set;
        }
        public bool PaidInFull
        {
            get;set;
        }
        public int DefaultPatchDiscoveryThreshold
        {
            get;set;
        }
        //True if puzzle needs to be processed (say after a new image upload)
        public bool IsDirty
        {
            get;set;
        }
        public string ProcessPuzzle(string specificPage = null)
        {
            try
            {
                string puzzleGuid = this.PuzzleID.ToString();
                string puzzleDir = Environment.CurrentDirectory + "\\Puzzles\\" + puzzleGuid;
                DirectoryInfo di = new DirectoryInfo(puzzleDir);
                if (di.Exists == false) {
                    return "The folder containing images of the puzzle pieces was not found on the server.";
                }
                var jpgFiles = di.EnumerateFiles("*.JPG", SearchOption.TopDirectoryOnly);
                SortedDictionary<int, string> pageFileNames = new SortedDictionary<int, string>();
                foreach(var jpgFile in jpgFiles)
                {
                    var fileName = jpgFile.ToString();
                    if (fileName.Contains("-"))
                    {
                        var startEnd = fileName.Split('-');
                        var start = startEnd[0];
                        int startPiece = 0;
                        if (int.TryParse(start, out startPiece))
                        {
                            pageFileNames.Add(startPiece, fileName);
                        }
                    }
                    else
                    {
                        return "An improperly named .jpg file was found in the folder.  The name should include 1 dash.";
                    }
                }
                this.Pages = new List<Page>();
                var properties = File.ReadLines(puzzleDir + "\\properties.txt");
                var dict = properties.ToDictionary(x => x.Split(',')[0]);
                foreach(KeyValuePair<int,string> kvp in pageFileNames)
                {
                    string fileName = kvp.Value;
                    if(specificPage!=null && fileName != specificPage) { continue; }
                    var pageProperties = dict[fileName].Split(',').ToList();
                    Page puzzlePage = new Page();
                    this.Pages.Add(puzzlePage);
                    puzzlePage.Puzzle = this;
                    SetPageProperties(pageProperties, puzzlePage);
                    puzzlePage.Directory = puzzleDir;
                    puzzlePage.ProcessPage();
                }

                return "SUCCESS";
            }
            catch(Exception ex)
            {
                return "UKNOWN ERROR " + ex.ToString();
            }
            return null;
        }

        /// <summary>
        /// Page properties are stored in Properties.txt.  For each page, there is an entry like so: 
        /// 1-15.jpg,1,15,5,3,125,11,0,1,3,#FFFFFF,#FFFFFF,#000000,#0000FF,#FF00FF
        //  (0)FileName,(1)StartPiece,(2)EndPiece,(3)PageRows,(4)PageColumns,(5)PatchDiscoverThreshold,(6-9)Patch1-4Count,(10-13)Patch1-4Colors
        /// </summary>
        /// <param name="pageProperties"></param>
        /// <param name="puzzlePage"></param>
        private static void SetPageProperties(List<string> pageProperties, Page puzzlePage)
        {
            puzzlePage.AssociatedImageFile = pageProperties[0];
            puzzlePage.StartPiece = pageProperties[1].ToInt();
            puzzlePage.EndPiece = pageProperties[2].ToInt();
            puzzlePage.PageRows = pageProperties[3].ToInt();
            puzzlePage.PageColumns = pageProperties[4].ToInt();
            puzzlePage.PatchDiscoveryThreshold = pageProperties[5].ToInt();
            puzzlePage.Patch1Count = pageProperties[6].ToInt();
            puzzlePage.Patch2Count = pageProperties[7].ToInt();
            puzzlePage.Patch3Count = pageProperties[8].ToInt();
            puzzlePage.Patch4Count = pageProperties[9].ToInt();
            puzzlePage.Patch1Color = pageProperties[10].ToColor();
            puzzlePage.Patch2Color = pageProperties[11].ToColor();
            puzzlePage.Patch3Color = pageProperties[12].ToColor();
            puzzlePage.Patch4Color = pageProperties[13].ToColor();
            puzzlePage.PatchBoundaryColor = pageProperties[14].ToColor();
        }

        public List<PuzzleFitMatix> PuzzleFitMatix
        {
            get; set;
        }
    }


   
    

    
}
