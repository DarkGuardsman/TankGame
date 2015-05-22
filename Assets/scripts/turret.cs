using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {

	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	private Quaternion m_turretTargetRot;
	private Quaternion m_cannonTargetRot;
	private GameObject cannonObject;


	void Awake()
	{
		Screen.lockCursor = true;
		m_turretTargetRot = transform.localRotation;
		cannonObject = gameObject.transform.Find ("cannon").gameObject;
		m_cannonTargetRot = cannonObject.transform.localRotation;
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
		m_cannonTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);
		cannonObject.transform.localRotation = m_cannonTargetRot;
	}
}
