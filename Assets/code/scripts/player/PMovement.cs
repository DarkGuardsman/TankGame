using UnityEngine;
using System.Collections;

//Controls the movement of the tank
public class Movement : MonoBehaviour {

	//Settings
	public float speed = 90f;
	public float turnSpeed = 5f;
	public float hoverForce = 65f;
	public float hoverHeight = 2.5f;
	public float tiltChangeValue = 0.1f;
	
	//Objects
	public Rigidbody rb;
	public GameObject centerOfMass;	

	//raycast data
	public GameObject rayCluster;	
	private Transform backLeft;
	private Transform backRight;
	private Transform frontLeft;
	private Transform frontRight;	
	
	private RaycastHit lr;
	private RaycastHit rr;
	private RaycastHit lf;
	private RaycastHit rf;
	
	private float powerInput;
	private float turnInput;
	private int collisionCount = 0;

	//On load of the object
	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		rb.centerOfMass = centerOfMass.transform.localPosition;

		//Only populate if empty, allows manual setting of trace points
		backLeft = rayCluster.transform.Find("backLeft");
		backRight = rayCluster.transform.Find("backRight");
		frontLeft = rayCluster.transform.Find("frontLeft");
		frontRight = rayCluster.transform.Find("frontRight");			
	}


	// On enable of the object
	void Start () {

	}
	
	// Called each frame update
	void Update () 
	{
		//Grabs input from the user
		powerInput = Input.GetAxis ("Vertical");
		turnInput = Input.GetAxis ("Horizontal");
	}

	//Called each normal update
	void FixedUpdate()
	{
		Stabalize();
		HandleHoverRays();

		//Hover rotation update
		Vector3 start = rayCluster.transform.position;
		Vector3 end = -rayCluster.transform.up;
		Ray ray = new Ray (start, end);
		RaycastHit hit;
		
		Debug.DrawRay (start, end, Color.red);
		
		if (Physics.Raycast (ray, out hit, hoverHeight)) {
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = rayCluster.transform.up * proportionalHeight * hoverForce;
			rb.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}
	
		//Moves the tank forward or backwards
		rb.AddRelativeForce (0f, 0f, powerInput * speed);

		//Rotates the tank
		rb.AddRelativeTorque (0f, turnInput * turnSpeed, 0f);
	}
	
	//Handles all hover code
	void HandleHoverRays()
	{
		//Raycast all 4 corners strait down
		Physics.Raycast(backLeft.position + Vector3.up, Vector3.down, out lr);
		Physics.Raycast(backRight.position + Vector3.up, Vector3.down, out rr);
		Physics.Raycast(frontLeft.position + Vector3.up, Vector3.down, out lf);
		Physics.Raycast(frontRight.position + Vector3.up, Vector3.down, out rf);
 		
		//calculate new up
		Vector3 newUp = (Vector3.Cross (rr.point - Vector3.up, lr.point - Vector3.up) +
			Vector3.Cross (lr.point - Vector3.up, lf.point - Vector3.up) +
			Vector3.Cross (lf.point - Vector3.up, rf.point - Vector3.up) +
			Vector3.Cross (rf.point - Vector3.up, rr.point - Vector3.up)).normalized;

		//Slow rotation to prevent screen rapid motion
		transform.up = Vector3.Slerp(transform.up, newUp, tiltChangeValue);

		//Debug info
		Debug.DrawRay(rr.point, Vector3.up);
		Debug.DrawRay(lr.point, Vector3.up);
		Debug.DrawRay(lf.point, Vector3.up);
		Debug.DrawRay(rf.point, Vector3.up);
	}
	
	//Called to level the tank out
	void Stabalize()
	{		
		//Code only triggers when no collisions exist
		if (collisionCount <= 0) {
			//Ensures the tank is levelled on X
			if (transform.rotation.x > (Mathf.Deg2Rad * 5f)) {
				transform.Rotate ((Mathf.Deg2Rad * -1f), 0f, 0f);
			} else if (transform.rotation.x < (Mathf.Deg2Rad * -5f)) {
				transform.Rotate ((Mathf.Deg2Rad * 1f), 0f, 0f);
			}

			//Ensures the tank is levelled on Z
			if (transform.rotation.z > (Mathf.Deg2Rad * 5f)) {
				transform.Rotate ((Mathf.Deg2Rad * -1f), 0f, 0f);
			} else if (transform.rotation.z < (Mathf.Deg2Rad * -5f)) {
				transform.Rotate ((Mathf.Deg2Rad * 1f), 0f, 0f);
			}
		}
	}
	

	void OnCollisionEnter()
	{
		collisionCount++;
		Debug.Log ("Collision Detected");
	}

	void OnCollisionExit()
	{
		collisionCount--;
	}
}
