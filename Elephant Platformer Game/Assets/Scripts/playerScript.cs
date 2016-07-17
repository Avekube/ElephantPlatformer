using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

    Vector2 movement;
    public int speed = 10;
    public int health = 3;

	// Use this for initialization
	void Start ()
    {
        
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

    void FixedUpdate()
    {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = movement.x;
        GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
