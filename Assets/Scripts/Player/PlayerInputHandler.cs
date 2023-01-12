using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputHandler : MonoBehaviour
{
    public Action<int, Vector2> OnUINavigate;
    public Action<int> OnUISubmit;
    public Action<int> OnUICancel;
    
    [HideInInspector] public int playerIndex;
    public InputDevice device;

    private PlayerController _playerController;
    private Vector2 _directionalInput;
    private bool _isBoosting;
    private const float UiNavDelay = .08f;
    private float _uiNavDelayTimer;
    
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        _playerController.SetDirectionalInput(_directionalInput, _isBoosting);

        if (_uiNavDelayTimer > 0)
        {
            _uiNavDelayTimer -= Time.deltaTime;
        }
    }

    public void OnMove(InputValue value)
    {
        Debug.Log($"OnMove - player: {playerIndex}");
        if(!_playerController.PlayerCanMove)
            return;
        _directionalInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        print($"jump pressed {value.isPressed}");
        if (value.isPressed)
        {
            _playerController.OnJumpInputDown();
        }
        else
        {
            _playerController.OnJumpInputUp();
        }
    }
    
    public void OnBoost(InputValue value)
    {
        print($"isBoosting {value.isPressed}");
        _isBoosting = value.isPressed;
    }

    public void OnNavigate(InputValue value)
    {
        if (_uiNavDelayTimer > 0)
            return;
        _uiNavDelayTimer = UiNavDelay;
        
        var dir = value.Get<Vector2>();
        OnUINavigate?.Invoke(playerIndex, dir);
        
    }
    
    public void OnSubmit(InputValue value)
    {
        if(value.isPressed)
            OnUISubmit?.Invoke(playerIndex);
    }
    
    public void OnCancel(InputValue value)
    {
        if(value.isPressed)
            OnUICancel?.Invoke(playerIndex);
    }
}