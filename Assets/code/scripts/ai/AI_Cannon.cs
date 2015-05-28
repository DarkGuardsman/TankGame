using UnityEngine;
using System.Collections;

public class AI_Cannon : Cannon {

	public bool fire = false;

	protected override bool userClickFire()
	{
		return fire;
	}
}
