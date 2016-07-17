using UnityEngine;
using System.Collections;

public class monkeyScript : MonoBehaviour {

    private float nextAtk = 0.0f;
    public float hitDelay = 5.0f;
    public float missDelay = 3.0f;
    bool atkReady = true;

    // Use this for initialization
    void Start ()
    {
        
	}

    public void ThrowBanana()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            atkReady = false;
            GameObject newBanana = (GameObject)Instantiate(Resources.Load("Banana"), transform.position, Quaternion.identity);
            Vector3 angle = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;
            newBanana.SendMessage("Initialize", transform.gameObject);
            newBanana.GetComponent<Rigidbody2D>().velocity = angle * newBanana.GetComponent<bananaScript>().speed;
        }
    }

    void Hit()
    {
        nextAtk = Time.time + hitDelay;
        atkReady = true;
    }

    void Miss()
    {
        nextAtk = Time.time + missDelay;
        atkReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (atkReady && GetComponent<Renderer>().isVisible && Time.time > nextAtk)
            ThrowBanana();
	}
    
    void FixedUpdate()
    {
        
    }

}
