using Common;
using Unity.Services;
using UnityEngine;

namespace Unity
{
    public class Cell : MonoBehaviour
    {
        
        [SerializeField] private SpriteRenderer _cellRenderer;
        [SerializeField] private SpriteRenderer _higlightRenderer;
        [SerializeField] private float SelectionHeight = 0.1f;

        [Header("Resources")]
        [SerializeField] private Sprite _evenBackground;
        [SerializeField] private Sprite _oddBackground;
        [SerializeField] private Sprite _moveHighlight;
        [SerializeField] private Sprite _captureHighlight;

        public int Position { get; private set; }
        public float Width => _cellRenderer.bounds.size.x;
        public float Height => _cellRenderer.bounds.size.y;


        public void Construct(int position)
        {
            Position = position;
            SetBackground(position);
        }

        public bool IsContains(Coordinate pos)
        {
            return _cellRenderer.bounds.IsOverlapInZ(pos.World);
        }

        private void SetBackground(int position)
        {
            _cellRenderer.sprite = position.Odd() ? _oddBackground : _evenBackground;
        }

        public void Highlight(bool isActive)
        {
            transform.localPosition = transform.localPosition.SetY(0);
            _higlightRenderer.sprite = null;
            if (isActive)
            {
                transform.localPosition = transform.localPosition.AddY(SelectionHeight);
                _higlightRenderer.sprite = _moveHighlight;
            }
        }
    }
}
