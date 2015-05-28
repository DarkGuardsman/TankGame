using UnityEngine;
using System.Collections;

public class PCannon : Cannon 
{
	protected override bool userClickFire()
	{
		return !fullAuto && Input.GetMouseButtonDown (0) || fullAuto && Input.GetMouseButton (0);
	}
}
