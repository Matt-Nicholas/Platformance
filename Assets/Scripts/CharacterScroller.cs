using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScroller : MonoBehaviour
{

    Scrollable[] scrollables;
    bool moved = false;

    // Use this for initialization
    void Start()
    {
        scrollables = Object.FindObjectsOfType<Scrollable>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("J_MainHorizontal1");

        if ((Mathf.Abs(h) > 0.2f))
        {
            foreach (Scrollable scrollable in scrollables)
            {
                scrollable.Move(new Vector3(5, 0, 0));
            }
            moved = true;
        }
        else
        {
            moved = false;
        }
    }
}
