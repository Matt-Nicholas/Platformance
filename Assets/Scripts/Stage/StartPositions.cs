using UnityEngine;

public class StartPositions : MonoBehaviour
{
    [SerializeField] private Transform[] _markers;

    public Vector2 GetPosition(int index)
    {
        return _markers[index].position;
    }
}
