using UnityEngine;
using System.Collections;

public class AI_Cannon : Cannon
{
	public bool fire = false;

	public override bool ShouldFire ()
	{
		return fire;
	}
}
