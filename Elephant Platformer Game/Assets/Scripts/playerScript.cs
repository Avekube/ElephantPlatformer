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
        GetComponent<Rigidbody2D>().velocity = movement;
    }
}
