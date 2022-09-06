using Unity.Services;
using UnityEngine;
using GameModel = Model.Game;

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

        private GameApp _game;

        private void Start()
        {
            _game = ComposeApp();
            _game.Boot();
        }

        private GameApp ComposeApp()
        {
            var board = Instantiate(_board, Vector3.zero, Quaternion.identity);
            
            var inputService = Instantiate(_inputService);
            inputService.Construct(_mainCamera);

            var humanPlayer = Instantiate(_player);
            humanPlayer.Construct(board, inputService, "White");

            var hud = Instantiate(_hud);
            
            return new GameApp(inputService, board, humanPlayer, hud);
        }
    }
}
