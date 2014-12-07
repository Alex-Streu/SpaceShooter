using UnityEngine;
using System.Collections;

public class PlayerLaserController : LaserController {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			other.SendMessage("Damage", _damage);
			Destroy(gameObject);
		}
	}
}
