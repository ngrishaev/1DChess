using Model;
using Model.Players;
using Unity.Services;

namespace Unity
{
    public class GameApp : IGameObserver
    {
        private const int BoardSize = 16;
        
        private readonly InputService _inputService;
        private readonly Board _board;
        private readonly Player _humanPlayer;
        private readonly Hud _hud;
        private Game _gameModel;
        private bool _gameInProcess;

        public GameApp(InputService inputService, Board board, Player humanPlayer, Hud hud)
        {
            _inputService = inputService;
            _board = board;
            _humanPlayer = humanPlayer;
            _hud = hud;
            _gameInProcess = false;

            _humanPlayer.OnPieceSelect += _board.HighlightMovesFor;
            _humanPlayer.OnPieceDeselect += _ => _board.ResetHighlight();
            _inputService.OnRestart += RestartHandler;
        }

        public void Boot() => StartGame();

        public void OnMoveEnd()
        {
            _board.UpdateState();
            _hud.UpdateState();
        }

        public void OnGameFinished(IPlayer winner)
        {
            _hud.GameEnd(winner);
            
            _gameInProcess = false;
        }

        private void StartGame()
        {
            _gameInProcess = true;
            
            var boardModel = new Model.Board(BoardSize);
            _board.SetBoard(boardModel);

            _humanPlayer.JoinGame(boardModel.Whites);

            _gameModel =
                new Game(boardModel, _humanPlayer, new AIPlayer(boardModel, boardModel.Blacks, "Black"), this);
            
            _hud.JoinGame(_gameModel);
            
            _gameModel.Run();
        }

        private void RestartHandler()
        {
            if(_gameInProcess)
                return;
            
            StartGame();
        }
    }
}