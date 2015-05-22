using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	public GameObject shooter;
	public GameObject firingNode;
	public GameObject bulletPrefab;
	public int reloadTicks = 0;
	public int reloadDelay = 100;
	public int bulletForce = 100;
	
	// Update is called once per frame
	void Update () 
	{        
        //if (Input.GetMouseButtonDown(1)) TODO use to lock turret rotation
        //    Debug.Log("Pressed right click.");
        
        //if (Input.GetMouseButtonDown(2)) TODO switch ammo?
        //    Debug.Log("Pressed middle click.");
		
		if (reloadTicks <= 0) 
		{
			if (Input.GetMouseButtonDown(0))
			{
				Fire ();
			}
		} 
		else 
		{
			if (Input.GetMouseButtonDown(0))
			{
				//TODO play weapon click audio
			}
			reloadTicks--;
		}
	}

	void Fire()
	{
		//TODO play firing audio
		reloadTicks = reloadDelay;
		GameObject bullet = Instantiate(bulletPrefab, firingNode.transform.position, Quaternion.identity) as GameObject;
		bullet.transform.rotation = transform.localRotation;
		bullet.GetComponent<Rigidbody>().AddRelativeForce (0f, 0f, bulletForce);
		bullet.GetComponent<Bullet> ().shooter = shooter;
	}



}
