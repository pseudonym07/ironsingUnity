using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float force;
    [SerializeField] KeyCode jump;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform pivot;
    [SerializeField] float jumpForce;

    [SerializeField] Animator animator;


    bool grounded = false;

    float velocity = 0f;

    void Start()
    {
        
    }

    Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        float ad = Input.GetAxis("Horizontal");
        float ws = Input.GetAxis("Vertical");
        bool jumping = Input.GetKeyDown(jump);

        moveDirection = new Vector3(ad, 0f, ws);
        moveDirection = pivot.TransformDirection(moveDirection);
        moveDirection.y = 0;
        moveDirection.Normalize();

        grounded = Physics.Raycast(transform.position, Vector3.down, 1.4f);


        // If you're jumping and you're grounded, you jump.
        if (jumping && grounded) {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce);
            animator.SetBool("isJumping", true);
        }

        // Sets friction when you're grounded
        if (grounded) {
            animator.SetBool("isJumping", false);
            rb.drag = 6f;
        }
        else rb.drag = 1f;


        // Rotates Player
        if (moveDirection.sqrMagnitude > 0f) {
            animator.SetBool("isWalking", true);
            float targetAngle = Mathf.Atan2(ad, ws) * Mathf.Rad2Deg + pivot.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref velocity, turnSmoothTime);
            rb.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        else animator.SetBool("isWalking", false);
    }
    void FixedUpdate() {
        rb.AddForce(moveDirection * force);
    }
}
