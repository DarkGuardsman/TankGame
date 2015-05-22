using UnityEngine;
using System.Collections;

//Controls the movement of the tank
public class movement : MonoBehaviour {

	public Rigidbody rb;
	public float speed = 90f;
	public float turnSpeed = 5f;
	public float hoverForce = 65f;
	public float hoverHeight = 2.5f;
	private float powerInput;
	private float turnInput;

	//Used to ray trace from
	public GameObject body;

	//On load of the object
	void Awake()
	{
		rb = GetComponent<Rigidbody>();
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
		Vector3 start = body.transform.position;
		Vector3 end = -body.transform.up;
		Ray ray = new Ray (start, end);
		RaycastHit hit;
		
		Debug.DrawRay (start, end, Color.red);
		
		if (Physics.Raycast (ray, out hit, hoverHeight)) {
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
			rb.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}
	
		//Moves the tank forward or backwards
		rb.AddRelativeForce (0f, 0f, powerInput * speed);

		//Rotates the tank
		rb.AddRelativeTorque (0f, turnInput * turnSpeed, 0f);
	}
}
