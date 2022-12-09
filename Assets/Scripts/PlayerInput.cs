﻿using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    public int playerNumber;
    public int playerID;

    private PlayerController _player;

    void Start()
    {
        _player = GetComponent<PlayerController>();
        if (transform.position.x < 15)
        {
            playerNumber = 1;
            playerID = TheGameManager.Instance.Players[1].controllerID;
        }
        else
        {
            playerNumber = 2;
            playerID = TheGameManager.Instance.Players[2].controllerID;
        }

        transform.GetComponent<Renderer>().material.color = TheGameManager.Instance.Players[playerNumber].Color;
    }

    void Update()
    {
        _player.SetDirectionalInput(new Vector2(InputManager.MainHorizontal(playerID), InputManager.MainVertical(playerID)),
            (InputManager.RightBumper(playerID) || InputManager.XButton(playerID)));

        if (InputManager.AButtonDown(playerID))
        {
            _player.OnJumpInputDown();
        }

        if (InputManager.AButtonUp(playerID))
        {
            _player.OnJumpInputUp();
        }
    }
}