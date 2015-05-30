using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AITurret : MonoBehaviour
{

	public GameObject target;
	public GameObject entity;
	public Transform cannon;

	public float speed = 3f;
	public float turnSpeed = 10f;
	public float firingAngle = .04f;
	public int targetLostSeconds = 10;
	public int targetFindDelay = 2;
	
	private Vector3 desiredRotation;

	//List of valid targets
	private ArrayList targetList = new ArrayList ();

	private float targetLostTicks = 0;
	
	void Start ()
	{
		desiredRotation = new Vector3 (0, 0, 0);
	}

	void Update ()
	{
		if (desiredRotation != null) {
			transform.eulerAngles = Vector3.Slerp (transform.eulerAngles, new Vector3 (transform.eulerAngles.x, desiredRotation.y, transform.eulerAngles.z), Time.deltaTime * turnSpeed);
			cannon.eulerAngles = Vector3.Slerp (cannon.eulerAngles, new Vector3 (desiredRotation.x, cannon.eulerAngles.y, cannon.eulerAngles.z), Time.deltaTime * turnSpeed);
		}
	}

	void FixedUpdate ()
	{
		//Reused target lost ticks as a counter for finding new targets
		if (target == null && targetLostTicks <= -targetFindDelay) {
			targetLostTicks = 0;
			FindTarget ();
		}

		//If target is valid update aim position
		if (isTargetValid ()) {
			targetLostTicks = targetLostSeconds;
			desiredRotation = Quaternion.LookRotation (target.transform.position - transform.position).eulerAngles;

			cannon.GetComponent<AICannon> ().fire = Vector3.Distance (desiredRotation, new Vector3 (cannon.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)) <= firingAngle;
			Debug.Log (desiredRotation + "   T:" + transform.eulerAngles + "  C:" + cannon.eulerAngles + "  F:" + cannon.GetComponent<AICannon> ().fire + "  CF:" + cannon.GetComponent<AICannon> ().CanFire ());
		} else {
			//If target is not valid count down until target lost
			targetLostTicks -= Time.deltaTime;
			if (targetLostTicks <= 0) {
				target = null;
			}
		}
	}

	bool canSeeTarget (Transform tran)
	{
		RaycastHit hit;
		bool hitSomething = Physics.Raycast (tran.position, cannon.GetComponent<AICannon> ().firingNode.transform.position - tran.position, out hit);
		Debug.DrawLine (cannon.GetComponent<AICannon> ().firingNode.transform.position, hit.point, Color.yellow);
		return  hitSomething && hit.transform == tran;
	}

	bool isTargetValid ()
	{
		return target != null && canSeeTarget (target.transform);
	}

	void FindTarget ()
	{
		GameObject bestTarget = null;
		float distance = 1000;
		foreach (Object obj in targetList) {
			if (obj != null) {
				GameObject potentialTarget = obj as GameObject;
				if (isValidTarget (potentialTarget)) {
					float d = Vector3.Distance (potentialTarget.transform.position, transform.position);
					if (d < distance) {
						distance = d;
						bestTarget = potentialTarget;
					}
				}
			}
		}
		target = bestTarget;
	}

	bool isValidTarget (GameObject obj)
	{
		return entity.GetComponent<Entity> ().IsValidTarget (obj);
	}

	void OnTriggerEnter (Collider other)
	{
		if (!targetList.Contains (other.gameObject) && isValidTarget (other.gameObject)) {
			targetList.Add (other.gameObject);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (targetList.Contains (other.gameObject)) {
			targetList.Remove (other.gameObject);
		}
	}
}
