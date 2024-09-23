using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] AudioSourceController asc;
    [SerializeField] private float speed;
    [SerializeField] private float jump;

    private bool isGrounded;
    private bool isMovable;
    private Vector3 movement;
    private LookDirection ld;

    public enum LookDirection
    {
        forward,
        backward,
        left,
        right
    }

    private void Start()
    {
        isGrounded = true;
        isMovable = true;
        movement = Vector3.zero;
        ld = LookDirection.forward;
    }

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            if (!isGrounded)
            {
                isGrounded = true;
                asc.PlayClip("Grounded");
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Start"))
        {
            GameManager.Instance.StartTimer();
        }

        if (col.gameObject.CompareTag("Game Over"))
        {
            GameManager.Instance.Lose();
        }
        
        if (col.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.FinishLevel();
        }
    }

    private void CheckInput()
    {
        isMovable = !UIManager.Instance.IsPaused;

        if (isMovable)
        {
            movement = Vector3.zero;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                movement += Vector3.left;
                ld = LookDirection.left;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                movement += Vector3.right;
                ld = LookDirection.right;
            }
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                movement += Vector3.forward;
                ld = LookDirection.forward;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                movement += Vector3.back;
                ld = LookDirection.backward;
            }
        }
    }

    private void MovePlayer()
    {
        float y_vel = rb.velocity.y;
        Vector3 temp_v = new Vector3(0f, y_vel, 0f);
        rb.velocity = movement * speed + temp_v;
    }

    private void RotatePlayer()
    {
        Vector3 temp_rotation = new Vector3();
        switch (ld)
        {
            case LookDirection.forward:
                temp_rotation = new Vector3(0f, 0f, 0f);
                gameObject.transform.localEulerAngles = temp_rotation;
                break;
            case LookDirection.backward:
                temp_rotation = new Vector3(0f, 180f, 0f);
                gameObject.transform.localEulerAngles = temp_rotation;
                break;
            case LookDirection.left:
                temp_rotation = new Vector3(0f, -90f, 0f);
                gameObject.transform.localEulerAngles = temp_rotation;
                break;
            case LookDirection.right:
                temp_rotation = new Vector3(0f, 90, 0f);
                gameObject.transform.localEulerAngles = temp_rotation;
                break;
            default:
                Debug.LogWarning("Àﬂœ»«ƒ≈÷");
                break;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddRelativeForce(0f, jump, 0f);
            asc.PlayClip("Jump");
            isGrounded = false;
        }
    }
}
