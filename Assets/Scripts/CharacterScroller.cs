using UnityEngine;

public class CharacterScroller : MonoBehaviour
{
    private Scrollable[] _scrollables;
    private bool _moved;

    void Start()
    {
        _scrollables = Object.FindObjectsOfType<Scrollable>();
    }

    void Update()
    {
        float h = Input.GetAxis("J_MainHorizontal1");

        if ((Mathf.Abs(h) > 0.2f))
        {
            foreach (Scrollable scrollable in _scrollables)
            {
                scrollable.Move(new Vector3(5, 0, 0));
            }
            _moved = true;
        }
        else
        {
            _moved = false;
        }
    }
}
