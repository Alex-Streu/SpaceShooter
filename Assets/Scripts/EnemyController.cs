using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float _health;
	public float _damage;
	public float _speed;
	public GameObject _shot;
	public Transform _shotSpawn;
	public float _rateOfFire;
	
	float _nextFire;
	Quaternion _shotRotation;
	
	void Start()
	{
		_shotRotation = _shotSpawn.rotation;
	}
	
	void Update()
	{		
		if ( Time.time > _nextFire)
		{
			_nextFire = Time.time + _rateOfFire;
			Instantiate(_shot, _shotSpawn.position, _shotRotation);
		}
	}
	
	void FixedUpdate()
	{
		Vector3 movement = new Vector3(0.0f, 0.0f, -1.0f);
		rigidbody.velocity = movement * _speed;
	}
	
	//Take damage
	public void Damage(float damage)
	{	
		_health -= damage;
		if (_health <= 0.0f)
		{
			Destroy(gameObject);
		}
	}
}
