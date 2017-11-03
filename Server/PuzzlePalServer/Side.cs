using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzlePalServer
{
    public class Side
    {
        public Piece Piece
        {
            get; set;
        }
        public bool isEdge { get; set; }
        public byte Index { get; set; }
        public List<PuzzleVertex> CoordinateString
        {
            get; set;
        }
        //get or set a translated/rotated coordinate string where the first vertex is at 0,0 and the last vertex is at (x,0)
        public List<PuzzleVertex> TransformedCoordinateString
        {
            get; set;
        }
    }
}
