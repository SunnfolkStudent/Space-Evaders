using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D _rb;
    
    [Header("Movement variables")] 
    private float _moveSpeed = 6;
    [HideInInspector] public Vector2 moveVector = Vector2.zero;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    public void UpdateMovement(Vector2 moveDirection)
    {
        moveVector = moveDirection;
        _rb.velocity = moveVector * _moveSpeed;
    }
}
