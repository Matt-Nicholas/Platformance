
public class SequenceBlock : Block
{
    SequenceBlock[] sequenceBlocks;

    public int OrderNumber;

    public override void Init()
    {
        base.Init();
        //SetColor(GamePlayState.startColor);
        sequenceBlocks = FindObjectsOfType(typeof(SequenceBlock)) as SequenceBlock[];
    }

    public override void TriggerEntered(Player player)
    {
        if (PrevousBlockIsColored())
        {
            Unclaim();
            Claim(player);
            SetColor(player.Color);
        }
    }

    private bool PrevousBlockIsColored()
    {
        if (OrderNumber == 1) return true;

        for (int i = 0; i < sequenceBlocks.Length; i++)
        {
            if (sequenceBlocks[i].OrderNumber == (OrderNumber - 1))
            {
                if (sequenceBlocks[i].Color != StartColor)
                {
                    return true;
                }
            }
        }

        return false;
    }
}