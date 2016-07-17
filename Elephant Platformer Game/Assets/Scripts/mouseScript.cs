using UnityEngine;
using System.Collections;

public class mouseScript : MonoBehaviour {

    Vector2 movement;
    public int speed = 2;
	// Use this for initialization
	void Start ()
    {
        movement.x = speed;
	}

    // Update is called once per frame
    void Update()
    {
        
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Blocking" || col.gameObject.tag == "Player")
            TurnAround();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
            TurnAround();
    }

    public void TurnAround()
    {
        transform.Rotate(Vector3.up * -180);
        movement.x *= -1;
    }

    void FixedUpdate()
    {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        velocity.x = movement.x;
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

}
