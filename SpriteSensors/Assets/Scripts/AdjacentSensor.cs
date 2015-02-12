using UnityEngine;
using System.Collections;

public class AdjacentSensor : MonoBehaviour {

	//radius for which player will sense other objects
	public float range;
	//center of player
	public Vector2 currentPosition;
	private string sensorText;

	// Use this for initialization
	void Start ()  
	{
		range = 2;
		GameObject sphere = transform.FindChild("Sphere").gameObject;
		sphere.transform.localScale = new Vector3 (range * 2, range * 2, 1);
		sensorText = "";
	}
	
	// Update is called once per frame
	void Update ()  
	{
		currentPosition = new Vector2(transform.position.x, transform.position.y);
		findAdjObjects (range, currentPosition);
	}

	void findAdjObjects (float radius, Vector2 center)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius);

		//init gui text 
		sensorText = "";

		//print out info for each object except player
		for (int i=0; i < colliders.Length; i++)
		{
			if ( !colliders[i].gameObject.CompareTag("Player") && !colliders[i].gameObject.CompareTag("Sphere") )
			{
				//list off objects to console
				//Debug.Log("Hit: " + colliders[i].name);
				
				Transform target = colliders[i].gameObject.transform;

				//calc distance from player to object
				float distance = Vector2.Distance(transform.position, target.position);

				//calc angle between player heading and object
				Vector2 targetDir = new Vector2(target.position.x, target.position.y) - new Vector2(transform.position.x, transform.position.y);
				Vector2 heading = transform.up;
				float angle = Vector2.Angle(targetDir, heading);
				//print to screen
				sensorText += colliders[i].name + " in range:\n Angle: " + angle.ToString("F2") + "  Distance: " + distance.ToString("F2") + "\n";

			}
		}
	}

	void OnGUI()
	{
		//print sensor data to upper right hand corner
		GUI.Label (new Rect (Screen.width-190,0,190,800), sensorText);
	}
}
