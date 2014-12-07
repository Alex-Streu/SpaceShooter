using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {

	public float _speed;
	public float _damage;
	
	void FixedUpdate()
	{
		rigidbody.velocity = transform.up * _speed;
	}
}
