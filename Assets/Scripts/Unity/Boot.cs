using System;
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
        [SerializeField] private Hud _hud;
        
        
        private GameModel _game;
        private Board _boardCreated;

        private void Start()
        {
             Compose();

            _game.OnMoveFinished += UpdateHandler;
            _game.Run();
        }

        private void UpdateHandler()
        {
            _boardCreated.UpdateState();
            _hud.UpdateState();
        }

        private void Compose()
        {
            var inputService = Instantiate(_inputService);
            inputService.Construct(_mainCamera);

            var boardModel = new Game.Board(11);
            
            var board = Instantiate(_board, Vector3.zero, Quaternion.identity);
            board.Construct(boardModel, _inputService);
            _boardCreated = board;

            var player1 = Instantiate(_player);
            player1.Construct(boardModel.Whites, boardModel.Blacks, boardModel, board, inputService, "White");
            
            var player2 = Instantiate(_player);
            player2.Construct(boardModel.Blacks, boardModel.Whites, boardModel, board, inputService, "Black");

            _game = new GameModel(boardModel, player1, player2);

            _hud = Instantiate(_hud);
            _hud.Construct(_game);
        }
    }
}
