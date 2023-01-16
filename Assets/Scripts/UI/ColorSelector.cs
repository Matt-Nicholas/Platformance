using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelector : MonoBehaviour
{
    [SerializeField] private PlayerPortrait[] playerPortraits;
    [SerializeField] private SelectorButton[] buttons;
    
    private const int P1FirstSelected = 0;
    private const int P2FirstSelected = 4;
    private const int P3FirstSelected = 5;
    private const int P4FirstSelected = 9;

    private Dictionary<int, int> _playerSelectionByIndex = new ();
    
    private readonly int lockIndex = 2;
    private Game _game;

    private bool joined;
    private bool justMoved;
    private bool ready;

    private void Start()
    {
        _game = Game.Instance;

        for (var i = 0; i < buttons.Length; i++)
        {
            // if (i > lockIndex) break;
            
            buttons[i].SetColor(colors[i]);
            buttons[i].Unselect();
        }
        
        NavRight(0, P1FirstSelected - 1);
        playerPortraits[0].SetStatus(PlayerPortrait.PlayerStatus.ChooseColor);
        
        SubscribePlayerEvents(Game.Instance.Players[0]);
        _game.OnPlayerJoinedGame += HandlePlayerJoined;
        
        _game.SetJoiningEnabled(true);
    }

    private void HandlePlayerJoined(int index)
    {
        switch (index){
            case 1:
                NavRight(index, P2FirstSelected - 1);
                break;
            case 2:
                NavRight(index, P3FirstSelected - 1);
                break;
            case 3:
                NavRight(index, P4FirstSelected - 1);
                break;
        }
        
        SubscribePlayerEvents(Game.Instance.Players[index]);
    }

    private void HandlePlayerUnjoined(Player player)
    {
        UnsubscribePlayerEvents(player);
    }

    public void NavRight(int playerIndex, int currentIndex)
    {
        var target = currentIndex + 1;
        if (target == buttons.Length)
        {
            target = 0;
        }
        
        for (int i = target; i < buttons.Length; i++)
        {
            if (buttons[i].TrySelect(playerIndex))
            {
                if (currentIndex < buttons.Length && currentIndex > -1)
                {
                    buttons[currentIndex].Unselect();    
                }
                
                if (_playerSelectionByIndex.ContainsKey(playerIndex))
                {
                    _playerSelectionByIndex[playerIndex] = i;
                }
                else
                {
                    _playerSelectionByIndex.Add(playerIndex, i);
                }
                
                playerPortraits[playerIndex].SetColor(buttons[i].Color);
                Game.Instance.Players[playerIndex].SetColor(buttons[i].Color);
                return;
            }
        }
        
        // If we made it here, we went through the loop without finding an available space. Try Again from the beginning
        NavRight(playerIndex, 0);
    }
    
    public void NavLeft(int playerIndex, int currentIndex)
    {
        buttons[currentIndex].Unselect();
        var target = currentIndex - 1;
        if (target < 0)
        {
            target = buttons.Length - 1;
        }
        
        for (int i = target; i >= 0; i--)
        {
            if (buttons[i].TrySelect(playerIndex))
            {
                if (_playerSelectionByIndex.ContainsKey(playerIndex))
                {
                    _playerSelectionByIndex[playerIndex] = i;
                }
                else
                {
                    _playerSelectionByIndex.Add(playerIndex, i);
                }
                
                playerPortraits[playerIndex].SetColor(buttons[i].Color);
                Game.Instance.Players[playerIndex].SetColor(buttons[i].Color);
                return;
            }
        }

        // If we made it here, we went through the loop without finding an available space. Try Again from the end
        NavLeft(playerIndex, buttons.Length - 1);
    }

    private void OnDestroy()
    {
        if (Game.Instance == null)
            return;
        
        _game.OnPlayerJoinedGame -= HandlePlayerJoined;
        
        foreach (var player in Game.Instance.Players)
        {
            UnsubscribePlayerEvents(player);
        }
    }

    private void SubscribePlayerEvents(Player player)
    {
        player.InputHandler.OnUINavigate += HandleUINavigate;
        player.InputHandler.OnUISubmit += HandleUISubmit;
        player.InputHandler.OnUICancel += HandleUICancel;
        player.OnUnjoined += HandlePlayerUnjoined;
    }

    private void UnsubscribePlayerEvents(Player player)
    {
        player.InputHandler.OnUINavigate -= HandleUINavigate;
        player.InputHandler.OnUISubmit -= HandleUISubmit;
        player.InputHandler.OnUICancel -= HandleUICancel;
        player.OnUnjoined -= HandlePlayerUnjoined;
    }
    
    private void HandleUINavigate(int playerIndex, Vector2 dir)
    {
        if (playerPortraits[playerIndex].IsReady)
            return;
        
        var currentSelection = _playerSelectionByIndex[playerIndex];
        if (dir.x > 0)
        {
            NavRight(playerIndex, currentSelection);
        }
        else if (dir.x < 0)
        {
            NavLeft(playerIndex, currentSelection);
        }
        
        //TODO vertical movement
    }
    
    private void HandleUISubmit(int playerIndex)
    {
        switch (playerPortraits[playerIndex].Status)
        {
            case PlayerPortrait.PlayerStatus.NotJoined:
                playerPortraits[playerIndex].SetStatus(PlayerPortrait.PlayerStatus.ChooseColor);
                break;
            case PlayerPortrait.PlayerStatus.ChooseColor:
                playerPortraits[playerIndex].SetStatus(PlayerPortrait.PlayerStatus.Ready);
                break;
            case PlayerPortrait.PlayerStatus.Ready:
                if (playerIndex == 0)
                { 
                    bool allPlayersReady = playerPortraits.All(p => p.IsReady || p.Status == PlayerPortrait.PlayerStatus.NotJoined);

                    if (allPlayersReady)
                    {
                        Game.Instance.StartGame();
                    }
                }
                break;
        }
    }

    private void HandleUICancel(int playerIndex)
    {
        throw new System.NotImplementedException();
    }
    
    private void Update()
    {
        
    }

    private int AdjustIndex(int tempIndex, bool movingUp)
    {
        var i = tempIndex;
        if (movingUp)
        {
            if (i + 1 < buttons.Length && i + 1 <= lockIndex)
                i++;
            else
                i--;
        }
        else
        {
            if (i - 1 >= 0)
                i--;
            else
                i++;
        }

        return i;
    }

    private readonly Color[] colors =
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.magenta,
        Color.cyan,
        Color.white,
        Color.black,
        Color.green,
        Color.green,
        Color.green
    };
}