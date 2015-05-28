using UnityEngine;
using System.Collections;

public class CarEngine : MovementEngine {

	public GameObject wheelCluster;

	public float steer_max = 20;

	private float steer = 0;
	private float motor = 0;
	private float brake = 0;

	private WheelCollider rearLeftWheel;
	private WheelCollider rearRightWheel;
	private WheelCollider frontLeftWheel;
	private WheelCollider frontRightWheel;

	// Use this for initialization
	protected override void Awake () {
		base.Awake ();
		rearLeftWheel = wheelCluster.transform.FindChild ("BL").GetComponent<WheelCollider>();
		rearRightWheel = wheelCluster.transform.FindChild ("BR").GetComponent<WheelCollider>();
		frontLeftWheel = wheelCluster.transform.FindChild ("FL").GetComponent<WheelCollider>();
		frontRightWheel = wheelCluster.transform.FindChild ("FR").GetComponent<WheelCollider>();
	}
	
	protected override void FixedUpdate () {
		
		steer = Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1);
		motor = Mathf.Clamp(Input.GetAxis("Vertical"), -1, 1);
		
		rearLeftWheel.motorTorque = speed * motor;
		rearRightWheel.motorTorque = speed * motor;
		
		frontLeftWheel.steerAngle = steer_max * steer;
		frontRightWheel.steerAngle = steer_max * steer;
		
	}
}
