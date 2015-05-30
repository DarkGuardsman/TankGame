using UnityEngine;
using System.Collections;

public class GenericEntity : Entity
{
	//Objects to be recolored when SetBodyColor is called
	public GameObject[] colorObjects;

	protected override void SetBodyColor (Color color)
	{
		if (colorObjects != null) {
			foreach (GameObject obj in colorObjects) {
				obj.GetComponent<Renderer> ().material.color = color;
			}
		}
	}
}
