using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D _rb;
    
    [Header("Movement variables")] 
    private float _moveSpeed = 6;
    private float _maxMoveSpeed = 10;
    private float _moveSpeedIncrease = 0.05f;
    [HideInInspector] public Vector2 moveVector = Vector2.zero;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    public void UpdateMovement(Vector2 moveDirection)
    {
        if (_moveSpeed < _maxMoveSpeed)
            _moveSpeed += _moveSpeedIncrease * Time.deltaTime;
        moveVector = moveDirection;
        _rb.velocity = moveVector * _moveSpeed;
    }
}
