using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
      [HideInInspector] public int Index;

      public Action<Player> OnUnjoined;
      public PlayerInput PlayerInput;
      public PlayerInputHandler InputHandler;
      public PlayerController Controller;

      [SerializeField] private GameObject _artParent;
      [SerializeField] private SpriteRenderer _renderer;
      
      public Color Color { get; private set; }
      
      private void Awake()
      {
            SetVisible(false);
      }

      public void SetupForUI()
      {
            SetControlsActive(false);
            SetVisible(false);
            SetPosition(new Vector2(0, 0));
            SetActionMap("UI");
      }
      
      public void SetControlsActive(bool active)
      {
            Controller.PlayerCanMove = active;
      }

      public void SetVisible(bool visible)
      {
            _artParent.SetActive(visible);
      }

      public void SetPosition(Vector2 pos)
      { 
            transform.position = pos;
      }

      public void SetActionMap(string map)
      {
            InputHandler.ResetInputValues();
            PlayerInput.SwitchCurrentActionMap(map);
      }
      
      public void SetColor(Color color)
      {
            Color = color;
            _renderer.color = Color;
      }

      private void OnDestroy()
      {
            OnUnjoined?.Invoke(this);
      }
}