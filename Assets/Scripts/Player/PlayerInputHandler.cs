using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputHandler : MonoBehaviour
{
    public Action<int, Vector2> OnUINavigate;
    public Action<int> OnUISubmit;
    public Action<int> OnUICancel;
    
    public InputDevice device;

    [SerializeField] private Player _player;
    [SerializeField] private PlayerController _playerController;
    private Vector2 _directionalInput;
    private bool _isBoosting;
    private const float UiNavDelay = .08f;
    private float _uiNavDelayTimer;

    private void Update()
    {
        _playerController.SetDirectionalInput(_directionalInput, _isBoosting);

        if (_uiNavDelayTimer > 0)
        {
            _uiNavDelayTimer -= Time.deltaTime;
        }
    }

    public void ResetInputValues()
    {
        _directionalInput = Vector2.zero;
        _isBoosting = false;
        _uiNavDelayTimer = 0;
        
        _playerController.ResetMovementValues();
    }
    
    public void OnMove(InputValue value)
    {
        Debug.Log($"OnMove - player: {_player.Index}");
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
        OnUINavigate?.Invoke(_player.Index, dir);
        
    }
    
    public void OnSubmit(InputValue value)
    {
        if(value.isPressed)
            OnUISubmit?.Invoke(_player.Index);
    }
    
    public void OnCancel(InputValue value)
    {
        if(value.isPressed)
            OnUICancel?.Invoke(_player.Index);
    }
}