using UnityEngine;
using System.Collections;

public class Tank : Entity
{	
	public GameObject dummyCameraPrefab;

	private GameObject turret;
	private GameObject hull;
	private GameObject cannon;

	void Awake()
	{
		//TODO replace with iterator that finds all materials and replaces with grey
		turret = gameObject.transform.FindChild ("turret").gameObject;
		hull = gameObject.transform.FindChild ("hull").gameObject;
		cannon = turret.transform.FindChild ("cannon").gameObject;
	}

	protected override void OnDeath()
	{
		base.OnDeath ();

		//TODO replace with wreck rather than disabling scripts

		//Turn off all engines, its dead so it can't move
		MovementEngine[] engines = gameObject.GetComponents<MovementEngine> ();
		foreach(MovementEngine e in engines)
		{
			e.enabled = false;
		}

		turret.transform.FindChild ("body").gameObject.GetComponent<Renderer>().material.color = Color.gray;
		cannon.transform.FindChild ("barrel").gameObject.GetComponent<Renderer>().material.color = Color.gray;
		hull.GetComponent<Renderer>().material.color = Color.gray;

		if (gameObject.tag == "Player") {
			//Disable movement script
			gameObject.GetComponent<PMovement>().enabled = false;

			//Disable turret rotation script
			turret.GetComponent<PTurret>().enabled = false;

			//Disable cannon firing script
			cannon.GetComponent<PCannon>().enabled = false;

			//Disable camera
			turret.transform.FindChild("Main Camera").gameObject.GetComponent<Camera>().enabled = false;

			//Create new camera object for the player
			Instantiate(dummyCameraPrefab, transform.position, Quaternion.identity);
		}
	}
}
