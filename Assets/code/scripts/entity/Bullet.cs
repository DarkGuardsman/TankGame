using UnityEngine;
using System.Collections;
using BuiltBroken.Damage;

public class Bullet : Entity {

	public GameObject shooter;
	public float damage = 25f;
	public int MAX_TICKS = 10000;
	private int ticks = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Ensure the bullet dies if no collision happens
		ticks++;
		if(ticks >= MAX_TICKS)
		{
			Destroy(gameObject);
		}
	}
	
	
	void OnCollisionEnter(Collision collision)
	{
		//Create damage source
		DamageSource damageSource;
		if (shooter != null) {
			damageSource = new BulletDamageSource(shooter);
		} else {
			damageSource = new BulletDamageSource(gameObject);
		}

		this.AttackGameObject (collision.gameObject, damageSource, damage);
		Destroy(gameObject);
	}	
}
