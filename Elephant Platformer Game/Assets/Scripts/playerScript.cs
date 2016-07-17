using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

    Vector2 movement;
    public int speed;
	// Use this for initialization
	void Start ()
    {
        speed = 10;
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

    void FixedUpdate()
    {
        Vector2 temp = GetComponent<Rigidbody2D>().velocity;
        temp.x = movement.x;
        GetComponent<Rigidbody2D>().velocity = temp;

        //GetComponent<Rigidbody2D>().addForce(jumpingForce);
        //add jump force

    }
}
