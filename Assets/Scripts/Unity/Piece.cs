using System;
using Game.Pieces;
using UnityEngine;
using Color = Game.Pieces.Color;

namespace Unity
{
    public class Piece : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private UnityEngine.Color _whiteColor;
        [SerializeField] private UnityEngine.Color _blackColor;

        [Header("Resources")]
        [SerializeField] private Sprite _king;

        [SerializeField] private Sprite _knight;
        [SerializeField] private Sprite _rook;

        public Game.Pieces.Piece PieceData { get; private set; }

        private readonly Vector3 _capturedPosition = Vector3.one * 1000f;

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

            _spriteRenderer.color = piece.Color == Color.White ? _whiteColor : _blackColor;
        }

        public void Capture()
        {
            transform.localPosition = _capturedPosition;
        }

        public void PlaceAt(float xPos) => transform.localPosition = new Vector3(xPos, 0, 0);
        public bool IsAt(int position) => PieceData.Position.ValueEquals(position);
    }
}
