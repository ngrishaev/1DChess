using Game;
using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerIndicator;
    
    private Game.Game _game;

    public void Construct(Game.Game gameModel)
    {
        _game = gameModel;
    }

    public void UpdateState() => _playerIndicator.text = "Current player: " + _game.CurrentPlayer.Name;
}
