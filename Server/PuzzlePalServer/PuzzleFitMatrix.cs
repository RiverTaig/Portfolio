using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzlePalServer
{
    public class PuzzleFitMatix
    {
        public int PieceToFit
        {
            get; set;
        }
        public byte SideToFit
        {
            get; set;
        }
        public List<Score> SideFitAndColorScores
        {
            get; set;
        }
        public int ColorWeight { get; set; }
        public int FitWeight { get; set; }
    }
}
