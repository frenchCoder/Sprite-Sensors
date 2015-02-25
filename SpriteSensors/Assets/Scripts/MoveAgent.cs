
﻿using UnityEngine;
using System.Collections;

public class MoveAgent : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate(Vector3.forward * -Input.GetAxis("Horizontal") * 100 * Time.deltaTime);
		
		transform.position += transform.up * Input.GetAxis("Vertical") * 5 * Time.deltaTime;
		
	}
}