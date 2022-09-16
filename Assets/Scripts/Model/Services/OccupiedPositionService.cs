using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Model.Pieces;

namespace Model.Services
{
    public class OccupiedPositionService
    {
        public bool DefaultStrategy(Piece actor, int target, List<Piece> pieces) =>
            !pieces
                .Where(piece => !piece.Captured)
                .Any(piece => piece.Position.Value == target && piece.Color == actor.Color);
        
        public bool PawnOccupyTarget(Pawn pawn, int target, List<Piece> pieces) =>
            Math.Abs(pawn.Position.Value - target) == 1
                ? PawnOccupyClosest(pawn, pieces)
                : PawnOccupySecond(pawn, pieces);

        private static bool PawnOccupyClosest(Pawn pawn, List<Piece> pieces)
        {
            var target = pawn.ForwardIsRight ? pawn.Position.Value + 1 : pawn.Position.Value - 1;

            var nextPiece = pieces.MaybeFind(piece => !piece.Captured && piece.Position.Value == target);
            
            return !(nextPiece.Exists && nextPiece.Value.Color == pawn.Color);
        }
        
        private static bool PawnOccupySecond(Pawn pawn, List<Piece> pieces)
        {
            var target = pawn.ForwardIsRight ? pawn.Position.Value + 2 : pawn.Position.Value - 2;
            
            var nextPiece = pieces.MaybeFind(piece => !piece.Captured && piece.Position.Value == target);
            
            return !nextPiece.Exists;
        }
    }
}