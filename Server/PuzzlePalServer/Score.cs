using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzlePalServer
{
    public class Score
    {
        public int ColorWeight { get; set; }
        public int FitWeight { get; set; }
        public int Piece2 { get; set; }
        public byte Side2 { get; set; }

        public double CalculateScore()
        {
            return TotalScore;
        }
        public double FitScore { get; set; }
        public double ColorScore { get; set; }
        public double TotalScore { get; set; }
    }
}
