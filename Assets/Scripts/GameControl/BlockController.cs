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
        if (other.tag.Equals("Block"))
            other.gameObject.SendMessage("Entered", player.Color, SendMessageOptions.DontRequireReceiver);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Block"))
            other.gameObject.SendMessage("Exited", player.Color, SendMessageOptions.DontRequireReceiver);
    }
}