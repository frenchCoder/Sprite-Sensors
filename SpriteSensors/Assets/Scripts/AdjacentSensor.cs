using UnityEngine;
using System.Collections;

public class AdjacentSensor : MonoBehaviour {

	//radius for which player will sense other objects
	public float range;
	private string sensorText;
	private GameObject sphereChild;

	// Use this for initialization
	void Start ()  
	{
		range = 2;
		sphereChild = transform.FindChild("Sphere").gameObject;
		sphereChild.transform.localScale = new Vector3 (range * 2, range * 2, 1);
		sensorText = "";
	}
	
	// Update is called once per frame
	void Update ()  
	{
		Vector3 currentPosition = transform.position;
		findAdjObjects (range, currentPosition);
		sphereChild.transform.localScale = new Vector3 (range * 2, range * 2, 1);
	}

	void findAdjObjects (float radius, Vector3 center)
	{
		Collider[] colliders = Physics.OverlapSphere(center, radius);

		//init gui text 
		sensorText = "";

		//print out info for each object except player
		for (int i=0; i < colliders.Length; i++)
		{
			if ( colliders[i].gameObject.CompareTag("Enemy") )
			{
				//list off objects to console
				Debug.Log("Hit: " + colliders[i].name);
				
				Transform target = colliders[i].gameObject.transform;

				//calc distance from player to object
				float distance = Vector3.Distance(transform.position, target.position);

				//calc angle between player heading and object
				Vector3 targetDir = target.position - transform.position;
				Vector3 heading = transform.up;


				//get angle and negate it if object is on left of Player
				float angle = Vector3.Angle(targetDir, heading);
				//parent the object to get it's local position relative to the player
				colliders[i].transform.parent = transform;
				
				//if local x pos is > than the player's then it is on the right
				if(colliders[i].transform.localPosition.x < 0)
				{
					angle = -angle;
				}
				
				//unparent the object so it doesn't follow the player anymore
				colliders[i].transform.parent = null;

				//print to screen
				Debug.Log (colliders[i].name + " in range:\n Angle: " + angle.ToString("F2") + "  Distance: " + distance.ToString("F2") + "\n");
				sensorText += "\n( angle: " + angle.ToString("F2") + ", dist: " + distance.ToString("F2") + ")";

			}
		}
	}

	void OnGUI()
	{
		//print sensor data to upper right hand corner
		GUI.Label (new Rect (Screen.width-190,0,190,800), "Adj Sensor: " + sensorText);
	}
}
