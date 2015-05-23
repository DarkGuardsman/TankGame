using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject shooter;
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
		Debug.Log ("Bullet Impact");
		Destroy(gameObject);
	}
}
