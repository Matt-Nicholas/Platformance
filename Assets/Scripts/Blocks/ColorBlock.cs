
public class ColorBlock : Block
{
    public override void TriggerEntered(Player player)
    {
        if (_owner == player)
        {
            return;
        }
        
        if (_owner != null)
        {
            Unclaim();    
        }

        Claim(player);
        SetColor(player.Color);
    }
}
