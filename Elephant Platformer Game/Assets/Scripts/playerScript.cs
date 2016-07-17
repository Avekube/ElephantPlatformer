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
    public float airTime;
    public float verticalForce;
    private float reverseGravity;
    private float distanceToGround;
    private CharacterController controller;
	// Use this for initialization
	void Start ()
    {
		health = 3;
        speed = 10.0f;
        jumpSpeed = 20.0f;
        maxJumps = 2;
        airTime = 2.0f;
        verticalForce = 0;
        reverseGravity = airTime * Physics2D.gravity.y;
        distanceToGround = GetComponent<Collider2D>().bounds.extents.y;
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
        }

        if (isGrounded())
        {
            maxJumps = 2;
            verticalForce = 0;
            reverseGravity = airTime * Physics2D.gravity.y;
        }

        if (Input.GetKeyDown(KeyCode.Space) && verticalForce != 0)
        {
            reverseGravity -= Time.deltaTime;
            verticalForce += reverseGravity * Time.deltaTime;
        }
	}
	
	void FixedUpdate()
    {
        Vector2 horziontalVector = GetComponent<Rigidbody2D>().velocity;
        horziontalVector.x = movement.x;
        GetComponent<Rigidbody2D>().velocity = horziontalVector;

        Vector2 verticalForceVector = GetComponent<Rigidbody2D>().velocity;
        verticalForceVector.y = verticalForce;
        GetComponent<Rigidbody2D>().AddForce(verticalForceVector);
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
        verticalForce = jumpSpeed;
    }

    private bool isGrounded()
    {
        Vector2 origin = GetComponent<BoxCollider2D>().bounds.center;
        bool temp = Physics2D.Raycast(origin, -Vector2.up, distanceToGround + 0.1f);
        return temp;
    }
}
