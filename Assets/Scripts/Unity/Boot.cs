using Unity.Services;
using UnityEngine;
using GameModel = Game.Game;

namespace Unity
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private InputService _inputService;
        [SerializeField] private Camera _mainCamera;
        
        [Header("Prefabs")]
        [SerializeField] private Board _board;
        [SerializeField] private Player _player;
        
        private GameModel _game;

        private void Start()
        {
            _game = new GameModel(11, null, null);

            var inputService = Instantiate(_inputService);
            inputService.Construct(_mainCamera);
            
            var board = Instantiate(_board, Vector3.zero, Quaternion.identity);
            board.Construct(_game.Board, _inputService);

            var player = Instantiate(_player);
            player.Construct(_game.Board.Pieces, board, inputService);
        }
    }
}
