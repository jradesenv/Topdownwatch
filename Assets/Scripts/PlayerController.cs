using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool isMoving;
    public float moveSpeed;
    public bool isLookingUp;

    protected Vector3 targetPosition;
    protected float horizontalAxis;
    protected float verticalAxis;
    protected Rigidbody2D myRigidbody;
    protected Vector2 currentVelocity;
    protected AimHelper.AimAngle currentAimAngle;
    protected AimHelper.AimAngle lastAimAngle;
    protected Animator myAnimator;


    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateMouseKeyboardInputs();
        UpdateMovement();
        UpdateSprites();
        UpdateCacheVariables();
        myRigidbody.velocity = currentVelocity;
    }

    public virtual void UpdateMovement()
    {
        //move
        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            var moveInput = new Vector2(horizontalAxis, verticalAxis);
            currentVelocity = moveInput * moveSpeed;
        }
        else
        {
            currentVelocity = Vector2.zero;
        }
    }

    private void UpdateSprites()
    {
        currentAimAngle = AimHelper.GetCurrentAimAngle(targetPosition, transform.position);

        if (currentAimAngle != lastAimAngle)
        {
            isLookingUp = currentAimAngle.y == Enums.Angles.Y.Top;

            if ((lastAimAngle == null && currentAimAngle.x == Enums.Angles.X.Left) || (lastAimAngle != null && currentAimAngle.x != lastAimAngle.x))
            {
                Flip();
            }
        }
    }

    public virtual void UpdateCacheVariables()
    {
        lastAimAngle = currentAimAngle;
        myAnimator.SetBool("isMoving", isMoving);
        myAnimator.SetBool("isLookingUp", isLookingUp);
    }

    protected virtual void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1;
        gameObject.transform.localScale = newScale;
    }

    private void UpdateMouseKeyboardInputs()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
