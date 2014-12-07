using UnityEngine;
using System.Collections;

public class EnemyLaserController : LaserController {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.SendMessage("Damage", _damage);
			Destroy(gameObject);
		}
	}
}
