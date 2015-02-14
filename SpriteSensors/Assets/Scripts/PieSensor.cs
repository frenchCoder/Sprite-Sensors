using UnityEngine;
using System.Collections;

public class PieSensor : MonoBehaviour {

	//the range of objects that will be detected
	public float range;

	//number of objects in up, down, left, and right sections of the pi sensor
	private int u;
	private int d;
	private int l;
	private int r;

	//output string of the function
	private string output;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//reset the counters
		u = 0;
		d = 0; 
		l = 0;
		r = 0;

		//get all objects in radius around the player
		Collider[] p = Physics.OverlapSphere(transform.position, range);

		//cycle through all objects in radius
		for(int i = 0; i < p.Length; i++)
		{
			//if object is not the player and is tagged as an agent
			if(transform != p[i].transform && p[i].gameObject.tag == "Agent")
			{
				//get the vector between them
				Vector3 targetDir = p[i].transform.position - transform.position;
	        	//get the heading
				Vector3 forward = transform.up;
				//get the angle between the heading and the object
				float angle = Vector3.Angle(targetDir, forward);

				//if in front
				if(angle < 45)
				{
					u++;
				}

				//if in back
				else if(angle > 135)
				{
					d++;
				}

				//if on the sides
				else
				{
					//parent the object to get it's local position relative to the player
					p[i].transform.parent = transform;

					//if local x pos is > than the player's then it is on the right
					if(p[i].transform.localPosition.x > transform.position.x)
					{
						r++;
					}

					//otherwise it is on the left
					else
					{
						l++;
					}

					//unparent the object so it doesn't follow the player anymore
					p[i].transform.parent = null;
				}
				
			}

		}
		//print to console
		output = u + "," + r + "," + d + "," + l;
		print(u + "," + r + "," + d + "," + l);

	}

	void OnGUI()
	{
		GUILayout.Label("Position -- x: " + transform.position.x.ToString("F") + "   y: " + transform.position.y.ToString("F"));
		GUILayout.Label("Heading: " + transform.eulerAngles.z.ToString("F"));
		GUILayout.Label("Activation Levels : " + output);
	}

}
