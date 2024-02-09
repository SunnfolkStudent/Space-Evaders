using UnityEngine;
using UnityEngine.Serialization;

public class Input_Controller : MonoBehaviour
{
    private  Controls _input;

    [HideInInspector] public Vector2 moveDirection;
    
    private void Update()
    {
        moveDirection = _input.Player.Movement.ReadValue<Vector2>();
    }
    private void Awake()
    {
        _input = new Controls();
    }
    
    private void OnEnable() { _input.Enable(); }
    private void OnDisable() { _input.Disable(); }
}