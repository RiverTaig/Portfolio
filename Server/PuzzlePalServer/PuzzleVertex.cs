using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzlePalServer
{

    public class PuzzleVertex
    {
        public PuzzleVertex(int x, int y) {
            this.X = x;
            this.Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public override int GetHashCode()
        {
            return (X.ToString() + ":" + Y.ToString()).GetHashCode();
        }
        public static string GetIdentifier(int x, int y) {
            return x.ToString() + ":" + y.ToString();
        }
        public static string GetIdentifier(PuzzleVertex puzzleVertex)
        {
            return puzzleVertex.X.ToString() + ":" + puzzleVertex.Y.ToString();
        }
    }
}
