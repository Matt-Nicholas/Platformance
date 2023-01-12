using UnityEngine;

public class UncolorAfterTimeBlock : Block
{

    [SerializeField] private float countTime = 0;
    private float counter;

    private void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else if(_owner != null)
        {
            SetColor(StartColor);
            Unclaim();
            _owner = null;
        }
    }

    public override void TriggerEntered(Player player)
    {
        base.TriggerEntered(player);
        SetColor(player.Color);
        counter = countTime;
        Claim(player);
    }
}