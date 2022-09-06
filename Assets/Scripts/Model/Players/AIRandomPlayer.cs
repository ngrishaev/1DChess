using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Model.Actions;
using Model.Pieces;
using UnityEngine;

namespace Model
{
    public class AIPlayer : IPlayer
    {
        private readonly Board _board;
        private readonly List<Piece> _pieces;
        public string Name { get; }

        public AIPlayer(Board board, List<Piece> pieces, string name)
        {
            _board = board;
            _pieces = pieces;
            Name = name;
        }
        
        public Task<GameAction> GetInput()
        {
            var allMoves = GetAllMoves();
            var chosenAction = allMoves[Random.Range(0, allMoves.Count)];
            return Task.FromResult(chosenAction);
        }
        public bool KingCaptured() => _pieces.Any(piece => piece is King && piece.Captured);

        private List<GameAction> GetAllMoves() => _pieces
            .Where(piece => !piece.Captured)
            .SelectMany(GetMoves)
            .ToList();

        private List<GameAction> GetMoves(Piece piece) =>
            Enumerable.Range(0, _board.Size)
                .Where(piece.CanMoveTo)
                .Aggregate(new List<GameAction>(), (aggregate, position) =>
                    {
                        Maybe<Piece> pieceAtPosition = GetPieceAtPosition(position);
                        GameAction move = pieceAtPosition.Exists
                            ? new Capture(piece, pieceAtPosition.Value)
                            : new Move(piece, position);
                        aggregate.Add(move);
                        return aggregate;
                    }
                )
                .ToList();

        private Maybe<Piece> GetPieceAtPosition(int position)
        {
            foreach (var piece in _board.Pieces)
            {
                if(piece.Position.ValueEquals(position))
                    return Maybe<Piece>.Yes(piece);
            }
            return Maybe<Piece>.No();
        }
    }
}