using System;
using Game.Pieces;
using UnityEngine;

namespace Unity
{
    public class Piece : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Header("Resources")]
        [SerializeField] private Sprite _king;
        [SerializeField] private Sprite _knight;
        [SerializeField] private Sprite _rook;

        public Game.Pieces.Piece PieceData { get; private set; }

        public void Construct(Game.Pieces.Piece piece)
        {
            PieceData = piece;
            _spriteRenderer.sprite = piece switch
            {
                Knight => _knight,
                King => _king,
                Rook => _rook,
                _ => throw new ArgumentOutOfRangeException($"Cant find sprite for {piece.GetType()} piece")
            };
        }
    }
}
