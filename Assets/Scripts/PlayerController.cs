using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary2D
{
	public float xMin, xMax, zMin, zMax;
}

[System.Serializable]
public class Boundary3D
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

	public float _health;
	public float _damage;
	public float _speed;
	public float _3Dspeed;
	public float _hAngularSpeed;
	public float _vAngularSpeed;
	public Boundary2D _boundary2d;
	public Boundary3D _boundary3d;
	
	public GameObject _shot;
	public Transform _shotSpawn;
	public float _rateOfFire;
	public GameObject _crossFront;
	public GameObject _crossBack;
	
	bool _is3d;
	bool _switchEngines;
	float _switchStartTime;
	GameObject _engine2d;
	GameObject _engine3d;
	
	float _nextFire;
	
	void Start()
	{
		//Turn off 3d engine
		_engine2d = GameObject.Find ("engines_player");
		_engine3d = GameObject.Find ("engine_player3d");
		_engine3d.SetActive(false);
		_switchEngines = false;
		
		//Turn off crosshairs
		_crossFront.SetActive(false);
		_crossBack.SetActive(false);
	}
	
	void Update()
	{
		//Switch engines during transition
		if (_switchEngines && Time.time - _switchStartTime > 0.5f)
		{
			if (_is3d)
			{
				//Switch engines and turn on crosshairs
				_engine2d.SetActive(false);
				_engine3d.SetActive(true);
				_crossFront.SetActive(true);
				_crossBack.SetActive(true);
			}
			_switchEngines = false;
		}
		
		//If fire is pressed and it's been enough time
		if (Input.GetButton("Fire1") && Time.time > _nextFire)
		{
			_nextFire = Time.time + _rateOfFire;
			Instantiate(_shot, _shotSpawn.position, _shotSpawn.rotation);
		}
	}
	
	void FixedUpdate()
	{
		float hMove = Input.GetAxis ("Horizontal");
		float vMove = Input.GetAxis ("Vertical");
		Vector3 movement = _is3d ? new Vector3(hMove, vMove, 0.0f) : new Vector3(hMove, 0.0f, vMove);
		rigidbody.velocity = movement * _speed;
		
		rigidbody.position = _is3d ?
			//3D clamp position
			new Vector3(
				Mathf.Clamp(rigidbody.position.x, _boundary3d.xMin, _boundary3d.xMax),
				Mathf.Clamp (rigidbody.position.y, _boundary3d.yMin, _boundary3d.yMax),
				0.0f
				) :
			//2D clamp position
			new Vector3(
				Mathf.Clamp(rigidbody.position.x, _boundary2d.xMin, _boundary2d.xMax),
				0.0f,
				0.0f
			);
		
		//Rotate ship for effect
		rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.y * -_vAngularSpeed, 0.0f, rigidbody.velocity.x * -_hAngularSpeed);
	}
	
	//Set mode between 2d and 3d
	public void SetMode(bool _3d)
	{
		_is3d = _3d;
		if (_is3d)
		{
			rigidbody.position = new Vector3(
				Mathf.Clamp(rigidbody.position.x, _boundary3d.xMin, _boundary3d.xMax),
				Mathf.Clamp (rigidbody.position.y, _boundary3d.yMin, _boundary3d.yMax),
				0.0f
				);
			_speed = _3Dspeed;
			_switchEngines = true;
			_switchStartTime = Time.time;
		}
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
