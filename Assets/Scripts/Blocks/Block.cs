using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    public Action<Player> OnBlockClaimed; 
    public Action<Player> OnBlockUnClaimed;
    
    public Color Color { get; protected set; }
    
    protected Renderer Renderer;
    protected Color CurrentColor;
    protected Color StartColor;

    private bool _canBeChanged = true;
    protected Player _owner = null;
    
    public virtual void Init()
    {
        if(Game.Instance == null)
            return;
        
        StartColor = Game.Instance.GameSettings.ColorBlockStartColor;
        Renderer = GetComponent<Renderer>();
        SetColor(StartColor);
    }

    public virtual void TriggerEntered(Player player)
    {
        
    }

    public virtual void TriggerExited(Player player)
    {
        
    }
    
    protected void Claim(Player player)
    {
        _owner = player;
        OnBlockClaimed?.Invoke(player);
    }

    protected void Unclaim()
    {
        OnBlockUnClaimed?.Invoke(_owner);
        _owner = null;
    }

    public virtual void SetColor(Color col)
    {
        CurrentColor = col;
        Renderer.material.color = CurrentColor;
    }

    private IEnumerator ActivateBlock()
    {
        float delayTime = Random.Range(0.25f, 3.0f);

        yield return new WaitForSeconds(delayTime);
        SetColor(StartColor);

    }
}