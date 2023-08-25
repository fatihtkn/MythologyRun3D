using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoSingleton<PlayerMovement>,IMovable
{
    [SerializeField] private CanvasBasedMoveInputs MovementInputs;
     Rigidbody _rigidbody;
    
    

    private bool _moving;

    public bool IsMoving
    {
        get=>_moving; set=>_moving = value;
    }


   [SerializeField] private float moveSpeed;

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public bool moveDelta { get => _moving; set => _moving = value; }

    private void Awake()
    {
        _rigidbody= GetComponent<Rigidbody>();  
    }


    private void Move(Vector3 direction)
    {
            _rigidbody.velocity = direction * moveSpeed * Time.fixedDeltaTime;
    }

    private void Rotate(Vector3 rotation)
    {
        transform.rotation = Quaternion.LookRotation(rotation, Vector3.up);
        
    }
    private bool GetMoveStatus(Vector3 direction)
    {
        if (direction.magnitude <= 0) _moving = false;
        else _moving = true;
       
        return _moving;
    }

    private void FixedUpdate()
    {
        
       var direction = new Vector3(MovementInputs.Direction.x, 0, MovementInputs.Direction.y);
        
       
        var rotation = new Vector3(MovementInputs.Rotation.x, 0, MovementInputs.Rotation.y);
        Move(direction);
        Rotate(rotation);
        

    }
    private void Update()
    {
        var direction = new Vector3(MovementInputs.Direction.x, 0, MovementInputs.Direction.y);
        
        PlayerAnimationController.Instance.SetMoveBlend(GetMoveStatus(direction));
    }
}
