using UnityEngine;
using System.Collections;
/// <summary>
/// Basic interface to be aplied gameobjects that can take damage
/// </summary>
public interface IEntity {

	///<summary>
	///Amount of Heath the entity has left
	///</summary>
	///<remarks>Can be used to access the heath of the entity</remarks>
	///<returns>float value of the hp</returns>
	float getHeath();

	///<summary>
	///Directly sets the heath value of the entity
	///</summary>
	///<remarks>Shouldn't be used to deal damage</remarks>
	void setHeath(float amount);

	///<summary>
	///Used to inflict damage to the entity
	///</summary>
	///<remarks>TODO add damage type</remarks>
	/// <returns>True if the entity was damaged</returns>
	bool damageEntity(float damage);

	///<summary>
	///Checks to see if the entity is dead
	///</summary>
	/// <returns>True if the entity is dead and waiting to be despawned</returns>
	bool isDead();
}
