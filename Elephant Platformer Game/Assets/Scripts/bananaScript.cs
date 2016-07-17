using UnityEngine;
using System.Collections;

public class bananaScript : MonoBehaviour {

    public float speed = 1f;
    public GameObject monkey;

    // Use this for initialization
	void Start () {
	
	}

    void Initialize(GameObject sourceMonkey)
    {
        monkey = sourceMonkey;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Blocking" || col.gameObject.tag == "Ground")
        {
            monkey.SendMessage("Miss");
            this.gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "Player")
        {
            monkey.SendMessage("Hit");
            col.SendMessage("GetHit");
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
