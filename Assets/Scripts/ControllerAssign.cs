using UnityEngine;

public class ControllerAssign : MonoBehaviour
{
    private void Awake()
    {
        Game.Instance.OnPlayerJoinedGame += HandlePlayerJoined;
    }

    private void OnDestroy()
    {
        Game.Instance.OnPlayerJoinedGame -= HandlePlayerJoined;
    }

    private void Start()
    {
        Game.Instance.SetJoiningEnabled(true);
    }

    private void HandlePlayerJoined(int playerIndex)
    {
        Game.Instance.SetJoiningEnabled(false);
        Game.Instance.LoadMainMenu();
    }
}