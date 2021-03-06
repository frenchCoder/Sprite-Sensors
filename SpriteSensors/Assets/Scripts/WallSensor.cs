﻿using UnityEngine;
using System.Collections;
using System;

public class WallSensor : MonoBehaviour 
{
	public int sensorRange;
	private float distLeftDiag;
	private float distRightDiag;
	private float distUp;
	// Use this for initialization
	void Start () 
	{
		sensorRange = 4;
		distLeftDiag = (float)sensorRange;
		distRightDiag = (float)sensorRange;
		distUp = (float)sensorRange;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//UP: Looks for wall in front of agent
		Vector3 up = transform.up;
		up.Normalize();
		distUp = check_and_draw (transform.up, "in front");

		//RIGHT DIAGONAL: Looks for wall in forward right diagonal of agent
		Vector3 right_diagonal = transform.right + transform.up;
		right_diagonal.Normalize();
		distRightDiag = check_and_draw (right_diagonal, "right diagonal");

		//LEFT DIAGONAL: Looks for wall in forward left diagonal of agent
		Vector3 left_diagonal = transform.up - transform.right;
		left_diagonal.Normalize();
		distLeftDiag = check_and_draw (left_diagonal, "left diagonal");
	}

	void OnGUI()
	{
		String feelerDeets = "(" + distLeftDiag.ToString("F2") + ", " + distUp.ToString("F2") + ", " + distRightDiag.ToString("F2") + ")";

		GUI.Label(new Rect (0, Screen.height-90, 200, 100),  "Wall Sensor:\n" + feelerDeets);
	}

	float check_and_draw(Vector3 dir, String dir_text)
	{
		float buffer = 0.000001f;
		RaycastHit hit;
		string text = "";
		
		if (Physics.Raycast (transform.position, dir, out hit, sensorRange + buffer) && hit.transform.tag.Equals("Wall"))
		{
			print (hit.transform.tag);
			text = "\nWall detected " + dir_text + " of agent: " + hit.distance.ToString("F2") + " units away.";
			Debug.DrawRay(transform.position, dir * hit.distance);
			print ( text );
			return hit.distance;
		}
		else
		{
			text = "\nWall not " + dir_text + " of agent.";
			Debug.DrawRay(transform.position, dir * sensorRange);
			print ( text );
		}
		return (float)sensorRange;
	}
}
