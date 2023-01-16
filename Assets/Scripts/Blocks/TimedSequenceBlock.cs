using System;
using UnityEngine;

public class TimedSequenceBlock : Block
{

    [SerializeField] private float countdownTime;
    private int _orderNumber;
    private TimedSequenceBlock[] _sequenceBlocks;
    private float _timer;
    private bool _isTriggered;

    public override void Init()
    {
        base.Init();
        _sequenceBlocks = FindObjectsOfType(typeof(TimedSequenceBlock)) as TimedSequenceBlock[];
        string text = GetComponentInChildren<TextMesh>().text;
        _orderNumber = Convert.ToInt32(text.Trim());
    }

    private void Update()
    {
        if (_timer <= 0)
        {
            if (_isTriggered)
            {
                Unclaim();
                SetColor(Game.Instance.GameSettings.ColorBlockStartColor);
                _isTriggered = false;
            }
        }
        else
        {
            if (!NextBlockIsColored())
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                _timer = countdownTime;
            }
        }
    }

    public override void TriggerEntered(Player player)
    {
        if (PreviousBlockIsColored())
        {
            _isTriggered = true;
            Claim(player);
            SetColor(player.Color);
            _timer = countdownTime;
        }
    }

    private bool PreviousBlockIsColored()
    {
        if (_orderNumber == 1) return true;
        for (int i = 0; i < _sequenceBlocks.Length; i++)
        {
            if (_sequenceBlocks[i]._orderNumber == (_orderNumber - 1))
            {
                if (_sequenceBlocks[i].Renderer.material.color != Game.Instance.GameSettings.ColorBlockStartColor)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool NextBlockIsColored()
    {
        // If this is the last block in the sequence return false
        if (_orderNumber == _sequenceBlocks.Length) return false;
        for (int i = 0; i < _sequenceBlocks.Length; i++)
        {
            if (_sequenceBlocks[i]._orderNumber == (this._orderNumber + 1))
            {
                if (_sequenceBlocks[i].Renderer.material.color != Game.Instance.GameSettings.ColorBlockStartColor)
                {
                    return true;
                }
            }
        }

        return false;
    }
}