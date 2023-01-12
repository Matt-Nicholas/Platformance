using UnityEngine;

public class BlockController : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals("Block")) 
            return;

        var block = other.GetComponent<Block>();
        block.TriggerEntered(player);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.tag.Equals("Block")) 
            return;

        var block = other.GetComponent<Block>();
        block.TriggerExited(player);
    }
}