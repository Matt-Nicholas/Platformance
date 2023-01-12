
public class UncolorBlock : Block
{
    
    public override void TriggerEntered(Player player)
    {
        if (CurrentColor != player.Color)
        {
            SetColor(player.Color);
            Claim(player);
        }
        else if (CurrentColor == player.Color)
        {
            Unclaim();
            SetColor(Game.Instance.GameSettings.ColorBlockStartColor);
        }
    }
}