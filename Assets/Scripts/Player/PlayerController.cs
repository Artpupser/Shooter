using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Vector2 direction;

    [SerializeField] private LayerMask solid;

    [Range(0, 2)][SerializeField] private byte maxCountJump;
    [Range(0, 2)][SerializeField] private byte countJump;

    [SerializeField] private float jumpCd;

    [SerializeField] private Joystick walkJoystick;
    [SerializeField] private Joystick shootJoystick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform legs;
    private bool jumped = false;

    private void Start()
    {
        countJump = maxCountJump;   
    }
    private void Update()
    {
        direction = new(walkJoystick.Direction.x * walkSpeed, rb.velocity.y);
        if (walkJoystick.Direction.y > .5f)
            StartCoroutine(Jump());
        Move();
    }
    private void FixedUpdate()
    {
        if (IsGrounded())
            countJump = maxCountJump;
    }
    private void Move() => rb.velocity = direction;
    private IEnumerator Jump()
    {
        if (!jumped && countJump > 0)
        {
            jumped = true;
            direction.y = walkJoystick.Direction.y * jumpSpeed;
            countJump--;
            yield return new WaitForSeconds(jumpCd);
            jumped = false;
        }
        
    }
    private bool IsGrounded() => Physics2D.OverlapCircle(legs.position, 0.2f, solid);
}
