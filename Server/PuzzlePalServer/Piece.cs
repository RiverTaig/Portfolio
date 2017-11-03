using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzlePalServer
{
    public class Piece
    {
        public int PieceNumber
        {
            get; set;
        }
        public List<Side> Sides { get; set; }
        public bool isCorner { get; set; }

        public bool isPlaced { get; set; }
        public Piece LeftPiece { get; set; }
        public Piece RightPiece { get; set; }
        public Piece TopPiece { get; set; }
        public Piece BottomPiece { get; set; }
        public int PlacedLocationIndex { get; set; }
        public int PlacedLocationRow { get; set; }
        public int PlacedLocationColumn { get; set; }
    }
}
