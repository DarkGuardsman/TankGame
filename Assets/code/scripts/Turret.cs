using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	public float MaxCannonPitch = 30f;
	public float MinCannonPitch = -25f;
	private Quaternion m_turretTargetRot;
	private GameObject cannonObject;
	private float rotationX;


	void Awake()
	{
		Screen.lockCursor = true;
		m_turretTargetRot = transform.localRotation;
		cannonObject = gameObject.transform.Find ("cannon").gameObject;
		rotationX = cannonObject.transform.localEulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Handles mouse movement for turret rotation
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

		// Rotates the turret around the Y axis
		m_turretTargetRot *= Quaternion.Euler (0f, yRot, 0f);
		transform.localRotation = m_turretTargetRot;

		//Rotates the barrel
		rotationX += xRot;
		rotationX = Mathf.Clamp (rotationX, MinCannonPitch, MaxCannonPitch);             
		cannonObject.transform.localEulerAngles = new Vector3(-rotationX, cannonObject.transform.localEulerAngles.y, cannonObject.transform.localEulerAngles.z);
	}
}
