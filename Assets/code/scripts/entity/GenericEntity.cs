using UnityEngine;
using System.Collections;

public class GenericEntity : Entity
{
	//Objects to be recolored when SetBodyColor is called
	public GameObject[] colorObjects;

	public GameObject deadVersion;

	protected override void SetBodyColor (Color color)
	{
		if (colorObjects != null) {
			foreach (GameObject obj in colorObjects) {
				obj.GetComponent<Renderer> ().material.color = color;
			}
		}
	}

	protected override void OnDeath ()
	{
		Instantiate (deadVersion, transform.position, transform.rotation);
		Destroy (gameObject);	

		if (gameObject.tag == "Player") {
			//Disable camera
			Camera.main.enabled = false;					
			//Create new camera object for the player
			Instantiate (Resources.Load ("player/camera_dummy"), transform.position, Quaternion.identity);
		}
	}
}
