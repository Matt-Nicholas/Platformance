using UnityEngine;

public class Magnifier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Scrollable")
        {
            collision.transform.localScale = new Vector3(3, 3, 3);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Scrollable")
        {
            collision.transform.localScale = new Vector3(2, 2, 2);
        }
    }
}