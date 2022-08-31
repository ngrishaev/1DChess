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
            var player1 = Instantiate(_player);
            var player2 = Instantiate(_player);
            _game = new GameModel(11, player1, player2);

            var inputService = Instantiate(_inputService);
            inputService.Construct(_mainCamera);
            
            var board = Instantiate(_board, Vector3.zero, Quaternion.identity);
            board.Construct(_game.Board, _inputService);
            
            player1.Construct(_game.Board.Whites, board, _game.Board, inputService, "p1");
            player2.Construct(_game.Board.Blacks, board, _game.Board, inputService, "p2");

            _game.OnMoveFinished += board.UpdateState;
            _game.Run();
        }
    }
}
