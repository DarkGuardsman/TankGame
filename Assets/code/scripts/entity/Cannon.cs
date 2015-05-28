using UnityEngine;
using System.Collections;
/// <summary>
/// Basic gun behavior script. Includes firing delay, clip size, and reload time
/// </summary>
public abstract class Cannon : MonoBehaviour {

	/// <summary>
	/// The game object that will take credit for firing the bullet
	/// </summary>
	public GameObject shooter;
	/// <summary>
	/// Gameobject to be used as the exit point for the bullet. Prevents clipping of the collision box of the gun.
	/// </summary>
	public GameObject firingNode;
	/// <summary>
	/// The bullet prefab to create and fire.
	/// </summary>
	public GameObject bulletPrefab;
	
	[HideInInspector]
	public float firingDelayTicks = 0;
	[HideInInspector]
	public float reloadDelayTicks = 0;
	[HideInInspector]
	public int roundsLeft = 0;

	//Ammount of ammo in the clip
	public int ammoPerReload = 1;
	//delay between rounds firing
	public float firingDelaySeconds = 0.1f;
	//time in seconds it take to reload the clip
	public float reloadSpeedSeconds = 20f;
	//force to apply to the bullet
	public float bulletForce = 1000f;
	
	public bool fullAuto = false;
	
	// Update is called once per frame
	protected virtual void Update () 
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
	
	protected abstract bool userClickFire ();
	
	protected virtual void Fire()
	{
		//TODO play firing audio
		firingDelayTicks = firingDelaySeconds;
		GameObject bullet = Instantiate(bulletPrefab, firingNode.transform.position, Quaternion.identity) as GameObject;
		bullet.transform.rotation = transform.rotation;
		bullet.GetComponent<Rigidbody>().AddRelativeForce (0f, 0f, bulletForce);
		bullet.GetComponent<Bullet> ().shooter = shooter;
	}
}
