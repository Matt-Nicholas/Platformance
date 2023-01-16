using System.Collections.Generic;
using System.Linq;

public class MultiTouchBlock : Block
{
    private List<Block> _childBlocks = new List<Block>();
    private int _nextIndex;
    
    public override void Init()
    {
        base.Init();
        _childBlocks = gameObject.GetComponentsInChildren<Block>().ToList();
    }

    public override void TriggerEntered(Player player)
    {
        if (_owner != player)
        {
            foreach (var block in _childBlocks)
            {
                block.Unclaim();
                _childBlocks[_nextIndex].SetColor(StartColor);
            }
            _nextIndex = 0;
        }

        if (_childBlocks.Count > _nextIndex)
        {
            _childBlocks[_nextIndex].Claim(player);
            _childBlocks[_nextIndex].SetColor(player.Color);
            _nextIndex++;    
        }
    }
}