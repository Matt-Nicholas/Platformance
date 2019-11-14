using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAssign : MonoBehaviour
{

    private bool playerSet = false;

    void Start()
    {

    }

    void Update()
    {

        int i = 1;

        while (i < 3)
        {
            if (playerSet) break;

            if (Input.GetButtonDown("A_Button" + i))
            {

                TheGameManager.Instance.CreatePlayer(i, true);
                playerSet = true;

                break;
            }
            i++;
        }
    }
}
