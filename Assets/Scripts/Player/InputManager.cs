using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance { get { return _instance; } }

    public PlayerControls playerControls;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        playerControls = new PlayerControls();
        Cursor.visible = false;
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    public Vector2 GetMovementInput()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }
    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }
    public bool GetJumpInput()
    {
        return playerControls.Player.Jump.triggered;
    }
    public bool GetEBtnPressed()
    {
        return playerControls.Player.TranlateShelfE.triggered;
    }
    public bool GetQBtnPressed()
    {
        return playerControls.Player.TranlateShelfQ.triggered;
    }
    public bool GetGBtnPressed()
    {
        return playerControls.Player.TranlateShelfG.triggered;
    }
    public Vector2 GetMousePosition()
    {
        return playerControls.Player.Mouse.ReadValue<Vector2>();
    }
    public bool GetMouseLeftClick()
    {
        return playerControls.Player.MouseLeftBtn.triggered;
    }
}
