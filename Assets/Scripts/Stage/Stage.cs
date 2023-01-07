using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private StartPositions _startPositions;
    [SerializeField] private ScoreBoard _scoreBoard;
    
    void Start()
    {
        for (int i = 0; i < Game.Instance.Players.Count; i++)
        {
            var player = Game.Instance.Players[i];
            player.SetPosition(_startPositions.GetPosition(i));
            player.SetVisible(true);
            player.SetControlsActive(true);
            player.SetActionMap("Player");
        }

        StartGame();
    }

    public void StartGame()
    {
        _scoreBoard.StartTimer();
    }
}
