using System.Collections.Generic;
using System.Linq;

public class MultiTouchBlock : Block
{

    List<Block> childBlocks = new List<Block>();

    public override void Init()
    {
        base.Init();
        childBlocks = gameObject.GetComponentsInChildren<Block>().ToList();
    }

    public override void TriggerEntered(Player player)
    {
        for (int i = 0; i < childBlocks.Count; i++)
        {
            if (childBlocks[i].Color != StartColor && childBlocks[i].Color != player.Color)
            {
                childBlocks[i].SetColor(StartColor);
                childBlocks[i].SetColor(player.Color);
                return;
            }
            
            if (childBlocks[i].Color == StartColor)
            {
                childBlocks[i].SetColor(player.Color);
                return;
            }
        }
    }
}