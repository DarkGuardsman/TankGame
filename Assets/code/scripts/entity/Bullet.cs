using UnityEngine;
using System.Collections;
using BuiltBroken.Damage;

public class Bullet : Entity {

	public GameObject shooter;
	public float damage = 25f;
	public int MAX_TICKS = 10000;
	private int ticks = 0;
	private Vector3 prev_pos;

	// Use this for initialization
	void Start () {
		prev_pos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Ensure the bullet dies if no collision happens
		ticks++;
		if(ticks >= MAX_TICKS)
		{
			Destroy(gameObject);
			return;
		}

		Physics.Linecast (transform.position, prev_pos);
		prev_pos = transform.position;
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
