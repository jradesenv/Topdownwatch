using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool isMoving;
    public float moveSpeed;
    public bool isLookingUp;
    public float timeToDeath;
    public float timeDeathAnimation;
    public bool isDead;

    protected Vector3 targetPosition;
    protected float horizontalAxis;
    protected float verticalAxis;
    protected Rigidbody2D myRigidbody;
    protected Vector2 currentVelocity;
    protected AimHelper.AimAngle currentAimAngle;
    protected AimHelper.AimAngle lastAimAngle;
    protected Animator myAnimator;    
    protected float timeToDeathCounter;
    protected float timeDeathAnimationCounter;

    
    protected Vector2 lastMoveInput;


    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        timeToDeathCounter = timeToDeath;
        isDead = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isDead) {
            timeDeathAnimationCounter -= Time.deltaTime;
            if(timeDeathAnimationCounter > 0) {
                currentVelocity = new Vector2(lastMoveInput.x * -1, lastMoveInput.y * -1) * moveSpeed;
            } else {
                currentVelocity = Vector2.zero;
            }
        } else {
            timeToDeathCounter -= Time.deltaTime;
            if (timeToDeathCounter <= 0) {
                isDead = !isDead;
                timeToDeathCounter = timeToDeath;
                timeDeathAnimationCounter = timeDeathAnimation;
            }
        }

        UpdateMouseKeyboardInputs();
        UpdateMovement();
        UpdateSprites();
        UpdateCacheVariables();
        myRigidbody.velocity = currentVelocity;
    }

    public virtual void UpdateMovement()
    {
        if(isDead)                          
        {
            isMoving = false;
            
        } else {
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
                lastMoveInput = new Vector2(horizontalAxis, verticalAxis);
                currentVelocity = lastMoveInput * moveSpeed;
            }
            else
            {
                currentVelocity = Vector2.zero;
            }
        }
    }

    private void UpdateSprites()
    {
        if (!isDead) {
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
    }

    public virtual void UpdateCacheVariables()
    {
        lastAimAngle = currentAimAngle;
        myAnimator.SetBool("isMoving", isMoving);
        myAnimator.SetBool("isLookingUp", isLookingUp);
        myAnimator.SetBool("isDead", isDead);
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
