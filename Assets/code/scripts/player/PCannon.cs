using UnityEngine;
using System.Collections;

public class PCannon : MonoBehaviour {

	public GameObject shooter;
	public GameObject firingNode;
	public GameObject bulletPrefab;

	[HideInInspector]
	public float firingDelayTicks = 0;
	[HideInInspector]
	public float reloadDelayTicks = 0;
	[HideInInspector]
	public int roundsLeft = 0;

	public int ammoPerReload = 1;
	public float firingDelaySeconds = 0.1f;
	public float reloadSpeedSeconds = 20f;
	public float bulletForce = 1000f;
	
	public bool fullAuto = false;
	
	// Update is called once per frame
	void Update () 
	{                
		if (roundsLeft > 0) 
		{
			//Firing delay
			if (firingDelayTicks <= 0) 
			{
				if (userClickFire()) 
				{
					Fire ();
					roundsLeft--;
				}
			} 
			else 
			{
				if (userClickFire()) 
				{
					//TODO play weapon click audio
				}
				firingDelayTicks -= Time.deltaTime;
			}

			//Starts reloading process
			if(roundsLeft <= 0)
			{
				reloadDelayTicks = reloadSpeedSeconds;
			}
		} 
		else if (reloadDelayTicks <= 0) 
		{
			roundsLeft = ammoPerReload;
		} 
		else 
		{
			reloadDelayTicks -= Time.deltaTime;
		}
	}

	protected virtual bool userClickFire()
	{
		return !fullAuto && Input.GetMouseButtonDown (0) || fullAuto && Input.GetMouseButton (0);
	}

	void Fire()
	{
		//TODO play firing audio
		firingDelayTicks = firingDelaySeconds;
		GameObject bullet = Instantiate(bulletPrefab, firingNode.transform.position, Quaternion.identity) as GameObject;
		bullet.transform.rotation = transform.rotation;
		bullet.GetComponent<Rigidbody>().AddRelativeForce (0f, 0f, bulletForce);
		bullet.GetComponent<Bullet> ().shooter = shooter;
	}



}
