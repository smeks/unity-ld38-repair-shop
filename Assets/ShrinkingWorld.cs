using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingWorld : MonoBehaviour {

    public GameObject player;

    Vector3 worldScale = new Vector3(0.005f, 0.005f, 0.005f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var newScale = worldScale * Time.deltaTime;
        transform.localScale -= newScale;
    }
}
