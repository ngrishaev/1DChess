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
            var board = Compose();

            _game.OnMoveFinished += board.UpdateState;
            _game.Run();
        }

        private Board Compose()
        {
            var inputService = Instantiate(_inputService);
            inputService.Construct(_mainCamera);

            var boardModel = new Game.Board(11);
            
            var board = Instantiate(_board, Vector3.zero, Quaternion.identity);
            board.Construct(boardModel, _inputService);
            
            var player1 = Instantiate(_player);
            player1.Construct(boardModel.Whites, boardModel.Blacks, boardModel, board, inputService, "p1");
            
            var player2 = Instantiate(_player);
            player2.Construct(boardModel.Blacks, boardModel.Whites, boardModel, board, inputService, "p2");

            var game = new GameModel(boardModel, player1, player2);
            
            game.Run();

            return board;
        }
    }
}
