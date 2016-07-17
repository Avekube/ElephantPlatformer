using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

    public enum physicalState
    {
        Idle,       //no input
        Walking,
        Jumping,
        Falling,
        Hovering,
        Dead,
    }

    public enum actionState
    {
        Idle,
        TrunkingIn,
    }

    Vector2 movement;
	public int health;
    public float speed;
    public float jumpSpeed;
    public int maxJumps;
    float groundedDelay;
    private CharacterController controller;
	// Use this for initialization
	void Start ()
    {
		health = 3;
        speed = 5.0f;
        jumpSpeed = 5.0f;
        maxJumps = 2;
        groundedDelay = 0f;
 	}

    // Update is called once per frame
    void Update()
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

        if (Input.GetKeyDown(KeyCode.Space) && maxJumps > 0)
        {
            jump();
            groundedDelay = Time.time + .5f;
        }

        else if ( Time.time > groundedDelay && isGrounded())
        {
            maxJumps = 2;
        }
	}
	
	void FixedUpdate()
    {
        Vector2 horziontalVector = GetComponent<Rigidbody2D>().velocity;
        horziontalVector.x = movement.x;
        GetComponent<Rigidbody2D>().velocity = horziontalVector;
	}

    void Die()
    {
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
    //sets verticalForce for jumps. Does not actually affect movement itself.
    private void jump()
    {
        maxJumps -= 1;
        Vector2 verticalForceVector = GetComponent<Rigidbody2D>().velocity;
        verticalForceVector.y = jumpSpeed;
        GetComponent<Rigidbody2D>().velocity = verticalForceVector;
    }

    private bool isGrounded()
    {
        Vector2 origin = GetComponent<BoxCollider2D>().bounds.center;
        origin.y -= GetComponent<BoxCollider2D>().bounds.extents.y + 0.01f;
        bool temp = Physics2D.Raycast(origin, -Vector2.up, 0.1f);
        return temp;
    }
}
