using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

    public enum physicalStates
    {
        Idle,       //no input
        Walking,
        Jumping,
        Falling,
        Hovering,
        Dead,
    }    

    public enum actionStates
    {
        Idle,
        TrunkingIn,
    }

    public int physicalState;
    public int actionState;
    Vector2 movement;
	public int health;
    public float speed;
    public float jumpSpeed;
    public int maxJumps;
    float groundedDelay;

	// Use this for initialization
	void Start ()
    {
        physicalState = (int)physicalStates.Idle;
        actionState = (int)actionStates.Idle;
		health = 3;
        speed = 5.0f;
        jumpSpeed = 5.0f;
        maxJumps = 2;
        groundedDelay = 0f;
 	}

    // Update is called once per frame
    void Update()
    {
        switch (physicalState)
        {
            case (int)physicalStates.Idle:
            case (int)physicalStates.Walking:
            case (int)physicalStates.Jumping:
            case (int)physicalStates.Falling:
            case (int)physicalStates.Hovering:
            case (int)physicalStates.Dead:
                break;
        }

        switch (actionState)
        {
            case (int)actionStates.Idle:
            case (int)actionStates.TrunkingIn:
                break;
        }
	}
	
	void FixedUpdate()
    {
        switch (physicalState)
        {
            case (int)physicalStates.Idle:
            case (int)physicalStates.Walking:
                horizontalMovement();
                jumpCheck();
                updatePhysEnum();
                break;
            case (int)physicalStates.Jumping:
            case (int)physicalStates.Falling:
                horizontalMovement();
                jumpCheck();
                hoverCheck();
                updatePhysEnum();
                break;
            case (int)physicalStates.Hovering:
            case (int)physicalStates.Dead:
                break;
        }

        switch (actionState)
        {
            case (int)actionStates.Idle:
            case (int)actionStates.TrunkingIn:
                break;
        }
    }

    //[FU] manages physicalStates enum transitions
    private void updatePhysEnum()
    {
        if (GetComponent<Rigidbody2D>() != null) {
            Vector2 movementVector = GetComponent<Rigidbody2D>().velocity;
            if (movementVector.x != 0 && physicalState != (int)physicalStates.Jumping && physicalState != (int)physicalStates.Falling)
            {
                physicalState = (int)physicalStates.Walking;
            }
            else if (movementVector.y > 0)
            {
                physicalState = (int)physicalStates.Jumping;
            }
            else if (movementVector.y < 0)
            {
                physicalState = (int)physicalStates.Falling;
            }
            else
            {
                physicalState = (int)physicalStates.Idle;
            }
        }
    }

    //[FU] handles horizontal movement
    private void horizontalMovement()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            switch ((int)horizontalInput)
            {
                case 1:
                    movement.x = 1;
                    break;
                case -1:
                    movement.x = -1;
                    break;
                case 0:
                    movement.x = 0;
                    break;
            }
            movement.x *= speed;

            Vector2 horziontalVector = GetComponent<Rigidbody2D>().velocity;
            horziontalVector.x = movement.x;
            GetComponent<Rigidbody2D>().velocity = horziontalVector;
        }
    }

    //[FU] applies upwards velocity, checking if able to jump
    private void jumpCheck()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            if (Input.GetKeyDown(KeyCode.Space) && maxJumps > 0)
            {
                physicalState = (int)physicalStates.Jumping;
                maxJumps -= 1;
                Vector2 verticalForceVector = GetComponent<Rigidbody2D>().velocity;
                verticalForceVector.y = jumpSpeed;
                GetComponent<Rigidbody2D>().velocity = verticalForceVector;
                groundedDelay = Time.time + .5f;
            }
            else if (Time.time > groundedDelay && isGrounded())
            {
                maxJumps = 2;
            }
        }
    }

    //[FU] reduces downwards velocity if in the air
    private void hoverCheck()
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            if (Input.GetKey(KeyCode.Space) && GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                print("hovering");
                GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            }
            else
            {
                print("not hovering");
                GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }
    }

    void Die()
    {
        physicalState = (int)physicalStates.Dead;
        this.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
            GetHit();
    }

    void GetHit()
    {
        health--;
        if (health == 0)
            Die();
    }

    //checks if there is a collision below the collider
    private bool isGrounded()
    {
        Vector2 origin = GetComponent<BoxCollider2D>().bounds.center;
        origin.y -= GetComponent<BoxCollider2D>().bounds.extents.y + 0.01f;
        bool temp = Physics2D.Raycast(origin, -Vector2.up, 0.1f);
        return temp;
    }
}
