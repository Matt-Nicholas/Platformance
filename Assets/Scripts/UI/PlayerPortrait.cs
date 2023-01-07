using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPortrait : MonoBehaviour
{
    public bool IsReady { get; private set; }

    public PlayerStatus Status { get; private set; }
    [SerializeField] private TextMeshProUGUI _statusTMP;
    [SerializeField] private Image _fill;
    
    private readonly string _notJoinedText = "Press Start";
    private readonly string _chooseColorText = "Choose Color";
    private readonly string _readyText = "Ready!";

    private void Awake()
    {
        SetStatus(PlayerStatus.NotJoined);
    }

    public void SetStatus(PlayerStatus status)
    {
        switch (status)
        {
            case PlayerStatus.NotJoined:
                _statusTMP.text = _notJoinedText;
                IsReady = false;
                break;
            case PlayerStatus.ChooseColor:
                _statusTMP.text = _chooseColorText;
                IsReady = false;
                break;
            case PlayerStatus.Ready:
                _statusTMP.text = _readyText;
                IsReady = true;
                break;
        }
        Status = status;
    }


    public void SetColor(Color color)
    {
        _fill.color = color;
    }

    public enum PlayerStatus
    {
        NotJoined,
        ChooseColor,
        Ready
    }
}
