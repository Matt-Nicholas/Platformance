using UnityEngine;

public class PhysicsVolume : MonoBehaviour
{
    [SerializeField] private Vector2 _velocity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter Physics Volume");
        if (other.CompareTag("Player"))
        {
            var pc = other.GetComponent<PlayerController>();
            pc.EnterPhysicsVolume(_velocity);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit Physics Volume");
        if (other.CompareTag("Player"))
        {
            var pc = other.GetComponent<PlayerController>();
            pc.ExitPhysicsVolume();
        }
    }
}