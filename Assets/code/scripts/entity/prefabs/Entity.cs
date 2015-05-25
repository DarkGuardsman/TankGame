using UnityEngine;
using System.Collections;
using BuiltBroken.Damage;
/// <summary>
/// Basic gameobject that can take damage
/// </summary>
public class Entity : MonoBehaviour, IEntity {

	public float hp = 1;
	public int deathTicks = -10;
	public bool alive = true;

	// Update is called once per frame
	protected virtual void Update () {
		if(hp <= 0 && alive)
		{
			alive = false;
			OnDeath();
		}

		if(isDead()) {
			if(deathTicks != -10)
			{
				deathTicks--;
				if(deathTicks <= 0)
				{
					BeforeDestroyed();
					Destroy(gameObject);
				}
			}
		}
	}

	public virtual float getHeath()
	{
		return hp;
	}

	public virtual void setHeath(float amount)
	{
		hp = amount;
	}

	public virtual bool damageEntity(DamageSource source, float damage)
	{
		if (alive) {
			setHeath(getHeath() - damage);
			return true;
		}
		return false;
	}

	public virtual bool isDead()
	{
		return !alive;
	}

	/// <summary>
	/// Called the first tick after the entity has died
	/// </summary>
	protected virtual void OnDeath()
	{
		if (gameObject.tag == "Player") {
			//Disable camera
			Camera.main.enabled = false;
			
			//Create new camera object for the player
			Instantiate(Resources.Load("camera_dummy"), transform.position, Quaternion.identity);
		}
	}

	/// <summary>
	/// Called in the death update loop right before the game object is destoryed
	/// </summary>
	protected virtual void BeforeDestroyed()
	{

	}

	/// <summary>
	/// Attacks the game object threw all of it's IEntity scripts
	/// </summary>
	/// <param name="target">Game object to look for an IEntity script on</param>
	/// <param name="source">Type of damage and it's source object</param>
	/// <param name="damage">amount of damage to doDamage.</param>
	protected virtual bool AttackGameObjectOnly(GameObject target, DamageSource source, float damage)
	{
		bool damagedTarget = false;
		IEntity[] scripts = target.GetComponents<IEntity>();
		for(int i = 0; i < scripts.Length; i++)
		{
			if(scripts[i].damageEntity(source, damage))
			{
				damagedTarget = true;
			}
		}
		return damagedTarget;
	}

	/// <summary>
	/// Iterates threw object & it's parent objects to find an Entity tag. Once it finds
	/// a matching object it attacks the object if it has an IEntity script.
	/// </summary>
	/// <param name="target">Game object to look for an IEntity script on</param>
	/// <param name="source">Type of damage and it's source object</param>
	/// <param name="damage">amount of damage to doDamage.</param>
	protected virtual bool AttackGameObject(GameObject target, DamageSource source, float damage)
	{
		GameObject parent = target;
		while (parent != null) 
		{
			if ((target.tag == "Entity" || target.tag == "Player") && AttackGameObjectOnly(target, source, damage)) {
				return true;
			}
			if(parent.transform == null || parent.transform.parent == null)
			{
				return false;
			}
			parent = parent.transform.parent.gameObject;
		}
		return false;
	}
}
