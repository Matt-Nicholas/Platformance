
public class ColorBlock : Block
{
    public override void TriggerEntered(Player player)
    {
        if (_owner == player) 
            return;
        
        Claim(player);
        SetColor(player.Color);
    }
}
