using UnityEngine;

public class MatchBlock : Block
{
    public Color colorToMatch;
    public bool matched;

    private Color _startColor;
    
    public override void Init()
    {
        base.Init();
        _startColor = new Color(colorToMatch.r, colorToMatch.g, colorToMatch.b, colorToMatch.a * 0.5f);
        SetColor(_startColor);
    }

    public override void TriggerEntered(Player player)
    {
        if (player.Color == colorToMatch)
        {
            Claim(player);
            matched = true;
            SetColor(player.Color);
        }
    }
}