using Game;
using Unity.Services;

namespace Unity
{
    public class GameApp
    {
        private const int BoardSize = 11;
        
        private readonly InputService _inputService;
        private readonly Board _board;
        private readonly Player _humanPlayer;
        private readonly Hud _hud;
        private Game.Game _gameModel;
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

        public void StartGame()
        {
            _gameInProcess = true;
            
            var boardModel = new Game.Board(BoardSize);
            _board.SetBoard(boardModel);

            _humanPlayer.JoinGame(boardModel.Whites, boardModel.Blacks, boardModel);

            _gameModel = new Game.Game(boardModel, _humanPlayer, new AIPlayer(boardModel, boardModel.Blacks, "Black"));

            _gameModel.OnMoveFinished += UpdateHandler;
            _gameModel.OnGameFinished += FinishHandler;
            
            _hud.JoinGame(_gameModel);
            
            _gameModel.Run();
        }

        private void FinishHandler(IPlayer winner)
        {
            _gameModel.OnMoveFinished -= UpdateHandler;
            _gameModel.OnGameFinished -= FinishHandler;
            
            _hud.GameEnd(winner);
            
            _gameInProcess = false;
        }

        private void UpdateHandler()
        {
            _board.UpdateState();
            _hud.UpdateState();
        }

        private void RestartHandler()
        {
            if(_gameInProcess)
                return;
            
            StartGame();
        }
    }
}