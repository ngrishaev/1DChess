using Game;
using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerIndicator;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private TMP_Text _winner;
    
    private Game.Game _game;

    public void JoinGame(Game.Game gameModel)
    {
        _game = gameModel;
        _endScreen.SetActive(false);
    }

    public void UpdateState() => _playerIndicator.text = "Current player: " + _game.CurrentPlayer.Name;
    public void GameEnd(IPlayer winner)
    {
        _endScreen.SetActive(true);
        _winner.text = "Winner: " + winner.Name;
    }
}
