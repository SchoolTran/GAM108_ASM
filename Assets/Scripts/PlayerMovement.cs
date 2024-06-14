using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumbSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite ();
    }

    void OnMove(InputValue value) 
    {
        moveInput = value.Get<Vector2 >();
        Debug.Log(moveInput);
    }

    void OnJumb(InputValue value)
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (value.isPressed)
        {
            myRigidbody.velocity = new Vector2(0f, jumbSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVelocyti = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocyti;

        bool hasHori = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHori);
    }

    void FlipSprite()
    {
        bool hasHori = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (hasHori)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
