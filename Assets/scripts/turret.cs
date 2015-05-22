using UnityEngine;
using System.Collections;

public class turret : MonoBehaviour {

	public float XSensitivity = 2f;
	private Quaternion m_CharacterTargetRot;



	void Awake()
	{
		Screen.lockCursor = true;
		m_CharacterTargetRot = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Handles mouse movement for turret rotation
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;		
		m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
		transform.localRotation = m_CharacterTargetRot;
	}
}
