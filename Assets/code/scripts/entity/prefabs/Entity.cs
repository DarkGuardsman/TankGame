using UnityEngine;
using System.Collections;
using BuiltBroken.Damage;
/// <summary>
/// Basic gameobject that can take damage
/// </summary>
public class Entity : MonoBehaviour, IEntity {

	private float hp;
	protected bool alive = true;

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
	/// Attacks the game object threw all of it's IEntity scripts
	/// </summary>
	/// <param name="target">Game object to look for an IEntity script on</param>
	/// <param name="source">Type of damage and it's source object</param>
	/// <param name="damage">amount of damage to doDamage.</param>
	protected virtual bool AttackGameObjectOnly(GameObject target, DamageSource source, float damage)
	{
		Debug.Log ("Attacking object " + target);
		bool damagedTarget = false;
		IEntity[] scripts = target.GetComponents<IEntity>();
		for(int i = 0; i < scripts.Length; i++)
		{
			Debug.Log ("\tIEntity " + scripts[i]);
			if(scripts[i].damageEntity(source, damage))
			{
				Debug.Log ("\t\tTook Damage");
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
			if (target.tag == "Entity" && AttackGameObjectOnly(target, source, damage)) {
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
