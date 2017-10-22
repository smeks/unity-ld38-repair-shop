using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoTextController : MonoBehaviour {

    Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        transform.forward = cam.transform.forward;
	}
}
