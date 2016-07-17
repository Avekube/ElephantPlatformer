using UnityEngine;
using System.Collections;

public class peelScript : MonoBehaviour {

    public float lingerTime = 3f;
    float endTime;
	// Use this for initialization
	void Start () {
        endTime = Time.time + lingerTime;
	}
	
	// Update is called once per frame
	void Update () {
	    if( Time.time > endTime )
            this.gameObject.SetActive(false);
    }
}
