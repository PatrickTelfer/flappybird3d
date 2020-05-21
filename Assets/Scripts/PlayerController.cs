using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rb;
    Vector2 playerInput;
    bool jumpRequested;
    Vector3 startPos;

    public float jumpForce;

    void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        HandleInput();

        rb.AddForce(new Vector3(0, 0, -playerInput.x) * jumpForce * 10);

        jumpRequested = Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0;
        if (jumpRequested)
        {
            Jump();
        }
    }

    private void HandleInput()
    {
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");

    }

    private void Jump ()
    {
        rb.velocity = new Vector3(0, jumpForce, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
            transform.position = startPos;
            GameManager.displayText(true);
            Time.timeScale = 0;
        
    }


}
